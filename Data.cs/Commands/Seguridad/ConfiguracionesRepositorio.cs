using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Entities;
using Microsoft.EntityFrameworkCore;
using Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Commands.Seguridad
{
    public class ConfiguracionesRepositorio:IConfiguracionesRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        public ConfiguracionesRepositorio(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Response<List<ElementoSrcModelo>>> ObtenerElementosSistema()
        {
            Response<List<ElementoSrcModelo>> response = new();
            try
            {
                var elementos = (from mod in dbContext.Modulo
                                 join pag in dbContext.Pagina.Where(x => x.bActivo == true) on mod.uIdModulo equals pag.uIdModulo into moduloPagina
                                 from mp in moduloPagina.DefaultIfEmpty()
                                 join bot in dbContext.Boton.Where(x => x.bActivo == true) on mp.uIdPagina equals bot.uIdPagina into paginaBoton
                                 from pb in paginaBoton.DefaultIfEmpty()
                                 where mod.bActivo == true && mod.bBaja == false
                                 orderby mod.sPathModulo, mp.sPathPagina, pb.sNombreBoton

                                 select new
                                 {
                                     IdModulo = mod.uIdModulo,
                                     ClaveModulo = mod.sClaveModulo,
                                     NombreModulo = mod.sNombreModulo,
                                     PathModulo = mod.sPathModulo,
                                     MostrarModuloEnMenu = mod.bMostrarEnMenu,
                                     IdPagina = mp.uIdPagina,

                                     ClavePagina = mp.sClavePagina,
                                     NombrePagina = mp.sNombrePagina,
                                     PathPagina = mp.sPathPagina,
                                     MostrarPaginaEnMenu = mp.bMostrarEnMenu,
                                     IdBoton = pb.uIdBoton,
                                     ClaveBoton = pb.sClaveBoton,
                                     NombreBoton = pb.sNombreBoton
                                 }
                    ).ToListAsync();
                List<ElementoSrcModelo> lstElementos = elementos.Result.Select(o => new ElementoSrcModelo
                {
                    IdModulo = o.IdModulo,
                    ClaveModulo = o.ClaveModulo,
                    NombreModulo = o.NombreModulo,
                    PathModulo = o.PathModulo,
                    MostrarModuloEnMenu = o.MostrarModuloEnMenu,
                    IdPagina = o.IdPagina,
                    ClavePagina = o.ClavePagina,
                    NombrePagina = o.NombrePagina,
                    PathPagina = o.PathPagina,
                    MostrarPaginaEnMenu = o.MostrarPaginaEnMenu,
                    IdBoton = o.IdBoton,
                    ClaveBoton = o.ClaveBoton,
                    NombreBoton = o.NombreBoton
                }).ToList();

                response.SetSuccess(lstElementos);
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }
        public async Task<Response<Configuracion>> DSave(Configuracion newItem)
        {
            var response = new Response<Configuracion>();
            try
            {
                newItem.uIdConfiguracion = Guid.NewGuid();
                newItem.dtFechaCreacion = DateTime.Now;
                newItem.bActivo = true;

                dbContext.Configuracion.Add(newItem);

                int i = await dbContext.SaveChangesAsync();

                if (i == 0)
                {
                    return default;
                }

                response.SetSuccess(newItem);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<Configuracion>> DObtenerConfiguracion()
        {
            var response = new Response<Configuracion>();
            try
            {
                var configuracion = await dbContext.Configuracion.AsNoTracking()
                            .SingleOrDefaultAsync();
                response.SetSuccess(configuracion);

            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }
        public async Task<Response<bool>> DUpdate(Configuracion entity)
        {
            var response = new Response<bool>();
            bool success = false;
            try
            {
                entity.dtFechaModificacion = DateTime.Now;
                entity.uIdUsuarioModificacion = new Guid("006F2E2B-5C9F-DEFA-E063-0400000A4139");

                var entry = dbContext.Configuracion.Attach(entity);
                dbContext.Entry(entity).Property(x => x.sTituloNavegador).IsModified = true;
                dbContext.Entry(entity).Property(x => x.sTitulo).IsModified = true;
                dbContext.Entry(entity).Property(x => x.sMetaDescripcion).IsModified = true;
                dbContext.Entry(entity).Property(x => x.sColorPrimario).IsModified = true;
                dbContext.Entry(entity).Property(x => x.sColorSecundario).IsModified = true;
                dbContext.Entry(entity).Property(x => x.sContrastePrimario).IsModified = true;
                dbContext.Entry(entity).Property(x => x.sContrasteSecundario).IsModified = true;
                dbContext.Entry(entity).Property(x => x.sUrlFuente).IsModified = true;
                dbContext.Entry(entity).Property(x => x.sNombreFuente).IsModified = true;
                dbContext.Entry(entity).Property(x => x.sRutaImagenFondo).IsModified = true;
                dbContext.Entry(entity).Property(x => x.sRutaImagenLogo).IsModified = true;
                dbContext.Entry(entity).Property(x => x.sRutaImagenPortal).IsModified = true;
                bool IsModified = entry.Properties.Where(e => e.IsModified).Count() > 0;
                if (IsModified)
                {
                    int i = await dbContext.SaveChangesAsync();
                }

                success = true;
            }
            catch (Exception)
            {

                success = false;
            }
            response.SetSuccess(success);
            return response;
        }
    }
}
