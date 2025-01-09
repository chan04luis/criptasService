using Data.cs.Interfaces.Seguridad;
using Data.cs.Entities.Seguridad;
using Microsoft.EntityFrameworkCore;
using Utils;
using Microsoft.Extensions.Logging;
using Models.Models;

namespace Data.cs.Commands.Seguridad
{
    public class PerfilesRepositorio: IPerfilesRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<PerfilesRepositorio> _logger;
        public PerfilesRepositorio(ApplicationDbContext dbContext, ILogger<PerfilesRepositorio> _logger)
        {
            this.dbContext = dbContext;
            this._logger = _logger;
        }
        public async Task<Response<bool>> AnyExistKey(Guid pKey)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsKey = await dbContext.Perfiles.AnyAsync(i => i.id == pKey && i.Activo == true);
                if (exitsKey)
                {
                    response.SetSuccess(exitsKey, "Perfil ya existente");
                }
                else
                {
                    response.SetError("No existe el perfil");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(AnyExistKey));
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<bool>> AnyExitNameAndKey(Perfil pEntity)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsName = await dbContext.Perfiles.AnyAsync(i => (i.id != pEntity.id)
                                                                        && (i.NombrePerfil.Equals(pEntity.NombrePerfil)) && i.Activo == true);

                if (exitsName)
                {
                    response.SetSuccess(exitsName, "Pefiles ya existente");
                }
                else
                {
                    response.SetError("No existe el perfil");
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
                var exitsName = await dbContext.Perfiles.AnyAsync(i => i.NombrePerfil.Equals(pName) && i.Activo == true);
                if (exitsName)
                {
                    response.SetSuccess(exitsName, "Perfil ya existente");
                }
                else
                {
                    response.SetError("No existe el perfil");
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
                var entity = await dbContext.Perfiles.FindAsync(iKey);

                entity.Activo = false;

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
        public async Task<Response<List<Perfil>>> GetAll()
        {
            Response<List<Perfil>> response = new Response<List<Perfil>>();

            try
            {
                List<Perfil> result = await dbContext.Perfiles.AsNoTracking().Where(i => i.Activo == true).OrderBy(i => i.NombrePerfil).ToListAsync();

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
        public async Task<Response<Perfil>> Get(Guid iKey)
        {
            Response<Perfil> response = new Response<Perfil>();

            try
            {
                var result = await dbContext.Perfiles.AsNoTracking().FirstOrDefaultAsync(i => i.id == iKey && i.Activo == true);

                if (result == null)
                {
                    response.SetSuccess(new Perfil(), "No se encontraron resultados");
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
        public async Task<Response<Perfil>> Save(Perfil newItem)
        {
            Response<Perfil> response = new Response<Perfil>();

            try
            {
                newItem.id = Guid.NewGuid();
                newItem.Activo = true;

                dbContext.Perfiles.Add(newItem);
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
        public async Task<Response<bool>> Update(Perfil entity)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                dbContext.Attach(entity);
                dbContext.Entry(entity).Property(x => x.ClavePerfil).IsModified = true;
                dbContext.Entry(entity).Property(x => x.NombrePerfil).IsModified = true;

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
