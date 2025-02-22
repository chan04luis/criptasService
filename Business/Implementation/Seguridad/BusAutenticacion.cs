using AutoMapper;
using Business.Data;
using Business.Interfaces.Seguridad;
using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using Models.Seguridad;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utils;
using Utils.Interfaces;

namespace Business.Implementation.Seguridad
{
    public class BusAutenticacion:IBusAutenticacion
    {
        private readonly IUsuariosRepositorio _datUsuario;
        private readonly IConfiguracionesRepositorio datConfiguracion;
        private readonly IHttpContextAccessor httpContext;
        private readonly IBusPermiso busPermisos;
        private readonly IMapper mapeador;
        private readonly IConfiguration configuration;
        private readonly IClientesRepositorio clientesRepositorio;
        private readonly IFiltros _filtros;

        public BusAutenticacion(IMapper mapeador, IUsuariosRepositorio _datUsuario, IConfiguracionesRepositorio datConfiguracion, IHttpContextAccessor httpContext, 
            IBusPermiso busPermisos, IConfiguration configuration, IClientesRepositorio clientesRepositorio, IFiltros filtros)
        {
            this.mapeador = mapeador;
            this._datUsuario = _datUsuario;
            this.datConfiguracion = datConfiguracion;
            this.httpContext = httpContext;
            this.busPermisos = busPermisos;
            this.configuration = configuration;
            this.clientesRepositorio = clientesRepositorio;
            _filtros = filtros;
        }
        public async Task<Response<LoginClienteResponseModelo>> LoginMovil(LoginModelo loginModel)
        {
            var response = new Response<LoginClienteResponseModelo>();
            string token = string.Empty;
            try
            {
                Response<bool> validarLogin = ValidarDatosLogin(loginModel);
                if (validarLogin.HasError)
                {
                    return response.GetResponse(validarLogin);
                }

                Response<EntClientes> obtenerUsuario = await clientesRepositorio.DGetByEmail(loginModel.sCorreo, loginModel.sPassword);
                if (obtenerUsuario.HasError)
                {
                    response.SetError("No se encontro correo");
                    return response;
                }else if(!_filtros.VerifyPassword(obtenerUsuario.Result.sContra, loginModel.sPassword))
                {
                    response.SetError("Contraseña incorrecta");
                }
                else
                {
                    await clientesRepositorio.DUpdateToken(obtenerUsuario.Result.uId, loginModel.sTokenFireBase);
                    var sToken = new LoginClienteResponseModelo{
                        Token = GenerateJwtToken(obtenerUsuario.Result),
                        uId = obtenerUsuario.Result.uId.ToString(),
                    };
                    response.SetSuccess(sToken);
                }
            }
            catch (Exception ex)
            {
                response.SetError("Credenciales no validadas: "+ ex.Message);
            }


            return response;

        }
        public async Task<Response<LoginResponseModelo>> Login(LoginModelo loginModel)
        {
            var response = new Response<LoginResponseModelo>();
            string token = string.Empty;
            try
            {
                Response<bool> validarLogin = ValidarDatosLogin(loginModel);
                if (validarLogin.HasError)
                {
                    return response.GetResponse(validarLogin);
                }

                Response<EntUsuarios> obtenerUsuario = await _datUsuario.DGet(loginModel.sCorreo);

                EntUsuarios usuario = obtenerUsuario.Result;

                if (!_filtros.VerifyPassword(obtenerUsuario.Result.sContra, loginModel.sPassword))
                {
                    return response.GetUnauthorized("Contraseña incorrecta");
                }

                UsuarioModelo usuarioMapeado = mapeador.Map<UsuarioModelo>(usuario);

                Guid uIdPerfil = usuario.uIdPerfil;

                if (obtenerUsuario.Result != null)
                {
                    token = GenerateJwtToken(usuario);
                }
                else
                {
                    return response.GetUnauthorized("Credenciales no validadas");
                }

                Response<Configuracion> obtenerConfiguracion = await datConfiguracion.DObtenerConfiguracion();
                if (obtenerConfiguracion.HasError)
                {
                    return response.GetError("Credenciales no validadas");
                }

                Response<PerfilPermisosModelo> obtenerPermisos = await busPermisos.ObtenerPermisos(uIdPerfil);
                if (obtenerPermisos.HasError)
                {
                    return response.GetError("Credenciales no validadas");
                }

                Response<object> obtenerMenu = await busPermisos.ObtenerPermisosMenu(uIdPerfil);
                if (obtenerMenu.HasError)
                {
                    return response.GetError("Credenciales no validadas");
                }
                ConfiguracionModelo obtenerConfiguracionMapeado = mapeador.Map<ConfiguracionModelo>(obtenerConfiguracion.Result);

                LoginResponseModelo entLoginResponse = new()
                {
                    Configuracion = obtenerConfiguracionMapeado,
                    PermisosModulos = obtenerPermisos.Result.Permisos.Select(x => new PermisoModuloModelo
                    {
                        IdModulo = x.IdModulo,
                        ClaveModulo = x.ClaveModulo,
                        NombreModulo = x.NombreModulo,
                        TienePermiso = x.TienePermiso
                    }).ToList(),
                    PermisosPaginas = obtenerPermisos.Result.Permisos.SelectMany(x => x.PermisosPagina).Select(x => new PermisoPaginaModelo
                    {
                        ClavePagina = x.ClavePagina,
                        IdPagina = x.IdPagina,
                        NombrePagina = x.NombrePagina,
                        TienePermiso = x.TienePermiso
                    }).ToList(),
                    PermisosBotones = obtenerPermisos.Result.Permisos.SelectMany(x => x.PermisosPagina).SelectMany(x => x.PermisosBoton).ToList(),

                    Token = token,
                    Usuario = usuarioMapeado,
                    Menu = obtenerMenu.Result
                };

                response.SetSuccess(entLoginResponse);
            }
            catch (Exception ex)
            {
                response.SetError("Credenciales no validadas");
            }


            return response;

        }
        public async Task<Response<LoginResponseModelo>> Refresh()
        {
            var response = new Response<LoginResponseModelo>();
            string token = string.Empty;
            try
            {

                string idUsuario = httpContext.HttpContext.User.FindFirstValue("userId");
                string idPerfil = httpContext.HttpContext.User.FindFirstValue("ProfileId");
                Guid uIdPerfil = Guid.Parse(idPerfil);
                Guid uIdUsuario = Guid.Parse(idUsuario);

                Response<EntUsuarios> obtenerUsuario = await _datUsuario.DGetByIdAndPerfilAsync(uIdUsuario, uIdPerfil);

                EntUsuarios usuario = obtenerUsuario.Result;

                UsuarioModelo usuarioMapeado = mapeador.Map<UsuarioModelo>(usuario);

                if (obtenerUsuario.Result != null)
                {
                    token = GenerateJwtToken(usuario);
                }
                else
                {
                    return response = response.GetError("Credenciales no validadas");
                }


                Response<Configuracion> obtenerConfiguracion = await datConfiguracion.DObtenerConfiguracion();
                if (obtenerConfiguracion.HasError)
                {
                    return response.GetError("Credenciales no validadas");
                }

                Response<PerfilPermisosModelo> obtenerPermisos = await busPermisos.ObtenerPermisos(uIdPerfil);
                if (obtenerPermisos.HasError)
                {
                    return response.GetError("Credenciales no validadas");
                }

                Response<object> obtenerMenu = await busPermisos.ObtenerPermisosMenu(uIdPerfil);
                if (obtenerMenu.HasError)
                {
                    return response.GetError("Credenciales no validadas");
                }

                ConfiguracionModelo obtenerConfiguracionMapeado = mapeador.Map<ConfiguracionModelo>(obtenerConfiguracion.Result);

                LoginResponseModelo entLoginResponse = new()
                {
                    Configuracion = obtenerConfiguracionMapeado,
                    PermisosModulos = obtenerPermisos.Result.Permisos.Select(x => new PermisoModuloModelo
                    {
                        IdModulo = x.IdModulo,
                        ClaveModulo = x.ClaveModulo,
                        NombreModulo = x.NombreModulo,
                        TienePermiso = x.TienePermiso
                    }).ToList(),
                    PermisosPaginas = obtenerPermisos.Result.Permisos.SelectMany(x => x.PermisosPagina).Select(x => new PermisoPaginaModelo
                    {
                        ClavePagina = x.ClavePagina,
                        IdPagina = x.IdPagina,
                        NombrePagina = x.NombrePagina,
                        TienePermiso = x.TienePermiso
                    }).ToList(),
                    PermisosBotones = obtenerPermisos.Result.Permisos.SelectMany(x => x.PermisosPagina).SelectMany(x => x.PermisosBoton).ToList(),

                    Token = token,
                    Usuario = usuarioMapeado,
                    Menu = obtenerMenu.Result
                };

                response.SetSuccess(entLoginResponse);
            }
            catch (Exception ex)
            {
                response.SetError("Credenciales no validadas");
            }
            return response;

        }
        private Response<bool> ValidarDatosLogin(LoginModelo entLoginRequest)
        {
            Response<bool> response = new();
            if (entLoginRequest is null)
            {
                return response.GetBadRequest("No se ingresó información");
            }

            if (string.IsNullOrWhiteSpace(entLoginRequest.sCorreo))
            {
                return response.GetBadRequest("No se ingresó el correo");
            }

            if (string.IsNullOrWhiteSpace(entLoginRequest.sPassword))
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
                new Claim(ClaimTypes.Name, usuario.sContra),
                new Claim("Correo", usuario.sCorreo),
                new Claim("userId", usuario.uId.ToString()),
                new Claim("ProfileId", usuario.uIdPerfil.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateJwtToken(EntClientes usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.uId.ToString()),
                new Claim(ClaimTypes.Name, usuario.sContra),
                new Claim("Correo", usuario.sEmail),
                new Claim("userId", usuario.uId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
