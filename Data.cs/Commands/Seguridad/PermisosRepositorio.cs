using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.EntityFrameworkCore;
using Models.Seguridad;
using Utils;

namespace Data.cs.Commands.Seguridad
{
    public class PermisosRepositorio:IPermisosRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public PermisosRepositorio(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
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

                response.SetSuccess(lstPermisos);
            }
            catch (Exception ex)
            {
                response.SetError("Ocurrió un error en la base de datos al consultar los permisos");
            }
            return response;
        }

        public async Task<Response<bool>> GuardarPermisosModulos(Guid uIdModulo, Guid idPerfil, bool btienePermiso, Guid idUsuario)
        {
            var response = new Response<bool>();
            try
            {
                PermisoModulos permisosModulos = new PermisoModulos();
                permisosModulos.uIdPermisoModulo = Guid.NewGuid();
                permisosModulos.uIdModulo = uIdModulo;
                permisosModulos.bTienePermiso = btienePermiso;
                permisosModulos.uIdPerfil = idPerfil;

                permisosModulos.dtFechaCreacion = DateTime.Now;
                permisosModulos.uIdUsuarioModificacion = idUsuario;
                permisosModulos.bActivo = true;

                dbContext.PermisosModulos.Add(permisosModulos);

                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    return default;
                }
                response.SetSuccess(true);
            }
            catch (Exception ex)
            {
                response.SetError("Ocurrió un error en la base de datos al guardar los permisos");
            }
            return response;
        }

        public async Task<Response<bool>> ActualizarPermisosModulos(Guid idPermisoModulo, Guid uIdModulo, Guid idPerfil, bool btienePermiso, Guid idUsuario)
        {
            var response = new Response<bool>();
            try
            {
                PermisoModulos permisosModulos = new PermisoModulos();
                permisosModulos.uIdPermisoModulo = idPermisoModulo;
                permisosModulos.uIdModulo = uIdModulo;
                permisosModulos.bTienePermiso = btienePermiso;
                permisosModulos.uIdPerfil = idPerfil;
                permisosModulos.dtFechaModificacion = DateTime.Now;
                permisosModulos.uIdUsuarioModificacion = idUsuario;
                permisosModulos.bActivo = true;

                var entry = dbContext.PermisosModulos.Attach(permisosModulos);
                dbContext.Entry(permisosModulos).Property(x => x.bTienePermiso).IsModified = true;
                dbContext.Entry(permisosModulos).Property(x => x.dtFechaModificacion).IsModified = true;
                dbContext.Entry(permisosModulos).Property(x => x.uIdUsuarioModificacion).IsModified = true;

                bool IsModified = entry.Properties.Where(e => e.IsModified).Count() > 0;
                if (IsModified)
                {
                    int i = await dbContext.SaveChangesAsync();
                }

                response.SetSuccess(true);
            }
            catch (Exception ex)
            {
                response.SetError("Ocurrió un error en la base de datos al guardar los permisos");
            }
            return response;
        }

        public async Task<Response<bool>> GuardarPermisoPaginas(Guid uIdPagina, Guid idPerfil, bool btienePermiso, Guid idUsuario)
        {
            var response = new Response<bool>();
            try
            {
                PermisosPagina permisosPagina = new PermisosPagina();
                permisosPagina.uIdPermisoPagina = Guid.NewGuid();
                permisosPagina.uIdPagina = uIdPagina;
                permisosPagina.bTienePermiso = btienePermiso;
                permisosPagina.uIdPerfil = idPerfil;
                permisosPagina.dtFechaCreacion = DateTime.Now;
                permisosPagina.uIdUsuarioModificacion = idUsuario;
                permisosPagina.bActivo = true;

                dbContext.PermisosPagina.Add(permisosPagina);

                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    return default;
                }
                response.SetSuccess(true);
            }
            catch (Exception ex)
            {
                response.SetError("Ocurrió un error en la base de datos al guardar los permisos");
            }
            return response;
        }

        public async Task<Response<bool>> ActualizarPermisosPaginas(Guid idPermisoPagina, Guid uIdPagina, Guid idPerfil, bool btienePermiso, Guid idUsuario)
        {
            var response = new Response<bool>();
            try
            {
                PermisosPagina permisosPagina = new PermisosPagina();
                permisosPagina.uIdPermisoPagina = idPermisoPagina;
                permisosPagina.uIdPagina = uIdPagina;
                permisosPagina.bTienePermiso = btienePermiso;
                permisosPagina.uIdPerfil = idPerfil;
                permisosPagina.dtFechaModificacion = DateTime.Now;
                permisosPagina.uIdUsuarioModificacion = idUsuario;
                permisosPagina.bActivo = true;

                var entry = dbContext.PermisosPagina.Attach(permisosPagina);
                dbContext.Entry(permisosPagina).Property(x => x.bTienePermiso).IsModified = true;
                dbContext.Entry(permisosPagina).Property(x => x.dtFechaModificacion).IsModified = true;
                dbContext.Entry(permisosPagina).Property(x => x.uIdUsuarioModificacion).IsModified = true;

                bool IsModified = entry.Properties.Where(e => e.IsModified).Count() > 0;
                if (IsModified)
                {
                    int i = await dbContext.SaveChangesAsync();
                }

                response.SetSuccess(true);
            }
            catch (Exception ex)
            {
                response.SetError("Ocurrió un error en la base de datos al guardar los permisos");
            }
            return response;
        }

        public async Task<Response<bool>> GuardarPermisoBotones(Guid uIdBoton, Guid idPerfil, bool? btienePermiso, Guid idUsuario)
        {
            var response = new Response<bool>();
            try
            {
                PermisoBotones permisoBotones = new PermisoBotones();
                permisoBotones.uIdPermisoBoton = Guid.NewGuid();
                permisoBotones.uIdBoton = uIdBoton;
                permisoBotones.bTienePermiso = btienePermiso;
                permisoBotones.uIdPerfil = idPerfil;
                permisoBotones.dtFechaCreacion = DateTime.Now;
                permisoBotones.uIdUsuarioModificacion = idUsuario;
                permisoBotones.bActivo = true;

                dbContext.PermisoBotones.Add(permisoBotones);

                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    return default;
                }
                response.SetSuccess(true);
            }
            catch (Exception ex)
            {
                response.SetError("Ocurrió un error en la base de datos al guardar los permisos");
            }
            return response;
        }

        public async Task<Response<bool>> ActualizarPermisosBotones(Guid idPermisoBoton, Guid uIdBoton, Guid idPerfil, bool? btienePermiso, Guid idUsuario)
        {
            var response = new Response<bool>();
            try
            {
                PermisoBotones permisoBotones = new PermisoBotones();
                permisoBotones.uIdPermisoBoton = idPermisoBoton;
                permisoBotones.uIdBoton = uIdBoton;
                permisoBotones.bTienePermiso = btienePermiso;
                permisoBotones.uIdPerfil = idPerfil;

                permisoBotones.dtFechaModificacion = DateTime.Now;
                permisoBotones.uIdUsuarioModificacion = idUsuario;
                permisoBotones.bActivo = true;

                var entry = dbContext.PermisoBotones.Attach(permisoBotones);
                dbContext.Entry(permisoBotones).Property(x => x.bTienePermiso).IsModified = true;
                dbContext.Entry(permisoBotones).Property(x => x.dtFechaModificacion).IsModified = true;
                dbContext.Entry(permisoBotones).Property(x => x.uIdUsuarioModificacion).IsModified = true;

                bool IsModified = entry.Properties.Where(e => e.IsModified).Count() > 0;
                if (IsModified)
                {
                    int i = await dbContext.SaveChangesAsync();
                }

                response.SetSuccess(true);
            }
            catch (Exception ex)
            {
                response.SetError("Ocurrió un error en la base de datos al guardar los permisos");
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

                response.SetSuccess(lstPermisos);
            }
            catch (Exception ex)
            {
                response.SetError("Ocurrió un error en la base de datos al consultar los permisos");
            }
            return response;
        }
    }
}
