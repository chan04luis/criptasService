using Business.Interfaces;
using Entities;
using Entities.JsonRequest.Iglesias;
using Entities.JsonRequest.Zonas;
using Entities.Models;
using Entities.Request.Secciones;
using Entities.Responses.Iglesia;
using Entities.Responses.Zonas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [Route("api/Iglesias")]
    [Authorize]
    [ApiController]
    public class IglesiasController : ControllerBase
    {
        private readonly IBusIglesias _busIglesias;
        private readonly IBusZonas _busZonas;
        private readonly IBusSecciones _busSecciones;
        private readonly ILogger<IglesiasController> _logger;

        public IglesiasController(IBusIglesias busIglesias, ILogger<IglesiasController> logger, IBusZonas busZonas, IBusSecciones busSecciones)
        {
            _busIglesias = busIglesias;
            _logger = logger;
            _busZonas = busZonas;
            _busSecciones = busSecciones;
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
        public async Task<Response<EntIglesiaResponse>> GetIglesiaById(Guid id)
        {
            _logger.LogInformation("Iniciando búsqueda de la iglesia con ID: {Id}", id);
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

        #region Zonas
        [HttpPost("Zonas/Create")]
        [SwaggerOperation(Summary = "Crea una zona", Description = "Valida los datos y guarda una nueva zona en la base de datos.")]
        public async Task<Response<EntZonas>> CreateZona([FromBody] EntZonaRequest zona)
        {
            _logger.LogInformation("Iniciando creación de zona.");
            var response = await _busZonas.ValidateAndSaveZone(zona);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear zona: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Zona creada exitosamente con ID: {Id}", response.Result?.uId);
            }
            return response;
        }

        [HttpPut("Zonas/Update")]
        [SwaggerOperation(Summary = "Actualiza una zona", Description = "Valida y actualiza los datos de una zona existente.")]
        public async Task<Response<EntZonas>> UpdateZona([FromBody] EntZonaUpdateRequest zona)
        {
            _logger.LogInformation("Iniciando actualización de zona con ID: {Id}", zona.uId);
            var response = await _busZonas.ValidateAndUpdateZone(zona);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar zona con ID {Id}: {Error}", zona.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Zona actualizada exitosamente con ID: {Id}", zona.uId);
            }
            return response;
        }

        [HttpPut("Zonas/UpdateStatus")]
        [SwaggerOperation(Summary = "Actualiza el estado de una zona", Description = "Actualiza el estado booleano de una zona.")]
        public async Task<Response<EntZonas>> UpdateZonaStatus([FromBody] EntZonaUpdateEstatusRequest zona)
        {
            _logger.LogInformation("Iniciando actualización de estado para zona con ID: {Id}", zona.uId);
            var response = await _busZonas.UpdateZoneStatus(zona);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar estado de zona con ID {Id}: {Error}", zona.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Estado de zona actualizado exitosamente con ID: {Id}", zona.uId);
            }
            return response;
        }

        [HttpDelete("Zonas/{id}")]
        [SwaggerOperation(Summary = "Elimina una zona por ID", Description = "Elimina una zona específica utilizando su ID.")]
        public async Task<Response<bool>> DeleteZonaById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminado de zona con ID: {Id}", id);
            var response = await _busZonas.DeleteZoneById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Zona no encontrada con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Zona eliminada con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("Zonas/{id}")]
        [SwaggerOperation(Summary = "Obtiene una zona por ID", Description = "Recupera una zona específica utilizando su ID.")]
        public async Task<Response<EntZonas>> GetZonaById(Guid id)
        {
            _logger.LogInformation("Iniciando búsqueda de zona con ID: {Id}", id);
            var response = await _busZonas.GetZoneById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Zona no encontrada con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Zona encontrada con ID: {Id}", id);
            }
            return response;
        }

        [HttpPost("Zonas/ByFilters")]
        [SwaggerOperation(Summary = "Obtiene zonas por filtros", Description = "Recupera una lista de zonas que coincidan con los filtros proporcionados.")]
        public async Task<Response<List<EntZonas>>> GetZonasByFilters([FromBody] EntZonaSearchRequest filtros)
        {
            _logger.LogInformation("Iniciando búsqueda de zonas con los filtros: {filtros}", filtros);
            var response = await _busZonas.GetZonesByFilters(filtros);
            if (response.HasError)
            {
                _logger.LogWarning("No se encontraron zonas con los filtros {filtros}: {Error}", filtros, response.Message);
            }
            else
            {
                _logger.LogInformation("Zonas encontradas con los filtros: {filtros}", filtros);
            }
            return response;
        }

        [HttpGet("Zonas/List/{IdIglesia}")]
        [SwaggerOperation(Summary = "Obtiene la lista de zonas", Description = "Recupera una lista de todas las zonas.")]
        public async Task<Response<List<EntZonas>>> GetZonaList(Guid IdIglesia)
        {
            _logger.LogInformation("Iniciando recuperación de lista de zonas.");
            var response = await _busZonas.GetZoneList(IdIglesia);
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar lista de zonas: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Lista de zonas recuperada exitosamente. Total: {Count}", response.Result?.Count);
            }
            return response;
        }
        #endregion

        #region Secciones
        [HttpPost("Zonas/Secciones/Create")]
        [SwaggerOperation(Summary = "Crea una sección", Description = "Valida los datos y guarda una nueva sección en la base de datos.")]
        public async Task<Response<EntSecciones>> CreateSeccion([FromBody] EntSeccionRequest seccion)
        {
            _logger.LogInformation("Iniciando creación de sección.");
            var response = await _busSecciones.ValidateAndSaveSection(seccion);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear sección: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Sección creada exitosamente con ID: {Id}", response.Result?.uId);
            }
            return response;
        }

        [HttpPut("Zonas/Secciones/Update")]
        [SwaggerOperation(Summary = "Actualiza una sección", Description = "Valida y actualiza los datos de una sección existente.")]
        public async Task<Response<EntSecciones>> UpdateSeccion([FromBody] EntSeccionesUpdateRequest seccion)
        {
            _logger.LogInformation("Iniciando actualización de sección con ID: {Id}", seccion.uId);
            var response = await _busSecciones.ValidateAndUpdateSection(seccion);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar sección con ID {Id}: {Error}", seccion.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Sección actualizada exitosamente con ID: {Id}", seccion.uId);
            }
            return response;
        }

        [HttpPut("Zonas/Secciones/UpdateStatus")]
        [SwaggerOperation(Summary = "Actualiza el estado de una sección", Description = "Actualiza el estado booleano de una sección.")]
        public async Task<Response<EntSecciones>> UpdateSeccionStatus([FromBody] EntSeccionesUpdateEstatusRequest seccion)
        {
            _logger.LogInformation("Iniciando actualización de estado para sección con ID: {Id}", seccion.uId);
            var response = await _busSecciones.UpdateSectionStatus(seccion);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar estado de sección con ID {Id}: {Error}", seccion.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Estado de sección actualizado exitosamente con ID: {Id}", seccion.uId);
            }
            return response;
        }

        [HttpDelete("Zonas/Secciones/{id}")]
        [SwaggerOperation(Summary = "Elimina una sección por ID", Description = "Elimina una sección específica utilizando su ID.")]
        public async Task<Response<bool>> DeleteSeccionById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminado de sección con ID: {Id}", id);
            var response = await _busSecciones.DeleteSectionById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Sección no encontrada con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Sección eliminada con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("Zonas/Secciones/{id}")]
        [SwaggerOperation(Summary = "Obtiene una sección por ID", Description = "Recupera una sección específica utilizando su ID.")]
        public async Task<Response<EntSecciones>> GetSeccionById(Guid id)
        {
            _logger.LogInformation("Iniciando búsqueda de sección con ID: {Id}", id);
            var response = await _busSecciones.GetSectionById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Sección no encontrada con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Sección encontrada con ID: {Id}", id);
            }
            return response;
        }

        [HttpPost("Zonas/Secciones/ByFilters")]
        [SwaggerOperation(Summary = "Obtiene secciones por filtros", Description = "Recupera una lista de secciones que coincidan con los filtros proporcionados.")]
        public async Task<Response<List<EntSecciones>>> GetSeccionesByFilters([FromBody] EntSeccionSearchRequest filtros)
        {
            _logger.LogInformation("Iniciando búsqueda de secciones con los filtros: {filtros}", filtros);
            var response = await _busSecciones.GetSectionsByFilters(filtros);
            if (response.HasError)
            {
                _logger.LogWarning("No se encontraron secciones con los filtros {filtros}: {Error}", filtros, response.Message);
            }
            else
            {
                _logger.LogInformation("Secciones encontradas con los filtros: {filtros}", filtros);
            }
            return response;
        }

        [HttpGet("Secciones/List/{IdZona}")]
        [SwaggerOperation(Summary = "Obtiene la lista de secciones", Description = "Recupera una lista de todas las secciones.")]
        public async Task<Response<List<EntSecciones>>> GetSeccionList(Guid IdZona)
        {
            _logger.LogInformation("Iniciando recuperación de lista de secciones.");
            var response = await _busSecciones.GetSectionList(IdZona);
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar lista de secciones: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Lista de secciones recuperada exitosamente. Total: {Count}", response.Result?.Count);
            }
            return response;
        }
        #endregion

    }
}
