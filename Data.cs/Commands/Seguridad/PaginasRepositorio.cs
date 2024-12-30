using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace Data.cs.Commands.Seguridad
{
    public class PaginasRepositorio:IPaginasRespositorio
    {
        private readonly ApplicationDbContext dbContext;
        public PaginasRepositorio(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Response<Pagina>> DSave(Pagina newItem)
        {
            var response = new Response<Pagina>();
            try
            {
                newItem.uIdPagina = Guid.NewGuid();
                newItem.bActivo = true;

                dbContext.Pagina.Add(newItem);

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
                var pagina = await DGet(iKey);

                if (pagina != null)
                {
                    Pagina entPagina = new()
                    {
                        uIdPagina = iKey,
                        bActivo = false
                    };

                    var entry = dbContext.Pagina.Attach(entPagina);
                    dbContext.Entry(entPagina).Property(x => x.bActivo).IsModified = true;

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

        public async Task<Response<List<Pagina>>> DGet()
        {
            var response = new Response<List<Pagina>>();
            try
            {
                var listaPaginas = await dbContext.Pagina.AsNoTracking().Where(x => x.bActivo == true).OrderBy(x => x.sNombrePagina).ToListAsync();
                response.SetSuccess(listaPaginas);
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        public async Task<Response<Pagina>> DGet(Guid iKey)
        {
            var response = new Response<Pagina>();
            try
            {
                var pagina = await dbContext.Pagina.AsNoTracking().SingleOrDefaultAsync(u => u.uIdPagina == iKey);
                response.SetSuccess(pagina);

            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }

        public async Task<Response<bool>> DUpdate(Pagina entity)
        {
            var response = new Response<bool>();
            bool success = false;
            try
            {
                var pagina = await DGet(entity.uIdPagina);

                if (pagina != null)
                {

                    entity.bActivo = true;

                    var entry = dbContext.Pagina.Attach(entity);

                    dbContext.Entry(entity).Property(x => x.sClavePagina).IsModified = true;
                    dbContext.Entry(entity).Property(x => x.sNombrePagina).IsModified = true;
                    dbContext.Entry(entity).Property(x => x.sPathPagina).IsModified = true;
                    dbContext.Entry(entity).Property(x => x.bMostrarEnMenu).IsModified = true;

                    bool IsModified = entry.Properties.Where(e => e.IsModified).Count() > 0;
                    if (IsModified)
                    {
                        int i = await dbContext.SaveChangesAsync();
                    }
                }
                response.SetSuccess(success);
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
