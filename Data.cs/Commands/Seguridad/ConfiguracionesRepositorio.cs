using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Seguridad;
using Utils;

namespace Data.cs.Commands.Seguridad
{
    public class ConfiguracionesRepositorio:IConfiguracionesRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<ConfiguracionesRepositorio> _logger;
        public ConfiguracionesRepositorio(ApplicationDbContext dbContext, ILogger<ConfiguracionesRepositorio> _logger)
        {
            this.dbContext = dbContext;
            this._logger = _logger;
        }

        public async Task<Response<List<Modulo>>> ObtenerElementosSistema()
        {
            Response<List<Modulo>> response = new Response<List<Modulo>>();

            try
            {
                var lstElementos = await dbContext.Modulo.Where(y => y.bActivo)
                    .Include(x => x.lstPaginas.Where(y => y.bActivo))
                    .ThenInclude(x => x.lstBotones.Where(y => y.bActivo))
                    .ToListAsync();

                if (lstElementos.Count > 0)
                {
                    response.SetSuccess(lstElementos);
                }
                else
                {
                    response.SetError("Sin registros");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(ObtenerElementosSistema));
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<Configuracion>> DSave(Configuracion newItem)
        {
            Response<Configuracion> response = new Response<Configuracion>();

            try
            {
                newItem.uIdConfiguracion = Guid.NewGuid();
                newItem.dtFechaCreacion = DateTime.Now;
                newItem.bActivo = true;

                dbContext.Configuracion.Add(newItem);
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
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DSave));
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<Configuracion>> DObtenerConfiguracion()
        {
            Response<Configuracion> response = new Response<Configuracion>();

            try
            {
                var configuracion = await dbContext.Configuracion.AsNoTracking().SingleOrDefaultAsync();

                if (configuracion == null)
                {
                    response.SetSuccess(new Configuracion(), "No se encontraron resultados");
                }
                else
                {
                    response.SetSuccess(configuracion, "Consultado Correctamente");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DObtenerConfiguracion));
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<bool>> DUpdate(Configuracion entity)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var bEntity = await dbContext.Configuracion.FindAsync(entity.uIdConfiguracion);
                if (bEntity == null)
                {
                    response.SetError("La configuracion no fue encontrada.");
                    return response;
                }

                bEntity.sTituloNavegador = entity.sTituloNavegador;
                bEntity.sMetaDescripcion = entity.sMetaDescripcion;
                bEntity.sColorPrimario = entity.sColorPrimario;
                bEntity.sColorSecundario = entity.sColorSecundario;
                bEntity.sContrastePrimario = entity.sContrastePrimario;
                bEntity.sContrasteSecundario = entity.sContrasteSecundario;
                bEntity.sUrlFuente = entity.sUrlFuente;
                bEntity.sNombreFuente = entity.sNombreFuente;
                bEntity.sRutaImagenFondo = entity.sRutaImagenFondo;
                bEntity.sRutaImagenLogo = entity.sRutaImagenLogo;
                bEntity.sRutaImagenPortal = entity.sRutaImagenPortal;
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
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DUpdate));
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<bool>> AnyExistKey(Guid pKey)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsKey = await dbContext.Configuracion.AnyAsync(i => i.uIdConfiguracion == pKey && i.bActivo == true);
                if (exitsKey)
                {
                    response.SetSuccess(exitsKey, "Configuración ya existente");
                }
                else
                {
                    response.SetError("No existe la configuración");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(AnyExistKey));
                response.SetError(ex.Message);
            }
            return response;
        }
    }
}
