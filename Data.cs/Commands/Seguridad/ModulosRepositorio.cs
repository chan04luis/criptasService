using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utils;

namespace Data.cs.Commands.Seguridad
{
    public class ModulosRepositorio:IModulosRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<ModulosRepositorio> _logger;
        public ModulosRepositorio(ApplicationDbContext dbContext, ILogger<ModulosRepositorio> _logger)
        {
            this.dbContext = dbContext;
            this._logger = _logger;
        }
        public async Task<Response<bool>> AnyExistKey(Guid pKey)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsKey = await dbContext.Modulo.AnyAsync(i => i.uIdModulo == pKey && i.bActivo == true);
                if (exitsKey)
                {
                    response.SetSuccess(exitsKey, "Modulo ya existente");
                }
                else
                {
                    response.SetError("No existe el modulo");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(AnyExistKey));
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<bool>> AnyExitNameAndKey(Modulo pEntity)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsName = await dbContext.Modulo.AnyAsync(i => (i.uIdModulo != pEntity.uIdModulo)
                                                                        && (i.sClaveModulo.Equals(pEntity.sClaveModulo)) && i.bActivo == true);

                if (exitsName)
                {
                    response.SetSuccess(exitsName, "Modulo ya existente");
                }
                else
                {
                    response.SetError("No existe el modulo");
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
                var exitsName = await dbContext.Modulo.AnyAsync(i => i.sClaveModulo.Equals(pName) && i.bActivo == true);
                if (exitsName)
                {
                    response.SetSuccess(exitsName, "Modulo ya existente");
                }
                else
                {
                    response.SetError("No existe el modulo");
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
                var entity = await dbContext.Modulo.FindAsync(iKey);

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
        public async Task<Response<Modulo>> Save(Modulo newItem)
        {
            Response<Modulo> response = new Response<Modulo>();

            try
            {
                newItem.uIdModulo = Guid.NewGuid();
                newItem.bActivo = true;

                dbContext.Modulo.Add(newItem);
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
        public async Task<Response<bool>> Update(Modulo entity)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var bEntity = await dbContext.Modulo.FindAsync(entity.uIdModulo);
                if (bEntity == null)
                {
                    response.SetError("El modulo no fue encontrado.");
                    return response;
                }

                bEntity.sNombreModulo = entity.sNombreModulo;
                bEntity.sClaveModulo = entity.sClaveModulo;
                bEntity.sPathModulo = entity.sPathModulo;
                bEntity.bMostrarEnMenu = entity.bMostrarEnMenu;
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
