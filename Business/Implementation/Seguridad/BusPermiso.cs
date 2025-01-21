using AutoMapper;
using Business.Interfaces.Seguridad;
using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Models.Seguridad;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Business.Implementation.Seguridad
{
    public class BusPermiso:IBusPermiso
    {
        private readonly IMapper mapeador;
        private readonly IHttpContextAccessor httpContext;
        private readonly IPermisosRepositorio datPermisos;
        private readonly IBusPerfiles busPerfil;
        private readonly IPerfilesRepositorio datPerfil;
        private readonly ILogger<BusPermiso> _logger;

        public BusPermiso(IMapper mapeador, IHttpContextAccessor httpContext, IPermisosRepositorio datPermisos, IBusPerfiles busPerfil, IPerfilesRepositorio datPerfil, ILogger<BusPermiso> _logger)
        {
            this.mapeador = mapeador;
            this.httpContext = httpContext;
            this.datPermisos = datPermisos;
            this.busPerfil = busPerfil;
            this.datPerfil = datPerfil;
            this._logger = _logger;
        }

        public async Task<Response<PerfilPermisosModelo>> ObtenerPermisos(Guid idPerfil)
        {
            Response<PerfilPermisosModelo> response = new();
            try
            {
                Response<Perfil> obtenerPerfiles = await datPerfil.Get(idPerfil);
                if (obtenerPerfiles.HasError)
                {
                    return response.GetResponse(obtenerPerfiles);
                }

                if (obtenerPerfiles.Result == null)
                {
                    return response.GetNotFound("El perfil no existe");
                }

                Response<List<Modulo>> obtenerElementos = await datPermisos.GetPermisosElementos(idPerfil);
                if (obtenerElementos.HasError)
                {
                    return response.GetResponse(obtenerElementos);
                }

                List<PermisoModuloModelo> lstElementos = obtenerElementos.Result
                .Select(x => new PermisoModuloModelo
                {
                    IdPermisoModulo = x.lstPermisosModulos.FirstOrDefault()?.uIdPermisoModulo??Guid.Empty,
                    IdModulo = x.uIdModulo,
                    ClaveModulo = x.sClaveModulo,
                    NombreModulo = x.sNombreModulo,
                    TienePermiso = x.lstPermisosModulos.FirstOrDefault()?.bTienePermiso??false,
                    PermisosPagina = x.lstPaginas.GroupBy(y => y.uIdPagina)
                        .Select(y => new PermisoPaginaModelo
                        {
                            IdPermisoPagina = y.FirstOrDefault()?.lstPermisosPaginas.FirstOrDefault()?.uIdPermisoPagina??Guid.Empty,
                            IdPagina = y.FirstOrDefault().uIdPagina,
                            ClavePagina = y.FirstOrDefault().sClavePagina,
                            NombrePagina = y.FirstOrDefault().sNombrePagina,
                            TienePermiso = y.FirstOrDefault()?.lstPermisosPaginas.FirstOrDefault()?.bTienePermiso??false,
                            PermisosBoton = y.FirstOrDefault()?.lstBotones
                                .GroupBy(z => z.uIdBoton)
                                .Select(z => new PermisoBotonModelo
                                {
                                    IdPermisoBoton = z.FirstOrDefault()?.lstPermisosBotones.FirstOrDefault()?.uIdPermisoBoton??Guid.Empty,
                                    IdBoton = z.FirstOrDefault().uIdBoton,
                                    ClaveBoton = z.FirstOrDefault().sClaveBoton,
                                    NombreBoton = z.FirstOrDefault().sNombreBoton,
                                    TienePermiso = z.FirstOrDefault().lstPermisosBotones.FirstOrDefault()?.bTienePermiso??false
                                })
                                .Where(z => z.IdBoton != Guid.Empty)
                                .ToList()
                        })
                        .Where(y => y.IdPagina != Guid.Empty)
                        .ToList()
                })
                .Where(x => x.IdModulo != Guid.Empty)
                .ToList();

                PerfilModelo entPerfil = mapeador.Map<PerfilModelo>(obtenerPerfiles.Result);

                PerfilPermisosModelo entPerfilPermisos = new()
                {
                    Perfil = entPerfil,
                    Permisos = lstElementos
                };

                response.SetSuccess(entPerfilPermisos, "Se han consultado los permisos");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(ObtenerPermisos));
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<bool>> GuardarPermisos(GuardarPermisosModelo lstPermisosElementos)
        {
            Response<bool> response = new();
            try
            {

                if (lstPermisosElementos.Permisos is null)
                {
                    return response.GetBadRequest("No se ingresó información");
                }

                Guid idPerfil = lstPermisosElementos.IdPerfil;
                Response<Perfil> obtenerPerfiles = await datPerfil.Get(idPerfil);
                if (obtenerPerfiles.HasError)
                {
                    return response.GetResponse(obtenerPerfiles);
                }

                if (obtenerPerfiles.Result == null)
                {
                    return response.GetNotFound("El perfil no existe");
                }

                List<PermisoModuloModelo> lstModulos = lstPermisosElementos.Permisos.Select(x => new PermisoModuloModelo
                {
                    IdPermisoModulo = x.IdPermisoModulo,
                    IdModulo = x.IdModulo,
                    TienePermiso = x.TienePermiso

                }).ToList();

               List<PermisoPaginaModelo> lstPaginas = lstPermisosElementos.Permisos.SelectMany(x => x.PermisosPagina).Select(x => new PermisoPaginaModelo
                {
                    IdPermisoPagina = x.IdPermisoPagina,
                    IdPagina = x.IdPagina,
                    TienePermiso = x.TienePermiso
                }).ToList();

                List<PermisoBotonModelo> lstBotones = lstPermisosElementos.Permisos.SelectMany(x => x.PermisosPagina).SelectMany(x => x.PermisosBoton).Select(x => new PermisoBotonModelo
                {
                    IdPermisoBoton = x.IdPermisoBoton,
                    IdBoton = x.IdBoton,
                    TienePermiso = x.TienePermiso
                }).ToList();

                var lstModulosSinPermisosSinGuardar = lstModulos.Where(x=>x.IdPermisoModulo==Guid.Empty);

                if (lstModulosSinPermisosSinGuardar.Any())
                {
                    await datPermisos.GuardarPermisosModulos(lstModulosSinPermisosSinGuardar, idPerfil);
                }

                var lstModulosPermisosGuardados = lstModulos.Where(x => x.IdPermisoModulo != Guid.Empty);

                if (lstModulosPermisosGuardados.Any())
                {
                    await datPermisos.ActualizarPermisosModulos(lstModulosPermisosGuardados, idPerfil);
                }

                var lstPaginasSinPermisosSinGuardar = lstPaginas.Where(x => x.IdPermisoPagina == Guid.Empty);

                if (lstPaginasSinPermisosSinGuardar.Any())
                {
                    await datPermisos.GuardarPermisoPaginas(lstPaginasSinPermisosSinGuardar, idPerfil);
                }

                var lstPaginasPermisosGuardados = lstPaginas.Where(x => x.IdPermisoPagina != Guid.Empty);

                if (lstPaginasPermisosGuardados.Any())
                {
                    await datPermisos.ActualizarPermisosPaginas(lstPaginasPermisosGuardados, idPerfil);
                }

                var lstBotonesSinPermisosSinGuardar = lstBotones.Where(x => x.IdPermisoBoton == Guid.Empty);

                if (lstBotonesSinPermisosSinGuardar.Any())
                {
                    await datPermisos.GuardarPermisoBotones(lstBotonesSinPermisosSinGuardar, idPerfil);
                }

                var lstBotonesPermisosGuardados = lstBotones.Where(x => x.IdPermisoBoton != Guid.Empty);

                if (lstBotonesPermisosGuardados.Any())
                {
                    await datPermisos.ActualizarPermisosBotones(lstBotonesPermisosGuardados, idPerfil);
                }

                response.SetSuccess(true, "Se han guardado los permisos");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(GuardarPermisos));
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<object>> ObtenerPermisosMenu(Guid idPerfil)
        {
            Response<object> response = new();
            try
            {
                Response<Perfil> obtenerPerfiles = await datPerfil.Get(idPerfil);
                if (obtenerPerfiles.HasError)
                {
                    return response.GetResponse(obtenerPerfiles);
                }

                if (obtenerPerfiles.Result == null)
                {
                    return response.GetNotFound("El perfil no existe");
                }

                Response<List<PerfilPermisoMenuModelo>> obtnerPermisos = await datPermisos.GetPermisosMenu(idPerfil);
                if (obtnerPermisos.HasError)
                {
                    return response.GetResponse(obtnerPermisos);
                }

                JObject modulosObject = new();
                JObject routeMap = new();
                foreach (var modulo in obtnerPermisos.Result)
                {
                    JObject paginasObject = new();
                    if (modulo.Paginas is not null)
                    {

                        foreach (var pagina in modulo.Paginas)
                        {
                            string route = modulo.PathModulo + pagina.sPathPagina;

                            paginasObject[pagina.sClavePagina] = new JObject
                            {
                                ["nombre"] = pagina.sNombrePagina,
                                ["path"] = route,
                                ["mostrar"] = pagina.bMostrarEnMenu
                            };

                            routeMap[pagina.sClavePagina] = route;
                        }

                    }

                    modulosObject[modulo.ClaveModulo] = new JObject
                    {
                        ["nombre"] = modulo.NombreModulo,
                        ["mostrar"] = modulo.MostrarModuloEnMenu,
                        ["_paginas"] = paginasObject
                    };
                }

                modulosObject["_paths"] = routeMap;

                object permisosMenu = modulosObject.ToObject<object>();
                response.SetSuccess(permisosMenu, "Se han consultado los permisos del menu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(ObtenerPermisosMenu));
                response.SetError(ex);
            }
            return response;
        }
    }
}
