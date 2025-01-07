using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.EntityFrameworkCore;
using Models.Seguridad;
using Utils;

namespace Data.cs.Commands.Seguridad
{
    public class ConfiguracionesRepositorio:IConfiguracionesRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        public ConfiguracionesRepositorio(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Response<List<Modulo>>> ObtenerElementosSistema()
        {
            Response<List<Modulo>> response = new();
            try
            {
                var lstElementos = await dbContext.Modulo.Where(y=>y.bActivo)
                    .Include(x=>x.lstPaginas.Where(y=>y.bActivo))
                    .ThenInclude(x=>x.lstBotones.Where(y=>y.bActivo))
                    .ToListAsync();

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
