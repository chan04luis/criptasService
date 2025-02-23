using Business.Interfaces.Catalogos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Responses.Servicio;
using Utils;

namespace Api.Controllers
{
    [Route("api/servicios")]
    [Authorize]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly IBusServicios _busServicios;
        private readonly ILogger<ServiciosController> _logger;

        public ServiciosController(IBusServicios busServicios, ILogger<ServiciosController> logger)
        {
            _busServicios = busServicios;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<Response<EntServicios>> CreateServicio([FromBody] EntServicios entServicio)
        {
            _logger.LogInformation("Iniciando creación de servicio.");
            var response = await _busServicios.SaveServicio(entServicio);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear servicio: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Servicio creado exitosamente con ID: {Id}", response.Result?.Id);
            }
            return response;
        }

        [HttpPut("Update")]
        public async Task<Response<EntServicios>> UpdateServicio([FromBody] EntServicios entServicio)
        {
            _logger.LogInformation("Iniciando actualización de servicio con ID: {Id}", entServicio.Id);
            var response = await _busServicios.UpdateServicio(entServicio);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar servicio con ID {Id}: {Error}", entServicio.Id, response.Message);
            }
            else
            {
                _logger.LogInformation("Servicio actualizado exitosamente con ID: {Id}", entServicio.Id);
            }
            return response;
        }

        [HttpPut("UpdateStatus")]
        public async Task<Response<EntServicios>> UpdateStatus([FromBody] EntServicios entServicio)
        {
            _logger.LogInformation("Iniciando actualización de estado para el servicio con ID: {Id}", entServicio.Id);
            var response = await _busServicios.UpdateServicioStatus(entServicio);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar estado del servicio con ID {Id}: {Error}", entServicio.Id, response.Message);
            }
            else
            {
                _logger.LogInformation("Estado del servicio actualizado exitosamente con ID: {Id}", entServicio.Id);
            }
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<Response<bool>> DeleteServicioById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminación de servicio con ID: {Id}", id);
            var response = await _busServicios.DeleteServicioById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Servicio no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Servicio eliminado exitosamente con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("{id}")]
        public async Task<Response<EntServicios>> GetById(Guid id)
        {
            _logger.LogInformation("Iniciando selección de servicio con ID: {Id}", id);
            var response = await _busServicios.BGetById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Servicio no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Servicio selección exitosamente con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("List")]
        public async Task<Response<List<EntServicios>>> GetServicioList()
        {
            _logger.LogInformation("Iniciando recuperación de lista de servicios.");
            var response = await _busServicios.GetServicioList();
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar lista de servicios: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Lista de servicios recuperada exitosamente. Total: {Count}", response.Result?.Count);
            }
            return response;
        }

        [HttpGet("ListActive")]
        public async Task<Response<List<EntServicios>>> GetServicioListActive()
        {
            _logger.LogInformation("Iniciando recuperación de lista de servicios activos.");
            var response = await _busServicios.GetListActive();
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar lista de servicios activos: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Lista de servicios activos recuperada exitosamente. Total: {Count}", response.Result?.Count);
            }
            return response;
        }

        [HttpGet("ListActive/{uIdSucursal}")]
        public async Task<Response<List<EntServiceItem>>> GetServicioListActive(Guid uIdSucursal)
        {
            _logger.LogInformation("Iniciando recuperación de lista de servicios activos.");
            var response = await _busServicios.GetListActive(uIdSucursal);
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar lista de servicios activos: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Lista de servicios activos recuperada exitosamente. Total: {Count}", response.Result?.Count);
            }
            return response;
        }

        [HttpGet("ListAssigment/{uIdSucursal}")]
        public async Task<Response<List<EntServiceItem>>> GetListPreAssigment(Guid uIdSucursal)
        {
            _logger.LogInformation("Iniciando recuperación de lista de servicios activos.");
            var response = await _busServicios.BGetListPreAssigment(uIdSucursal);
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar lista de servicios activos: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Lista de servicios activos recuperada exitosamente. Total: {Count}", response.Result?.Count);
            }
            return response;
        }
        [HttpPut("ListAssigment/Update/{uId}")]
        public async Task<Response<bool>> UpdateServicioAssigment([FromBody] List<EntServiceItem> entities, Guid uId)
        {
            _logger.LogInformation("Iniciando actualización de servicio con ID: {Id}", uId);
            var response = await _busServicios.BSaveToSucursal(entities,uId);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar servicio con ID {Id}: {Error}", uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Servicio actualizado exitosamente con ID: {Id}", uId);
            }
            return response;
        }
    }
}

