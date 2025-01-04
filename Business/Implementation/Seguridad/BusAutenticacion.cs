using AutoMapper;
using Business.Data;
using Business.Interfaces.Seguridad;
using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Models.Models;
using Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Business.Implementation.Seguridad
{
    public class BusAutenticacion:IBusAutenticacion
    {
        private readonly BusJwt _busJwt;
        private readonly IUsuariosRepositorio _datUsuario;
        private readonly IConfiguracionesRepositorio datConfiguracion;
        private readonly IHttpContextAccessor httpContext;
        private readonly IBusPermiso busPermisos;
        private readonly IMapper mapeador;
        public BusAutenticacion(IMapper mapeador, BusJwt _busJwt, IUsuariosRepositorio _datUsuario, IConfiguracionesRepositorio datConfiguracion, IHttpContextAccessor httpContext, IBusPermiso busPermisos)
        {
            this.mapeador = mapeador;
            this._busJwt = _busJwt;
            this._datUsuario = _datUsuario;
            this.datConfiguracion = datConfiguracion;
            this.httpContext = httpContext;
            this.busPermisos = busPermisos;
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

                Response<EntUsuarios> obtenerUsuario = await _datUsuario.DGet(loginModel.sCorreo, loginModel.sPassword);

                EntUsuarios usuario = obtenerUsuario.Result;

                UsuarioModelo usuarioMapeado = mapeador.Map<UsuarioModelo>(usuario);

                Guid uIdPerfil = usuario.uIdPerfil;

                if (obtenerUsuario.Result != null)
                {
                    token = GenerarToken(usuario);
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
                        idModulo = x.idModulo,
                        claveModulo = x.claveModulo,
                        nombreModulo = x.nombreModulo,
                        tienePermiso = x.tienePermiso
                    }).ToList(),
                    PermisosPaginas = obtenerPermisos.Result.Permisos.SelectMany(x => x.permisosPagina).Select(x => new PermisoPaginaModelo
                    {
                        clavePagina = x.clavePagina,
                        idPagina = x.idPagina,
                        nombrePagina = x.nombrePagina,
                        tienePermiso = x.tienePermiso
                    }).ToList(),
                    PermisosBotones = obtenerPermisos.Result.Permisos.SelectMany(x => x.permisosPagina).SelectMany(x => x.permisosBoton).ToList(),

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
        private string GenerarToken(EntUsuarios usuario)
        {
            var authClaims = new List<Claim>
                  {
                      new Claim(ClaimTypes.NameIdentifier, usuario.sCorreo),
                      new Claim("Correo", usuario.sCorreo),
                      new Claim("userId", usuario.uId.ToString()),
                      new Claim("ProfileId", usuario.uIdPerfil.ToString()),
                  };

            var entAutenticacion = _busJwt.GenerateJwtToken(authClaims, "");

            return entAutenticacion.sToken;
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
    }
}
