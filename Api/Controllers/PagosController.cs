using Business.Interfaces;
using Entities;
using Entities.Models;
using Entities.Request.Pagos;
using Entities.Request.TipoPagos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [Route("api/Pagos")]
    [Authorize]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IBusTiposPago _busTiposPago;
        private readonly IBusPagos _busPagos;

        private readonly ILogger<PagosController> _logger;

        public PagosController(IBusTiposPago busTiposPago, ILogger<PagosController> logger, IBusPagos busPagos)
        {
            _busTiposPago = busTiposPago;
            _logger = logger;
            _busPagos = busPagos;
        }

        #region PAGOS
        [HttpPost("Create")]
        [SwaggerOperation(Summary = "Crea un pago", Description = "Valida los datos y guarda un nuevo pago en la base de datos.")]
        public async Task<Response<EntPagos>> CreatePago([FromBody] EntPagosRequest pago)
        {
            _logger.LogInformation("Iniciando creación de pago.");
            var response = await _busPagos.ValidateAndSavePago(pago);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear pago: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Pago creado exitosamente con ID: {Id}", response.Result?.uId);
            }
            return response;
        }

        [HttpPut("Update")]
        [SwaggerOperation(Summary = "Actualiza un pago", Description = "Valida y actualiza los datos de un pago existente.")]
        public async Task<Response<EntPagos>> UpdatePago(Guid id, [FromBody] EntPagosRequest pago)
        {
            _logger.LogInformation("Iniciando actualización de pago con ID: {Id}", id);
            var response = await _busPagos.ValidateAndUpdatePago(id, pago);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar pago con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Pago actualizado exitosamente con ID: {Id}", id);
            }
            return response;
        }

        [HttpPut("AplicarPago")]
        [SwaggerOperation(Summary = "Aplica como pagado un pago", Description = "Proceso para generar el cierre de un pago.")]
        public async Task<Response<EntPagos>> UpdatePagado([FromBody] ReadPagosRequest pago)
        {
            _logger.LogInformation("Iniciando actualización de pago con ID: {Id}", pago.uIdPago);
            var response = await _busPagos.UpdatePagado(pago);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar pago con ID {Id}: {Error}", pago.uIdPago, response.Message);
            }
            else
            {
                _logger.LogInformation("Pago actualizado exitosamente con ID: {Id}", pago.uIdPago);
            }
            return response;
        }

        [HttpPut("CancelarPago/{uIdPago}")]
        [SwaggerOperation(Summary = "Aplica como pagado un pago", Description = "Proceso para generar el cierre de un pago.")]
        public async Task<Response<EntPagos>> UpdateCancelarPagado(Guid uIdPago)
        {
            _logger.LogInformation("Iniciando actualización de pago con ID: {Id}", uIdPago);
            var response = await _busPagos.UpdateCancelarPagado(uIdPago);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar pago con ID {Id}: {Error}", uIdPago, response.Message);
            }
            else
            {
                _logger.LogInformation("Pago actualizado exitosamente con ID: {Id}", uIdPago);
            }
            return response;
        }

        [HttpGet("Parcialidades/{id}")]
        [SwaggerOperation(Summary = "Obtiene parcialidades por ID de pago", Description = "Recupera un listado de pagos parciales específicando su ID de pago.")]
        public async Task<Response<List<EntPagosParciales>>> GetPagoParcialById(Guid id)
        {
            _logger.LogInformation("Iniciando búsqueda de pagos parciales con ID: {Id}", id);
            var response = await _busPagos.GetParcialidadesByIdPago(id);
            if (response.HasError)
            {
                _logger.LogWarning("Pagos no encontrados con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Pagos encontrados con ID: {Id}", id);
            }
            return response;
        }

        [HttpDelete("Parcialidades/{id}")]
        [SwaggerOperation(Summary = "Elimina un pago parcial por ID", Description = "Elimina un pago parcial específico utilizando su ID.")]
        public async Task<Response<bool>> DeletePagoById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminación de pago parcial con ID: {Id}", id);
            var response = await _busPagos.DeletePagoParcialById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Pago parcial no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Pago parcial eliminado con ID: {Id}", id);
            }
            return response;
        }

        [HttpPut("UpdateStatus")]
        [SwaggerOperation(Summary = "Actualiza el estado de un pago", Description = "Actualiza el estado booleano de un pago.")]
        public async Task<Response<EntPagos>> UpdatePagoStatus([FromBody] EntPagosUpdateEstatusRequest pago)
        {
            _logger.LogInformation("Iniciando actualización de estado para pago con ID: {Id}", pago.uId);
            var response = await _busPagos.UpdatePagoStatus(pago);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar estado de pago con ID {Id}: {Error}", pago.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Estado de pago actualizado exitosamente con ID: {Id}", pago.uId);
            }
            return response;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un pago por ID", Description = "Elimina un pago específico utilizando su ID.")]
        public async Task<Response<bool>> DeletePagoParcialById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminación de pago con ID: {Id}", id);
            var response = await _busPagos.DeletePagoById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Pago no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Pago eliminado con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un pago por ID", Description = "Recupera un pago específico utilizando su ID.")]
        public async Task<Response<EntPagos>> GetPagoById(Guid id)
        {
            _logger.LogInformation("Iniciando búsqueda de pago con ID: {Id}", id);
            var response = await _busPagos.GetPagoById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Pago no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Pago encontrado con ID: {Id}", id);
            }
            return response;
        }

        [HttpPost("ByFilters")]
        [SwaggerOperation(Summary = "Obtiene pagos por filtros", Description = "Recupera una lista de pagos que coincidan con los filtros proporcionados.")]
        public async Task<Response<List<EntPagos>>> GetPagosByFilters([FromBody] EntPagosSearchRequest filtros)
        {
            _logger.LogInformation("Iniciando búsqueda de pagos con los filtros: {filtros}", filtros);
            var response = await _busPagos.GetPagosByFilters(filtros);
            if (response.HasError)
            {
                _logger.LogWarning("No se encontraron pagos con los filtros {filtros}: {Error}", filtros, response.Message);
            }
            else
            {
                _logger.LogInformation("Pagos encontrados con los filtros: {filtros}", filtros);
            }
            return response;
        }
        #endregion

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
