using Business.Interfaces;
using Entities;
using Entities.Models;
using Entities.Request.Criptas;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [Route("api/Criptas")]
    [ApiController]
    public class CriptasController : ControllerBase
    {
        private readonly IBusCriptas _busCriptas;
        private readonly ILogger<CriptasController> _logger;

        public CriptasController(IBusCriptas busCriptas, ILogger<CriptasController> logger)
        {
            _busCriptas = busCriptas;
            _logger = logger;
        }

        [HttpPost("Create")]
        [SwaggerOperation(Summary = "Crea una cripta", Description = "Valida los datos y guarda una nueva cripta en la base de datos.")]
        public async Task<Response<EntCriptas>> CreateCripta([FromBody] EntCriptaRequest cripta)
        {
            _logger.LogInformation("Iniciando creación de cripta.");
            var response = await _busCriptas.ValidateAndSaveCripta(cripta);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear cripta: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Cripta creada exitosamente con ID: {Id}", response.Result?.uId);
            }
            return response;
        }

        [HttpPut("Update")]
        [SwaggerOperation(Summary = "Actualiza una cripta", Description = "Valida y actualiza los datos de una cripta existente.")]
        public async Task<Response<EntCriptas>> UpdateCripta([FromBody] EntCriptaUpdateRequest cripta)
        {
            _logger.LogInformation("Iniciando actualización de cripta con ID: {Id}", cripta.uId);
            var response = await _busCriptas.ValidateAndUpdateCripta(cripta);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar cripta con ID {Id}: {Error}", cripta.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Cripta actualizada exitosamente con ID: {Id}", cripta.uId);
            }
            return response;
        }

        [HttpPut("UpdateStatus")]
        [SwaggerOperation(Summary = "Actualiza el estado de una cripta", Description = "Actualiza el estado booleano de una cripta.")]
        public async Task<Response<EntCriptas>> UpdateCriptaStatus([FromBody] EntCriptaUpdateEstatusRequest cripta)
        {
            _logger.LogInformation("Iniciando actualización de estado para cripta con ID: {Id}", cripta.uId);
            var response = await _busCriptas.UpdateCriptaStatus(cripta);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar estado de cripta con ID {Id}: {Error}", cripta.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Estado de cripta actualizado exitosamente con ID: {Id}", cripta.uId);
            }
            return response;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina una cripta por ID", Description = "Elimina una cripta específica utilizando su ID.")]
        public async Task<Response<bool>> DeleteCriptaById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminado de cripta con ID: {Id}", id);
            var response = await _busCriptas.DeleteCriptaById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Cripta no encontrada con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Cripta eliminada con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene una cripta por ID", Description = "Recupera una cripta específica utilizando su ID.")]
        public async Task<Response<EntCriptas>> GetCriptaById(Guid id)
        {
            _logger.LogInformation("Iniciando búsqueda de cripta con ID: {Id}", id);
            var response = await _busCriptas.GetCriptaById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Cripta no encontrada con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Cripta encontrada con ID: {Id}", id);
            }
            return response;
        }

        [HttpPost("ByFilters")]
        [SwaggerOperation(Summary = "Obtiene criptas por filtros", Description = "Recupera una lista de criptas que coincidan con los filtros proporcionados.")]
        public async Task<Response<List<EntCriptas>>> GetCriptasByFilters([FromBody] EntCriptaSearchRequest filtros)
        {
            _logger.LogInformation("Iniciando búsqueda de criptas con los filtros: {filtros}", filtros);
            var response = await _busCriptas.GetCriptasByFilters(filtros);
            if (response.HasError)
            {
                _logger.LogWarning("No se encontraron criptas con los filtros {filtros}: {Error}", filtros, response.Message);
            }
            else
            {
                _logger.LogInformation("Criptas encontradas con los filtros: {filtros}", filtros);
            }
            return response;
        }

        [HttpGet("List/{IdSeccion}")]
        [SwaggerOperation(Summary = "Obtiene la lista de criptas", Description = "Recupera una lista de todas las criptas.")]
        public async Task<Response<List<EntCriptas>>> GetCriptaList(Guid IdSeccion)
        {
            _logger.LogInformation("Iniciando recuperación de lista de criptas.");
            var response = await _busCriptas.GetCriptaList(IdSeccion);
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar lista de criptas: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Lista de criptas recuperada exitosamente. Total: {Count}", response.Result?.Count);
            }
            return response;
        }
    }

}
