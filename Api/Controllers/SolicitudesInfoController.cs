using Business.Interfaces.Catalogos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Utils;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesInfoController : ControllerBase
    {
        private readonly IBusSolicitudesInfo _busSolicitudesInfo;
        private readonly ILogger<SolicitudesInfoController> _logger;

        public SolicitudesInfoController(IBusSolicitudesInfo busSolicitudesInfo, ILogger<SolicitudesInfoController> logger)
        {
            _busSolicitudesInfo = busSolicitudesInfo;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<Response<EntSolicitudesInfo>> CreateSolicitud([FromBody] EntSolicitudesInfo solicitud)
        {
            _logger.LogInformation("Iniciando creación de solicitud.");
            var response = await _busSolicitudesInfo.SaveSolicitud(solicitud);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear solicitud: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Solicitud creada exitosamente con ID: {Id}", response.Result?.Id);
            }
            return response;
        }

        [HttpPut("Update")]
        public async Task<Response<EntSolicitudesInfo>> UpdateSolicitud([FromBody] EntSolicitudesInfo solicitud)
        {
            _logger.LogInformation("Iniciando actualización de solicitud con ID: {Id}", solicitud.Id);
            var response = await _busSolicitudesInfo.UpdateSolicitud(solicitud);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar solicitud con ID {Id}: {Error}", solicitud.Id, response.Message);
            }
            else
            {
                _logger.LogInformation("Solicitud actualizada exitosamente con ID: {Id}", solicitud.Id);
            }
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<Response<bool>> DeleteSolicitudById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminación de solicitud con ID: {Id}", id);
            var response = await _busSolicitudesInfo.DeleteSolicitudById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Solicitud no encontrada con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Solicitud eliminada exitosamente con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("{id}")]
        public async Task<Response<EntSolicitudesInfo>> GetSolicitudById(Guid id)
        {
            _logger.LogInformation("Iniciando recuperación de solicitud con ID: {Id}", id);
            var response = await _busSolicitudesInfo.GetSolicitudById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar solicitud con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Solicitud recuperada exitosamente con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("List")]
        public async Task<Response<List<EntSolicitudesInfo>>> GetSolicitudList()
        {
            _logger.LogInformation("Iniciando recuperación de lista de solicitudes.");
            var response = await _busSolicitudesInfo.GetSolicitudList();
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar lista de solicitudes: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Lista de solicitudes recuperada exitosamente. Total: {Count}", response.Result?.Count);
            }
            return response;
        }

        [HttpGet("ListActive")]
        public async Task<Response<List<EntSolicitudesInfo>>> GetListActive()
        {
            _logger.LogInformation("Iniciando recuperación de lista de solicitudes activas.");
            var response = await _busSolicitudesInfo.GetListActive();
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar lista de solicitudes activas: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Lista de solicitudes activas recuperada exitosamente. Total: {Count}", response.Result?.Count);
            }
            return response;
        }
    }
}
