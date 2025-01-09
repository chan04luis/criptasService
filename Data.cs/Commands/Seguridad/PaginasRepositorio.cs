using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utils;

namespace Data.cs.Commands.Seguridad
{
    public class PaginasRepositorio:IPaginasRespositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<PaginasRepositorio> _logger;
        public PaginasRepositorio(ApplicationDbContext dbContext, ILogger<PaginasRepositorio> _logger)
        {
            this.dbContext = dbContext;
            this._logger = _logger;
        }
        public async Task<Response<bool>> AnyExistKey(Guid pKey)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsKey = await dbContext.Pagina.AnyAsync(i => i.uIdPagina == pKey && i.bActivo == true);
                if (exitsKey)
                {
                    response.SetSuccess(exitsKey, "Pagina ya existente");
                }
                else
                {
                    response.SetError("No existe la pagina");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(AnyExistKey));
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<bool>> AnyExitNameAndKey(Pagina pEntity)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsName = await dbContext.Pagina.AnyAsync(i => (i.uIdPagina != pEntity.uIdPagina)
                                                                        && (i.sNombrePagina.Equals(pEntity.sNombrePagina)) && i.bActivo == true);

                if (exitsName)
                {
                    response.SetSuccess(exitsName, "Pefiles ya existente");
                }
                else
                {
                    response.SetError("No existe la pagina");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(AnyExitNameAndKey));
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<bool>> AnyExitName(string pName)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsName = await dbContext.Pagina.AnyAsync(i => i.sNombrePagina.Equals(pName) && i.bActivo == true);
                if (exitsName)
                {
                    response.SetSuccess(exitsName, "Pagina ya existente");
                }
                else
                {
                    response.SetError("No existe la pagina");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(AnyExitName));
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<bool>> Delete(Guid iKey)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var entity = await dbContext.Pagina.FindAsync(iKey);

                entity.bActivo = false;

                dbContext.Update(entity);

                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                {
                    response.SetSuccess(true, "Eliminado correctamente");
                }
                else
                {
                    response.SetError("Registro no eliminado");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(Delete));
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<List<Pagina>>> GetAll()
        {
            Response<List<Pagina>> response = new Response<List<Pagina>>();

            try
            {
                List<Pagina> result = await dbContext.Pagina.AsNoTracking().Where(i => i.bActivo == true).OrderBy(i => i.sNombrePagina).ToListAsync();

                if (result.Count > 0)
                {
                    response.SetSuccess(result);
                }
                else
                {
                    response.SetError("Sin registros");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(GetAll));
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<Pagina>> Get(Guid iKey)
        {
            Response<Pagina> response = new Response<Pagina>();

            try
            {
                var result = await dbContext.Pagina.AsNoTracking().FirstOrDefaultAsync(i => i.uIdPagina == iKey && i.bActivo == true);

                if (result == null)
                {
                    response.SetSuccess(new Pagina(), "No se encontraron resultados");
                }
                else
                {
                    response.SetSuccess(result, "Consultado Correctamente");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(Get));
                response.SetError(ex.Message);
            }
            return response;

        }
        public async Task<Response<Pagina>> Save(Pagina newItem)
        {
            Response<Pagina> response = new Response<Pagina>();

            try
            {
                newItem.uIdPagina = Guid.NewGuid();
                newItem.bActivo = true;

                dbContext.Pagina.Add(newItem);
                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                {
                    response.SetSuccess(newItem, "Agregado correctamente");
                }
                else
                {
                    response.SetError("Registro no agregado");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(Save));
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<bool>> Update(Pagina entity)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var bEntity = await dbContext.Pagina.FindAsync(entity.uIdPagina);
                if (bEntity == null)
                {
                    response.SetError("La pagina no fue encontrada.");
                    return response;
                }

                bEntity.sNombrePagina = entity.sNombrePagina;
                bEntity.sClavePagina = entity.sClavePagina;
                dbContext.Update(bEntity);

                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                {
                    response.SetSuccess(true, "Actualizado correctamente");
                }

                else
                {
                    response.SetError("Registro no actualizado");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(Update));
                response.SetError(ex.Message);
            }
            return response;
        }
    }
}
