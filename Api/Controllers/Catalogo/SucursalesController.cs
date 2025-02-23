using Business.Interfaces.Catalogos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Request.Catalogo.Sucursales;
using Swashbuckle.AspNetCore.Annotations;
using Utils;

namespace Api.Controllers.Catalogo
{
    [Route("api/Sucursales")]
    [Authorize]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly IBusSucursal _bussines;
        private readonly ILogger<SucursalesController> _logger;

        public SucursalesController(IBusSucursal bussines, ILogger<SucursalesController> logger)
        {
            _bussines = bussines;
            _logger = logger;
        }

        [HttpPost("Create")]
        [SwaggerOperation(Summary = "Crea una sucursal", Description = "Valida los datos y guarda una nueva sucursal en la base de datos.")]
        public async Task<Response<EntSucursal>> CreateSucursal([FromBody] EntSucursalRequest sucursal)
        {
            _logger.LogInformation("Iniciando creación de sucursal.");
            var response = await _bussines.ValidateAndSave(sucursal);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear sucursal: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("sucursal creada exitosamente con ID: {Id}", response.Result?.uId);
            }
            return response;
        }

        [HttpPut("Update")]
        [SwaggerOperation(Summary = "Actualiza una sucursal", Description = "Valida y actualiza los datos de una sucursal existente.")]
        public async Task<Response<EntSucursal>> UpdateSucursal([FromBody] EntSucursalUpdateRequest sucursal)
        {
            _logger.LogInformation("Iniciando actualización de sucursal con ID: {Id}", sucursal.uId);
            var response = await _bussines.ValidateAndUpdate(sucursal);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar sucursal con ID {Id}: {Error}", sucursal.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("sucursal actualizada exitosamente con ID: {Id}", sucursal.uId);
            }
            return response;
        }

        [HttpPut("UpdateMaps")]
        [SwaggerOperation(Summary = "Actualiza la ubicación una sucursal", Description = "Valida y actualiza la ubicación de una sucursal existente.")]
        public async Task<Response<EntSucursal>> UpdateSucursalMaps([FromBody] EntSucursalMaps sucursal)
        {
            _logger.LogInformation("Iniciando actualización de sucursal con ID: {Id}", sucursal.uId);
            var response = await _bussines.UpdateMaps(sucursal);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar la ubicación de la sucursal con ID {Id}: {Error}", sucursal.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Ubicación de sucursal actualizada exitosamente con ID: {Id}", sucursal.uId);
            }
            return response;
        }

        [HttpPut("UpdateStatus")]
        [SwaggerOperation(Summary = "Actualiza el estado de una sucursal", Description = "Actualiza el estado booleano de una sucursal.")]
        public async Task<Response<EntSucursal>> UpdateSucursalStatus([FromBody] EntSucursalUpdateEstatusRequest sucursal)
        {
            _logger.LogInformation("Iniciando actualización de estado para sucursal con ID: {Id}", sucursal.uId);
            var response = await _bussines.UpdateStatus(sucursal);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar estado de sucursal con ID {Id}: {Error}", sucursal.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Estado de sucursal actualizado exitosamente con ID: {Id}", sucursal.uId);
            }
            return response;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina una sucursal por ID", Description = "Elimina una sucursal específica utilizando su ID.")]
        public async Task<Response<bool>> DeleteSucursalById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminado de sucursal con ID: {Id}", id);
            var response = await _bussines.DeleteById(id);
            if (response.HasError)
            {
                _logger.LogWarning("sucursal no encontrada con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("sucursal eliminada con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene una sucursal por ID", Description = "Recupera una sucursal específica utilizando su ID.")]
        public async Task<Response<EntSucursal>> GetSucursalById(Guid id)
        {
            _logger.LogInformation("Iniciando búsqueda de la sucursal con ID: {Id}", id);
            var response = await _bussines.GetById(id);
            if (response.HasError)
            {
                _logger.LogWarning("sucursal no encontrada con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("sucursal encontrada con ID: {Id}", id);
            }
            return response;
        }

        [HttpPost("ByFilters")]
        [SwaggerOperation(Summary = "Obtiene sucursales por filtros", Description = "Recupera una lista de sucursales que coincidan con los filtros proporcionados.")]
        public async Task<Response<List<EntSucursal>>> GetSucursalsByFilters([FromBody] EntSucursalSearchRequest filtros)
        {
            _logger.LogInformation("Iniciando sucursales de sucursales con los filtros: {filtros}", filtros);
            var response = await _bussines.GetByFilters(filtros);
            if (response.HasError)
            {
                _logger.LogWarning("No se encontraron sucursales con los filtros {filtros}: {Error}", filtros, response.Message);
            }
            else
            {
                _logger.LogInformation("sucursales encontradas con los filtros: {filtros}", filtros);
            }
            return response;
        }

        [HttpGet("List")]
        [SwaggerOperation(Summary = "Obtiene la lista de sucursales", Description = "Recupera una lista de todas las sucursales.")]
        public async Task<Response<List<EntSucursal>>> GetSucursalList()
        {
            _logger.LogInformation("Iniciando recuperación de lista de sucursales.");
            var response = await _bussines.GetList();
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar lista de sucursales: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Lista de sucursales recuperada exitosamente. Total: {Count}", response.Result?.Count);
            }
            return response;
        }
    }
}
