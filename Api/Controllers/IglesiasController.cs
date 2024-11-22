using Business.Interfaces;
using Entities;
using Entities.JsonRequest.Iglesias;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [Route("api/Iglesias")]
    [ApiController]
    public class IglesiasController : ControllerBase
    {
        private readonly IBusIglesias _busIglesias;
        private readonly ILogger<IglesiasController> _logger;

        public IglesiasController(IBusIglesias busIglesias, ILogger<IglesiasController> logger)
        {
            _busIglesias = busIglesias;
            _logger = logger;
        }

        [HttpPost("Create")]
        [SwaggerOperation(Summary = "Crea una iglesia", Description = "Valida los datos y guarda una nueva iglesia en la base de datos.")]
        public async Task<Response<EntIglesias>> CreateIglesia([FromBody] EntIglesiaRequest iglesia)
        {
            _logger.LogInformation("Iniciando creación de iglesia.");
            var response = await _busIglesias.ValidateAndSaveIglesia(iglesia);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear iglesia: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Iglesia creada exitosamente con ID: {Id}", response.Result?.uId);
            }
            return response;
        }

        [HttpPut("Update")]
        [SwaggerOperation(Summary = "Actualiza una iglesia", Description = "Valida y actualiza los datos de una iglesia existente.")]
        public async Task<Response<EntIglesias>> UpdateIglesia([FromBody] EntIglesiaUpdateRequest iglesia)
        {
            _logger.LogInformation("Iniciando actualización de iglesia con ID: {Id}", iglesia.uId);
            var response = await _busIglesias.ValidateAndUpdateIglesia(iglesia);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar iglesia con ID {Id}: {Error}", iglesia.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Iglesia actualizada exitosamente con ID: {Id}", iglesia.uId);
            }
            return response;
        }

        [HttpPut("UpdateStatus")]
        [SwaggerOperation(Summary = "Actualiza el estado de una iglesia", Description = "Actualiza el estado booleano de una iglesia.")]
        public async Task<Response<EntIglesias>> UpdateIglesiaStatus([FromBody] EntIglesiaUpdateEstatusRequest iglesia)
        {
            _logger.LogInformation("Iniciando actualización de estado para iglesia con ID: {Id}", iglesia.uId);
            var response = await _busIglesias.UpdateIglesiaStatus(iglesia);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar estado de iglesia con ID {Id}: {Error}", iglesia.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Estado de iglesia actualizado exitosamente con ID: {Id}", iglesia.uId);
            }
            return response;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina una iglesia por ID", Description = "Elimina una iglesia específica utilizando su ID.")]
        public async Task<Response<bool>> DeleteIglesiaById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminado de iglesia con ID: {Id}", id);
            var response = await _busIglesias.DeleteIglesiaById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Iglesia no encontrada con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Iglesia eliminada con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene una iglesia por ID", Description = "Recupera una iglesia específica utilizando su ID.")]
        public async Task<Response<EntIglesias>> GetIglesiaById(Guid id)
        {
            _logger.LogInformation("Iniciando búsqueda de iglesia con ID: {Id}", id);
            var response = await _busIglesias.GetIglesiaById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Iglesia no encontrada con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Iglesia encontrada con ID: {Id}", id);
            }
            return response;
        }

        [HttpPost("ByFilters")]
        [SwaggerOperation(Summary = "Obtiene iglesias por filtros", Description = "Recupera una lista de iglesias que coincidan con los filtros proporcionados.")]
        public async Task<Response<List<EntIglesias>>> GetIglesiasByFilters([FromBody] EntIglesiaSearchRequest filtros)
        {
            _logger.LogInformation("Iniciando búsqueda de iglesias con los filtros: {filtros}", filtros);
            var response = await _busIglesias.GetIglesiasByFilters(filtros);
            if (response.HasError)
            {
                _logger.LogWarning("No se encontraron iglesias con los filtros {filtros}: {Error}", filtros, response.Message);
            }
            else
            {
                _logger.LogInformation("Iglesias encontradas con los filtros: {filtros}", filtros);
            }
            return response;
        }

        [HttpGet("List")]
        [SwaggerOperation(Summary = "Obtiene la lista de iglesias", Description = "Recupera una lista de todas las iglesias.")]
        public async Task<Response<List<EntIglesias>>> GetIglesiaList()
        {
            _logger.LogInformation("Iniciando recuperación de lista de iglesias.");
            var response = await _busIglesias.GetIglesiaList();
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar lista de iglesias: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Lista de iglesias recuperada exitosamente. Total: {Count}", response.Result?.Count);
            }
            return response;
        }
    }
}
