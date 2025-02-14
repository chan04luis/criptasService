using Business.Implementation.Seguridad;
using Business.Interfaces.Catalogos;
using Business.Interfaces.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Request.Usuarios;
using Swashbuckle.AspNetCore.Annotations;
using Utils;

namespace Api.Controllers.Seguridad
{
   {
    [Route("api/tipoDeMantenimiento")]
    [Authorize]
    [ApiController]
    public class TipoDeMantenimientoController : ControllerBase
    {
        private readonly IBusTipoDeMantenimiento busTipoDeMantenimiento;
        private readonly ILogger<TipoDeMantenimientoController> _logger;
        public TipoDeMantenimientoController(IBusTipoDeMantenimiento busTipoDeMantenimiento, ILogger<TipoDeMantenimientoController> logger)
        {
            this.busTipoDeMantenimiento = busTipoDeMantenimiento;
            _logger = logger;
        }


        [HttpPost("Create")]
        
        public async Task<Response<EntTipoDeMantenimiento>> CreateTipoDeMantenimiento([FromBody] EntTipoDeMantenimiento entTipoDeMantenimiento)
        {
            _logger.LogInformation("Iniciando creación de tipo de mantenimiento.");
            var response = await busTipoDeMantenimiento.SaveTipoDeMantenimiento(entTipoDeMantenimiento);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear tipo de mantenimiento: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Tipo de mantenimiento creado exitosamente con ID: {Id}", response.Result?.Id);
            }
            return response;
        }
        [HttpPut("Update")]
       
        public async Task<Response<EntTipoDeMantenimiento>> UpdateTipoDeMantenimiento([FromBody] EntTipoDeMantenimiento entTipoDeMantenimiento)
        {
            _logger.LogInformation("Iniciando actualización de tipo de mantenimiento con ID: {Id}", entTipoDeMantenimiento.Id);
            var response = await busTipoDeMantenimiento.UpdateTipoDeMantenimiento(entTipoDeMantenimiento);
            return response;
        }

        [HttpPut("UpdateStatus")]
      
        public async Task<Response<EntTipoDeMantenimiento>> UpdateStatus([FromBody] EntTipoDeMantenimiento entTipoDeMantenimiento)
        {
            _logger.LogInformation("Iniciando actualización de estado para el tipo de mantenimiento con ID: {Id}", entTipoDeMantenimiento.Id);
            var response = await busTipoDeMantenimiento.UpdateTipoDeMantenimientoStatus(entTipoDeMantenimiento);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar estado de usuario con ID {Id}: {Error}", entTipoDeMantenimiento.Id, response.Message);
            }
            else
            {
                _logger.LogInformation("Estado de usuario actualizado exitosamente con ID: {Id}", entTipoDeMantenimiento.Id);
            }
            return response;
        }

        [HttpDelete("{id}")]
      
        public async Task<Response<bool>> DeleteTipoDeMantenimientoById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminado de tipo de mantenimiento con ID: {Id}", id);
            var response = await busTipoDeMantenimiento.DeleteTipoDeMantenimientoById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Tipo de mantenimiento no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Tipo de mantenimiento eliminado exitosamente con ID: {Id}", id);
            }
            return response;
        }
        [HttpGet("List")]
       
        public async Task<Response<List<EntTipoDeMantenimiento>>> GetUserList()
        {
            _logger.LogInformation("Iniciando recuperación de lista de usuarios.");
            var response = await busTipoDeMantenimiento.GetTipoDeMantenimientoList();
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar lista de tipo de mantenimiento: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Lista de  tipo de mantenimiento recuperada exitosamente. Total: {Count}", response.Result?.Count);
            }
            return response;
        }
    }
}
