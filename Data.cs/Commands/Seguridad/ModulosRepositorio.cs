using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace Data.cs.Commands.Seguridad
{
    public class ModulosRepositorio:IModulosRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        public ModulosRepositorio(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Response<Modulo>> DSave(Modulo newItem)
        {
            var response = new Response<Modulo>();
            try
            {
                newItem.uIdModulo = Guid.NewGuid();
                newItem.bActivo = true;

                dbContext.Modulo.Add(newItem);

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
                var modulo = await DGet(iKey);

                if (modulo != null)
                {
                    Modulo entModulo = new()
                    {
                        uIdModulo = iKey,
                        bActivo = false

                    };

                    var entry = dbContext.Modulo.Attach(entModulo);
                    dbContext.Entry(entModulo).Property(x => x.bBaja).IsModified = true;
                    bool IsModified = entry.Properties.Where(e => e.IsModified).Count() > 0;
                    if (IsModified)
                    {
                        int i = await dbContext.SaveChangesAsync();
                        if (i == 0)
                            response.SetError("Datos no eliminados");
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

        public async Task<Response<List<Modulo>>> DGet()
        {
            var response = new Response<List<Modulo>>();
            try
            {
                var listaModulos = await dbContext.Modulo.AsNoTracking().Where(x => x.bActivo == true).OrderBy(x => x.sNombreModulo).ToListAsync();
                response.SetSuccess(listaModulos);
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        public async Task<Response<Modulo>> DGet(Guid iKey)
        {
            var response = new Response<Modulo>();
            try
            {
                var modulo = await dbContext.Modulo.AsNoTracking().SingleOrDefaultAsync(u => u.uIdModulo == iKey);
                response.SetSuccess(modulo);

            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }

        public async Task<Response<bool>> DUpdate(Modulo entity)
        {
            var response = new Response<bool>();
            bool success = false;
            try
            {
                var modulo = await DGet(entity.uIdModulo);

                if (modulo != null)
                {
                    var entry = dbContext.Modulo.Attach(entity);

                    dbContext.Entry(entity).Property(x => x.sClaveModulo).IsModified = true;
                    dbContext.Entry(entity).Property(x => x.sNombreModulo).IsModified = true;
                    dbContext.Entry(entity).Property(x => x.sPathModulo).IsModified = true;
                    dbContext.Entry(entity).Property(x => x.bMostrarEnMenu).IsModified = true;

                    bool IsModified = entry.Properties.Where(e => e.IsModified).Count() > 0;
                    if (IsModified)
                    {
                        int i = await dbContext.SaveChangesAsync();
                    }
                }

                response.SetSuccess(true);
            }
            catch (Exception)
            {

                throw;
            }
            response.SetSuccess(success);
            return response;
        }
    }
}
