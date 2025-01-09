using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utils;

namespace Data.cs.Commands.Seguridad
{
    public class BotonesRepositorio:IBotonesRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<BotonesRepositorio> _logger;
        public BotonesRepositorio(ApplicationDbContext dbContext, ILogger<BotonesRepositorio> _logger)
        {
            this.dbContext = dbContext;
            this._logger = _logger;
        }
        public async Task<Response<bool>> AnyExistKey(Guid pKey)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsKey = await dbContext.Boton.AnyAsync(i => i.uIdBoton == pKey && i.bActivo == true);
                if (exitsKey)
                {
                    response.SetSuccess(exitsKey, "Boton ya existente");
                }
                else
                {
                    response.SetError("No existe el boton");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(AnyExistKey));
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<bool>> AnyExitNameAndKey(Boton pEntity)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsName = await dbContext.Boton.AnyAsync(i => (i.uIdBoton != pEntity.uIdBoton)
                                                                        && (i.sClaveBoton.Equals(pEntity.sClaveBoton)) && i.bActivo == true);

                if (exitsName)
                {
                    response.SetSuccess(exitsName, "Boton ya existente");
                }
                else
                {
                    response.SetError("No existe el boton");
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
                var exitsClave = await dbContext.Boton.AnyAsync(i => i.sClaveBoton.Equals(pName) && i.bActivo == true);
                if (exitsClave)
                {
                    response.SetSuccess(exitsClave, "Boton ya existente");
                }
                else
                {
                    response.SetError("No existe el boton");
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
                var entity = await dbContext.Boton.FindAsync(iKey);

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
        public async Task<Response<Boton>> Save(Boton newItem)
        {
            Response<Boton> response = new Response<Boton>();

            try
            {
                newItem.uIdBoton = Guid.NewGuid();
                newItem.bActivo = true;

                dbContext.Boton.Add(newItem);
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
        public async Task<Response<bool>> Update(Boton entity)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var bEntity = await dbContext.Boton.FindAsync(entity.uIdBoton);
                if (bEntity == null)
                {
                    response.SetError("El boton no fue encontrado.");
                    return response;
                }

                bEntity.sNombreBoton = entity.sNombreBoton;
                bEntity.sClaveBoton = entity.sClaveBoton;
                bEntity.uIdPagina = entity.uIdPagina;

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
