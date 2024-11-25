using Business.Interfaces;
using Entities;
using Entities.Models;
using Entities.Request.TipoPagos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [Route("api/Pagos")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IBusTiposPago _busTiposPago;
        private readonly ILogger<PagosController> _logger;

        public PagosController(IBusTiposPago busTiposPago, ILogger<PagosController> logger)
        {
            _busTiposPago = busTiposPago;
            _logger = logger;
        }

        #region TIPOS DE PAGOS
        [HttpPost("Tipos/Create")]
        [SwaggerOperation(Summary = "Crea un tipo de pago", Description = "Valida los datos y guarda un nuevo tipo de pago.")]
        public async Task<Response<EntTiposPago>> CreateTipoPago([FromBody] EntTiposPagoRequest tipoPago)
        {
            _logger.LogInformation("Iniciando creación de tipo de pago.");
            var response = await _busTiposPago.ValidateAndSaveTipoPago(tipoPago);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear tipo de pago: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Tipo de pago creado exitosamente con ID: {Id}", response.Result?.uId);
            }
            return response;
        }

        [HttpPut("Tipos/Update")]
        [SwaggerOperation(Summary = "Actualiza un tipo de pago", Description = "Valida y actualiza los datos de un tipo de pago existente.")]
        public async Task<Response<EntTiposPago>> UpdateTipoPago(Guid id, [FromBody] EntTiposPagoRequest tipoPago)
        {
            _logger.LogInformation("Iniciando actualización de tipo de pago con ID: {Id}", id);
            var response = await _busTiposPago.ValidateAndUpdateTipoPago(id, tipoPago);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar tipo de pago con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Tipo de pago actualizado exitosamente con ID: {Id}", id);
            }
            return response;
        }

        [HttpPut("Tipos/UpdateStatus")]
        [SwaggerOperation(Summary = "Actualiza el estado de un tipo de pago", Description = "Actualiza el estado booleano de un tipo de pago.")]
        public async Task<Response<EntTiposPago>> UpdateTipoPagoStatus(Guid id, [FromBody] bool bEstatus)
        {
            _logger.LogInformation("Iniciando actualización de estado para tipo de pago con ID: {Id}", id);
            var response = await _busTiposPago.UpdateTipoPagoStatus(id, bEstatus);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar estado de tipo de pago con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Estado de tipo de pago actualizado exitosamente con ID: {Id}", id);
            }
            return response;
        }

        [HttpDelete("Tipos/{id}")]
        [SwaggerOperation(Summary = "Elimina un tipo de pago por ID", Description = "Elimina un tipo de pago específico utilizando su ID.")]
        public async Task<Response<bool>> DeleteTipoPagoById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminación de tipo de pago con ID: {Id}", id);
            var response = await _busTiposPago.DeleteTipoPagoById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Tipo de pago no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Tipo de pago eliminado con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("Tipos/{id}")]
        [SwaggerOperation(Summary = "Obtiene un tipo de pago por ID", Description = "Recupera un tipo de pago específico utilizando su ID.")]
        public async Task<Response<EntTiposPago>> GetTipoPagoById(Guid id)
        {
            _logger.LogInformation("Iniciando búsqueda de tipo de pago con ID: {Id}", id);
            var response = await _busTiposPago.GetTipoPagoById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Tipo de pago no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Tipo de pago encontrado con ID: {Id}", id);
            }
            return response;
        }

        [HttpPost("Tipos/ByFilters")]
        [SwaggerOperation(Summary = "Obtiene tipos de pago por filtros", Description = "Recupera una lista de tipos de pago que coincidan con los filtros proporcionados.")]
        public async Task<Response<List<EntTiposPago>>> GetTiposPagoByFilters([FromBody] EntTiposPagoSearchRequest filtros)
        {
            _logger.LogInformation("Iniciando búsqueda de tipos de pago con los filtros: {filtros}", filtros);
            var response = await _busTiposPago.GetTiposPagoByFilters(filtros);
            if (response.HasError)
            {
                _logger.LogWarning("No se encontraron tipos de pago con los filtros {filtros}: {Error}", filtros, response.Message);
            }
            else
            {
                _logger.LogInformation("Tipos de pago encontrados con los filtros: {filtros}", filtros);
            }
            return response;
        }

        [HttpGet("Tipos")]
        [SwaggerOperation(Summary = "Obtiene tipos de pago", Description = "Recupera una lista de tipos de pago.")]
        public async Task<Response<List<EntTiposPago>>> GetTiposPago()
        {
            _logger.LogInformation("Iniciando búsqueda de tipos de pago");
            var response = await _busTiposPago.GetList();
            if (response.HasError)
            {
                _logger.LogWarning("No se encontraron tipos de pago", response.Message);
            }
            else
            {
                _logger.LogInformation("Tipos de pago encontrados");
            }
            return response;
        }
        #endregion
    }
}
