using AutoMapper;
using Microsoft.Extensions.Logging;
using Utils.Interfaces;
using Microsoft.Extensions.Configuration;
using FluentValidation;
using Business.Interfaces.Seguridad;
using Utils;
using Models.Models;
using Models.Validations.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Models.Request.Usuarios;

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
                usuarioMapeado.sContra = _filtros.HashPassword(usuarioMapeado.sContra);
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

        public async Task<Response<EntUsuarios>> UpdatePassword(EntChangePassword entPassword)
        {
            var response = new Response<EntUsuarios>();

            try
            {
                Response<bool> validarLogin = ValidarDatosLogin(entPassword);
                if (validarLogin.HasError)
                {
                    return response.GetResponse(validarLogin);
                }

                Response<EntUsuarios> obtenerUsuario = await _usuariosRepositorio.DGet(entPassword.sCorreo);

                EntUsuarios usuario = obtenerUsuario.Result;

                if (!_filtros.VerifyPassword(obtenerUsuario.Result.sContra, entPassword.sContra))
                {
                    response.SetError("Contraseña incorrecta");
                    return response;
                }else if (entPassword.sNContra != entPassword.sNCContra)
                {
                    response.SetError("Contraseña no coinciden");
                    return response;
                }
                var usuarioMapeado = _mapper.Map<EntUsuarios>(usuario);
                usuarioMapeado.sContra = _filtros.HashPassword(entPassword.sNContra);
                var result = await _usuariosRepositorio.DUpdatePassword(usuarioMapeado);
                result.Result.sContra = "";
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
        private Response<bool> ValidarDatosLogin(EntChangePassword entPassword)
        {
            Response<bool> response = new();
            if (entPassword is null)
            {
                return response.GetBadRequest("No se ingresó información");
            }else if (string.IsNullOrWhiteSpace(entPassword.sCorreo))
            {
                return response.GetBadRequest("No se ingresó el correo");
            }else if (string.IsNullOrWhiteSpace(entPassword.sContra))
            {
                return response.GetBadRequest("No se ingresó la contraseña");
            }
            else if (string.IsNullOrWhiteSpace(entPassword.sNContra))
            {
                return response.GetBadRequest("No se ingresó la nueva contraseña");
            }
            else if (string.IsNullOrWhiteSpace(entPassword.sNCContra))
            {
                return response.GetBadRequest("No se ingresó la confirmación de contraseña");
            }

            return response.GetSuccess(true);
        }
    }
}
