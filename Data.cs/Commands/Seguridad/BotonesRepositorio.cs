using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace Data.cs.Commands.Seguridad
{
    public class BotonesRepositorio:IBotonesRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        public BotonesRepositorio(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Response<Boton>> DSave(Boton newItem)
        {
            var response = new Response<Boton>();
            try
            {
                newItem.uIdBoton = Guid.NewGuid();
                newItem.bActivo = true;

                dbContext.Boton.Add(newItem);

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
        public async Task<Response<bool>> DDelete(Guid iKey)
        {
            var response = new Response<bool>();
            try
            {
                var boton = await DGet(iKey);

                if (boton != null)
                {
                    Boton entBoton = new()
                    {
                        uIdBoton = iKey,
                        bActivo = false
                    };

                    var entry = dbContext.Boton.Attach(entBoton);

                    dbContext.Entry(entBoton).Property(x => x.bActivo).IsModified = true;

                    bool IsModified = entry.Properties.Where(e => e.IsModified).Count() > 0;
                    if (IsModified)
                    {
                        int i = await dbContext.SaveChangesAsync();
                        if (i == 0)
                            response.SetError("Datos no guardados");
                        else
                            response.SetSuccess(true);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }
        public async Task<Response<List<Boton>>> DGet()
        {
            var response = new Response<List<Boton>>();
            try
            {
                var listaBotones = await dbContext.Boton.AsNoTracking().Where(x => x.bActivo == true).OrderBy(x => x.sNombreBoton).ToListAsync();
                response.SetSuccess(listaBotones);
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }
        public async Task<Response<Boton>> DGet(Guid iKey)
        {
            var response = new Response<Boton>();
            try
            {
                var boton = await dbContext.Boton.AsNoTracking()
                            .SingleOrDefaultAsync(u => u.uIdBoton == iKey);
                response.SetSuccess(boton);

            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }
        public async Task<Response<bool>> DUpdate(Boton entity)
        {
            var response = new Response<bool>();
            try
            {
                var boton = await DGet(entity.uIdBoton);

                if (boton != null)
                {
                    var entry = dbContext.Boton.Attach(entity);

                    dbContext.Entry(entity).Property(x => x.sClaveBoton).IsModified = true;
                    dbContext.Entry(entity).Property(x => x.sNombreBoton).IsModified = true;

                    bool IsModified = entry.Properties.Where(e => e.IsModified).Count() > 0;
                    if (IsModified)
                    {
                        int i = await dbContext.SaveChangesAsync();
                        if (i == 0)
                            response.SetError("Datos no actualizados");
                        else
                            response.SetSuccess(true);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
    }
}
