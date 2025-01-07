using AutoMapper;
using Business.Interfaces.Seguridad;
using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.AspNetCore.Http;
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

        public BusPermiso(IMapper mapeador, IHttpContextAccessor httpContext, IPermisosRepositorio datPermisos, IBusPerfiles busPerfil, IPerfilesRepositorio datPerfil)
        {
            this.mapeador = mapeador;
            this.httpContext = httpContext;
            this.datPermisos = datPermisos;
            this.busPerfil = busPerfil;
            this.datPerfil = datPerfil;
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
                response.SetError("Ocurrió un error al consultar los permisos");
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

                Guid idUsuario = new Guid();
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

                foreach (PermisoModuloModelo modulo in lstModulos)
                {
                    if (modulo.IdPermisoModulo == Guid.Empty)
                    {
                        await datPermisos.GuardarPermisosModulos(modulo.IdModulo, idPerfil, modulo.TienePermiso, idUsuario);
                    }
                    else
                    {
                        await datPermisos.ActualizarPermisosModulos(modulo.IdPermisoModulo, modulo.IdModulo, idPerfil, modulo.TienePermiso, idUsuario);

                    }
                }

                foreach (PermisoPaginaModelo pagina in lstPaginas)
                {
                    if (pagina.IdPermisoPagina == Guid.Empty)
                    {
                        await datPermisos.GuardarPermisoPaginas(pagina.IdPagina, idPerfil, pagina.TienePermiso, idUsuario);
                    }
                    else
                    {
                        await datPermisos.ActualizarPermisosPaginas(pagina.IdPermisoPagina, pagina.IdPagina, idPerfil, pagina.TienePermiso, idUsuario);
                    }
                }

                foreach (PermisoBotonModelo boton in lstBotones)
                {
                    if (boton.IdPermisoBoton == Guid.Empty)
                    {
                        await datPermisos.GuardarPermisoBotones(boton.IdBoton, idPerfil, boton.TienePermiso, idUsuario);
                    }
                    else
                    {
                        await datPermisos.ActualizarPermisosBotones(boton.IdPermisoBoton, boton.IdBoton, idPerfil, boton.TienePermiso, idUsuario);
                    }
                }

                response.SetSuccess(true, "Se han guardado los permisos");
            }
            catch (Exception ex)
            {
                response.SetError("Ocurrió un error al guardar los permisos");
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

                var groupMenu = obtnerPermisos.Result.GroupBy(x => x.IdModulo).Select(x => new
                {
                    IdModulo = x.Key,
                    x.FirstOrDefault()?.ClaveModulo,
                    x.FirstOrDefault()?.NombreModulo,
                    x.FirstOrDefault()?.PathModulo,
                    x.FirstOrDefault()?.MostrarModuloEnMenu,
                    Paginas = x.Select(y => new
                    {
                        y.IdPagina,
                        y.ClavePagina,
                        y.NombrePagina,
                        y.PathPagina,
                        y.MostrarPaginaEnMenu
                    }).Where(c => c.IdPagina is not null)
                }); ;

                JObject modulosObject = new();
                JObject routeMap = new();
                foreach (var modulo in groupMenu)
                {
                    JObject paginasObject = new();
                    if (modulo.Paginas is not null)
                    {

                        foreach (var pagina in modulo.Paginas)
                        {
                            string route = modulo.PathModulo + pagina.PathPagina;

                            paginasObject[pagina.ClavePagina] = new JObject
                            {
                                ["nombre"] = pagina.NombrePagina,
                                ["path"] = route,
                                ["mostrar"] = pagina.MostrarPaginaEnMenu
                            };

                            routeMap[pagina.ClavePagina] = route;
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
                response.SetError("Ocurrió un error al consultar los permisos");
            }
            return response;
        }
    }
}
