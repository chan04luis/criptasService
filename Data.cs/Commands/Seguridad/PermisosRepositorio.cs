using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Seguridad;
using System.Linq;
using Utils;

namespace Data.cs.Commands.Seguridad
{
    public class PermisosRepositorio:IPermisosRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<PermisosRepositorio> _logger;

        public PermisosRepositorio(ApplicationDbContext dbContext, ILogger<PermisosRepositorio> _logger)
        {
            this.dbContext = dbContext;
            this._logger = _logger;
        }
        public async Task<Response<List<Modulo>>> GetPermisosElementos(Guid piIdPerfil)
        {

            Response<List<Modulo>> response = new();
            try
            {

                var lstPermisos = await dbContext.Modulo.Where(mod => mod.bActivo)
                .Include(pag => pag.lstPermisosModulos.Where(x=>x.uIdPerfil== piIdPerfil))
                .Include(x=>x.lstPaginas.Where(mod => mod.bActivo))
                .ThenInclude(x=>x.lstPermisosPaginas.Where(x => x.uIdPerfil == piIdPerfil))
                .Include(x => x.lstPaginas.Where(mod => mod.bActivo))
                .ThenInclude(x=>x.lstBotones.Where(x=>x.bActivo))
                .ThenInclude(x=>x.lstPermisosBotones.Where(x => x.uIdPerfil == piIdPerfil))
                .ToListAsync();

                if (lstPermisos.Count > 0)
                {
                    response.SetSuccess(lstPermisos);
                }
                else
                {
                    response.SetError("Sin registros");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(GetPermisosElementos));
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<bool>> GuardarPermisosModulos(IEnumerable<PermisoModuloModelo> permisosModulosModelo, Guid idPerfil)
        {
            var response = new Response<bool>();
            try
            {
                var permisosModulos = permisosModulosModelo.Select(modelo => new PermisoModulos
                {
                    uIdPermisoModulo = Guid.NewGuid(),
                    uIdModulo = modelo.IdModulo,
                    bTienePermiso = modelo.TienePermiso,
                    uIdPerfil = idPerfil,
                    dtFechaCreacion = DateTime.Now,
                    bActivo = true
                }).ToList();

                dbContext.PermisosModulos.AddRange(permisosModulos);

                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                {
                    response.SetSuccess(true, "Agregados correctamente");
                }
                else
                {
                    response.SetError("Registros no agregados");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(GuardarPermisosModulos));
                response.SetError("Ocurrió un error en la base de datos al guardar los permisos");
            }
            return response;
        }

        public async Task<Response<bool>> ActualizarPermisosModulos(IEnumerable<PermisoModuloModelo> permisosModulosModelo, Guid idPerfil)
        {
            var response = new Response<bool>();
            try
            {
                var permisosModeloDiccionario = permisosModulosModelo.ToDictionary(modelo => modelo.IdPermisoModulo, modelo => modelo.TienePermiso);

                var ids = permisosModeloDiccionario.Keys;

                var permisos = await dbContext.PermisosModulos.Where(pm => ids.Contains(pm.uIdPermisoModulo)).ToListAsync();

                permisos.ForEach(p =>
                    p.bTienePermiso = permisosModeloDiccionario.TryGetValue(p.uIdPermisoModulo, out var tienePermiso) ? tienePermiso : p.bTienePermiso
                );

                dbContext.PermisosModulos.UpdateRange(permisos);

                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                {
                    response.SetSuccess(true, "Permisos de modulos actualizados correctamente");
                }
                else
                {
                    response.SetError("No se actualizaron los permisos");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(ActualizarPermisosModulos));
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<bool>> GuardarPermisoPaginas(IEnumerable<PermisoPaginaModelo> permisosPaginaModelo, Guid idPerfil)
        {
            var response = new Response<bool>();
            try
            {
                var permisosPaginas = permisosPaginaModelo.Select(modelo => new PermisosPagina
                {
                    uIdPermisoPagina = Guid.NewGuid(),
                    uIdPagina=modelo.IdPagina,
                    bTienePermiso = modelo.TienePermiso,
                    uIdPerfil = idPerfil,
                    dtFechaCreacion = DateTime.Now,
                    bActivo = true
                }).ToList();

                dbContext.PermisosPagina.AddRange(permisosPaginas);

                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                {
                    response.SetSuccess(true, "Permisos paginas agregados correctamente");
                }
                else
                {
                    response.SetError("Permisos paginas no agregados");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(GuardarPermisoPaginas));
                response.SetError("Ocurrió un error en la base de datos al guardar los permisos");
            }
            return response;
        }

        public async Task<Response<bool>> ActualizarPermisosPaginas(IEnumerable<PermisoPaginaModelo> permisosPaginasModelo, Guid idPerfil)
        {
            var response = new Response<bool>();
            try
            {
                var permisosModeloDiccionario = permisosPaginasModelo.ToDictionary(modelo => modelo.IdPermisoPagina, modelo => modelo.TienePermiso);

                var ids = permisosModeloDiccionario.Keys;

                var permisos = await dbContext.PermisosPagina.Where(pm => ids.Contains(pm.uIdPermisoPagina)).ToListAsync();

                permisos.ForEach(p =>
                    p.bTienePermiso = permisosModeloDiccionario.TryGetValue(p.uIdPermisoPagina, out var tienePermiso) ? tienePermiso : p.bTienePermiso
                );

                dbContext.PermisosPagina.UpdateRange(permisos);

                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                {
                    response.SetSuccess(true, "Permisos de paginas actualizados correctamente");
                }
                else
                {
                    response.SetError("No se actualizaron los permisos");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(ActualizarPermisosPaginas));
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<bool>> GuardarPermisoBotones(IEnumerable<PermisoBotonModelo> permisosBotonModelo, Guid idPerfil)
        {
            var response = new Response<bool>();
            try
            {
                var permisosBotones = permisosBotonModelo.Select(modelo => new PermisoBotones
                {
                    uIdPermisoBoton = Guid.NewGuid(),
                    uIdBoton = modelo.IdBoton,
                    bTienePermiso = modelo.TienePermiso,
                    uIdPerfil = idPerfil,
                    dtFechaCreacion = DateTime.Now,
                    bActivo = true
                }).ToList();

                dbContext.PermisoBotones.AddRange(permisosBotones);

                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                {
                    response.SetSuccess(true, "Permisos botones agregados correctamente");
                }
                else
                {
                    response.SetError("Permisos botones no agregados");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(GuardarPermisoBotones));
                response.SetError("Ocurrió un error en la base de datos al guardar los permisos");
            }
            return response;
        }
        public async Task<Response<bool>> ActualizarPermisosBotones(IEnumerable<PermisoBotonModelo> permisosBotonesModelo, Guid idPerfil)
        {
            var response = new Response<bool>();
            try
            {
                var permisosModeloDiccionario = permisosBotonesModelo.ToDictionary(modelo => modelo.IdPermisoBoton, modelo => modelo.TienePermiso);

                var ids = permisosModeloDiccionario.Keys;

                var permisos = await dbContext.PermisoBotones.Where(pm => ids.Contains(pm.uIdPermisoBoton)).ToListAsync();

                permisos.ForEach(p =>
                    p.bTienePermiso = permisosModeloDiccionario.TryGetValue(p.uIdPermisoBoton, out var tienePermiso) ? tienePermiso : p.bTienePermiso
                );

                dbContext.PermisoBotones.UpdateRange(permisos);

                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                {
                    response.SetSuccess(true, "Permisos de botones actualizados correctamente");
                }
                else
                {
                    response.SetError("No se actualizaron los permisos");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(ActualizarPermisosBotones));
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<List<PerfilPermisoMenuModelo>>> GetPermisosMenu(Guid piIdPerfil)
        {

            Response<List<PerfilPermisoMenuModelo>> response = new();

            try
            {
                var lstelementos = await dbContext.PermisosModulos.Where(mod => mod.bActivo && mod.bTienePermiso && mod.uIdPerfil== piIdPerfil)
                 .Include(pag => pag.modulo)
                 .ThenInclude(x => x.lstPaginas.Where(mod => mod.bActivo))
                 .ThenInclude(x => x.lstPermisosPaginas.Where(x => x.uIdPerfil == piIdPerfil && x.bActivo && x.bTienePermiso))
                 .Select(o => new
                 {
                     iIdModulo = o.uIdModulo,
                     sClaveModulo = o.modulo.sClaveModulo,
                     sNombreModulo = o.modulo.sNombreModulo,
                     sPathModulo = o.modulo.sPathModulo,
                     bMostrarModuloEnMenu = o.modulo.bMostrarEnMenu,
                     iIdPagina =o.modulo.lstPaginas.FirstOrDefault().uIdPagina,
                     sClavePagina = o.modulo.lstPaginas.FirstOrDefault().sClavePagina,
                     sNombrePagina = o.modulo.lstPaginas.FirstOrDefault().sNombrePagina,
                     sPathPagina = o.modulo.lstPaginas.FirstOrDefault().sPathPagina,
                     bMostrarPaginaEnMenu = o.modulo.lstPaginas.FirstOrDefault().bMostrarEnMenu,
                 })
                 .ToListAsync();

                List<PerfilPermisoMenuModelo> lstPermisos = lstelementos.Select(o => new PerfilPermisoMenuModelo
                {
                    MostrarModuloEnMenu = o.bMostrarModuloEnMenu,
                    MostrarPaginaEnMenu = o.bMostrarPaginaEnMenu,
                    IdModulo = o.iIdModulo,
                    IdPagina = o.iIdPagina,
                    ClaveModulo = o.sClaveModulo,
                    ClavePagina = o.sClavePagina,
                    NombreModulo = o.sNombreModulo,
                    NombrePagina = o.sNombrePagina,
                    PathModulo = o.sPathModulo,
                    PathPagina = o.sPathPagina
                }).ToList();

                if (lstPermisos.Count > 0)
                {
                    response.SetSuccess(lstPermisos);
                }
                else
                {
                    response.SetError("Sin registros");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(GetPermisosMenu));
                response.SetError(ex.Message);
            }
            return response;
        }
    }
}
