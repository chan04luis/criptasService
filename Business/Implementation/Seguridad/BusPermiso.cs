using AutoMapper;
using Business.Interfaces.Seguridad;
using Data.cs.Commands.Seguridad;
using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Entities;
using Microsoft.AspNetCore.Http;
using Modelos.Seguridad;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                Response<List<PermisoElementoSrcModelo>> obtenerElementos = await datPermisos.GetPermisosElementos(idPerfil);
                if (obtenerElementos.HasError)
                {
                    return response.GetResponse(obtenerElementos);
                }

                List<PermisoModuloModelo> lstElementos = obtenerElementos.Result
                    .GroupBy(x => x.IdModulo)
                    .Select(x => new PermisoModuloModelo
                    {
                        idPermisoModulo = x.First().idPermisoModulo,
                        idModulo = x.Key,
                        claveModulo = x.FirstOrDefault().ClaveModulo,
                        nombreModulo = x.FirstOrDefault().NombreModulo,
                        tienePermiso = x.FirstOrDefault().TienePermisoModulo,
                        permisosPagina = x
                            .GroupBy(y => y.IdPagina)
                            .Select(y => new PermisoPaginaModelo
                            {
                                idPermisoPagina = y.FirstOrDefault().idPermisoPagina,
                                idPagina = y.Key ?? Guid.Empty,
                                clavePagina = y.FirstOrDefault().ClavePagina,
                                nombrePagina = y.FirstOrDefault().NombrePagina,
                                tienePermiso = y.FirstOrDefault().TienePermisoPagina,
                                permisosBoton = y
                                    .GroupBy(z => z.IdBoton)
                                    .Select(z => new PermisoBotonModelo
                                    {
                                        idPermisoBoton = z.FirstOrDefault().idPermisoBoton,
                                        idBoton = z.Key ?? Guid.Empty,
                                        claveBoton = z.FirstOrDefault().ClaveBoton,
                                        nombreBoton = z.FirstOrDefault().NombreBoton,
                                        tienePermiso = z.FirstOrDefault().TienePermisoBoton
                                    })
                                    .Where(z => z.idBoton != Guid.Empty)
                                    .ToList()
                            })
                            .Where(y => y.idPagina != Guid.Empty)
                            .ToList()
                    })
                    .Where(x => x.idModulo != Guid.Empty)
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
        public async Task<Response<bool>> GuardarPermisos(IFormCollection form)
        {
            Response<bool> response = new();
            try
            {

                GuardarPermisosModelo lstPermisosElementos = JsonConvert.DeserializeObject<GuardarPermisosModelo>(form["body"]);
                if (lstPermisosElementos.permisos is null)
                {
                    return response.GetBadRequest("No se ingresó información");
                }

                Guid idUsuario = new Guid();
                Guid idPerfil = lstPermisosElementos.idPerfil;
                Response<Perfil> obtenerPerfiles = await datPerfil.Get(idPerfil);
                if (obtenerPerfiles.HasError)
                {
                    return response.GetResponse(obtenerPerfiles);
                }

                if (obtenerPerfiles.Result == null)
                {
                    return response.GetNotFound("El perfil no existe");
                }

                List<PermisoModuloModelo> lstModulos = lstPermisosElementos.permisos.Select(x => new PermisoModuloModelo
                {
                    idPermisoModulo = x.idPermisoModulo,
                    idModulo = x.idModulo,
                    tienePermiso = x.tienePermiso

                }).ToList();

                List<PermisoPaginaModelo> lstPaginas = lstPermisosElementos.permisos.SelectMany(x => x.permisosPagina).Select(x => new PermisoPaginaModelo
                {
                    idPermisoPagina = x.idPermisoPagina,
                    idPagina = x.idPagina,
                    tienePermiso = x.tienePermiso
                }).ToList();

                List<PermisoBotonModelo> lstBotones = lstPermisosElementos.permisos.SelectMany(x => x.permisosPagina).SelectMany(x => x.permisosBoton).Select(x => new PermisoBotonModelo
                {
                    idPermisoBoton = x.idPermisoBoton,
                    idBoton = x.idBoton,
                    tienePermiso = x.tienePermiso
                }).ToList();

                foreach (PermisoModuloModelo modulo in lstModulos)
                {
                    if (modulo.idPermisoModulo == Guid.Empty)
                    {
                        await datPermisos.GuardarPermisosModulos(modulo.idModulo, idPerfil, modulo.tienePermiso, idUsuario);
                    }
                    else
                    {
                        await datPermisos.ActualizarPermisosModulos(modulo.idPermisoModulo, modulo.idModulo, idPerfil, modulo.tienePermiso, idUsuario);

                    }
                }

                foreach (PermisoPaginaModelo pagina in lstPaginas)
                {
                    if (pagina.idPermisoPagina == Guid.Empty)
                    {
                        await datPermisos.GuardarPermisoPaginas(pagina.idPagina, idPerfil, pagina.tienePermiso, idUsuario);
                    }
                    else
                    {
                        await datPermisos.ActualizarPermisosPaginas(pagina.idPermisoPagina, pagina.idPagina, idPerfil, pagina.tienePermiso, idUsuario);
                    }
                }

                foreach (PermisoBotonModelo boton in lstBotones)
                {
                    if (boton.idPermisoBoton == Guid.Empty)
                    {
                        await datPermisos.GuardarPermisoBotones(boton.idBoton, idPerfil, boton.tienePermiso, idUsuario);
                    }
                    else
                    {
                        await datPermisos.ActualizarPermisosBotones(boton.idPermisoBoton, boton.idBoton, idPerfil, boton.tienePermiso, idUsuario);
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
