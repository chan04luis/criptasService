using AutoMapper;
using Business.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Interfaces;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using Business.Interfaces.Seguridad;
using Utils;
using Modelos.Models;
using Modelos.Request.Usuarios;
using Modelos.Validations.Seguridad;
using Modelos.Responses.Iglesia;

namespace Business.Implementation.Seguridad
{
    public class BusUsuarios : IBusUsuarios
    {
        private readonly IUsuariosRepositorio _usuariosRepositorio;
        private readonly IFiltros _filtros;
        private readonly ILogger<BusUsuarios> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public BusUsuarios(IUsuariosRepositorio usuariosRepositorio, IFiltros filtros, ILogger<BusUsuarios> logger, IMapper mapper, IConfiguration configuration)
        {
            _usuariosRepositorio = usuariosRepositorio;
            _filtros = filtros;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Response<EntUsuarios>> SaveUser(EntUsuarioRequest usuario)
        {
            var response = new Response<EntUsuarios>();

            try
            {
                UsuarioValidator usuarioValidator = new UsuarioValidator();
                var validator = usuarioValidator.Validate(usuario);
                if (!validator.IsValid)
                {
                    var errors = string.Join(", ", validator.Errors.Select(e => e.ErrorMessage));
                    response.SetError(errors);
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var existMail = await _usuariosRepositorio.DAnyExistEmail(usuario.sCorreo);
                if (existMail.Result)
                {
                    response.SetError(existMail.Message);
                    return response;
                }

                var usuarioMapeado = _mapper.Map<EntUsuarios>(usuario);

                var usuarioCreado = await _usuariosRepositorio.DSave(usuarioMapeado);

                response.SetSuccess(usuarioCreado.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y guardar el usuario");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<Response<EntUsuarios>> UpdateUser(EntUsuarioUpdateRequest usuario)
        {
            var response = new Response<EntUsuarios>();

            try
            {
                UsuarioUpdateValidator usuarioValidator = new UsuarioUpdateValidator();
                var validator = usuarioValidator.Validate(usuario);
                if (!validator.IsValid)
                {
                    var errors = string.Join(", ", validator.Errors.Select(e => e.ErrorMessage));
                    response.SetError(errors);
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var usuarioMapeado = _mapper.Map<EntUsuarios>(usuario);

                /*var usuariosExistentes = await _usuariosRepositorio.AnyExitMailAndKey(usuarioMapeado);

                if (usuariosExistentes.Result)
                {
                    response.SetError("Usuario ya existente.");
                    return response;
                }*/

                var result = await _usuariosRepositorio.DUpdate(usuarioMapeado);

                response.SetSuccess(result.Result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y actualizar el usuario");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<Response<EntUsuarios>> UpdateUserStatus(EntUsuarioUpdateEstatusRequest usuario)
        {
            try
            {
                var usuarioMapeado = _mapper.Map<EntUsuarios>(usuario);
                return await _usuariosRepositorio.DUpdateEstatus(usuarioMapeado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado del usuario");
                var response = new Response<EntUsuarios>();
                response.SetError("Hubo un error al actualizar el estado del usuario.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteUserById(Guid id)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var existKey = await _usuariosRepositorio.AnyExistKey(id);
                if (!existKey.Result)
                {
                    response.SetError(existKey.Message);
                    return response;
                }

                response = await _usuariosRepositorio.DDelete(id);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntUsuarios>> GetUserById(Guid id)
        {
            try
            {
                return await _usuariosRepositorio.DGetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario por ID");
                var response = new Response<EntUsuarios>();
                response.SetError("Hubo un error al obtener el usuario.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntUsuarios>>> GetUsersByFilters(EntUsuarioSearchRequest filtros)
        {
            try
            {
                return await _usuariosRepositorio.DGetByFilters(filtros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los usuarios por filtros");
                var response = new Response<List<EntUsuarios>>();
                response.SetError("Hubo un error al obtener los usuarios.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntUsuarios>>> GetUserList()
        {
            try
            {
                return await _usuariosRepositorio.DGetList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de usuarios");
                var response = new Response<List<EntUsuarios>>();
                response.SetError("Hubo un error al obtener la lista de usuarios.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<AuthLogin>> getLogin(EntUsuarioLoginRequest usuario)
        {
            var response = new Response<AuthLogin>();

            try
            {
                Response<bool> validarDatosLogin = ValidarGetLogin(usuario);
                if (validarDatosLogin.HasError)
                {
                    return response.GetResponse(validarDatosLogin);
                }

                var obtenerUsuario = await _usuariosRepositorio.DLogin(usuario);

                if (obtenerUsuario?.Result == null)
                {
                    response.SetError(obtenerUsuario.Message);
                    response.HttpCode = System.Net.HttpStatusCode.Unauthorized;
                    return response;
                }

                var usuarioEntidad = obtenerUsuario.Result;

                var token = GenerateJwtToken(usuarioEntidad);
                response.SetSuccess(_mapper.Map<AuthLogin>(usuarioEntidad), "Login Exitoso");
                response.Result.sToken = token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y actualizar el usuario");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;

            }

            return response;
        }

        private Response<bool> ValidarGetLogin(EntUsuarioLoginRequest entLoginRequest)
        {
            Response<bool> response = new();
            if (entLoginRequest is null)
            {
                return response.GetBadRequest("Los campos Correo y contraseña son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(entLoginRequest.sCorreo))
            {
                return response.GetBadRequest("No se ingresó el correo");
            }

            if (string.IsNullOrWhiteSpace(entLoginRequest.sContra))
            {
                return response.GetBadRequest("No se ingresó la contraseña");
            }

            return response.GetSuccess(true);
        }

        private string GenerateJwtToken(EntUsuarios usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.uId.ToString()),
                new Claim(ClaimTypes.Name, usuario.sContra)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
