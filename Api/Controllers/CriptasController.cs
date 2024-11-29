using Business.Interfaces;
using Entities;
using Entities.Models;
using Entities.Request.Criptas;
using Entities.Request.Fallecidos;
using Entities.Request.Visitas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [Route("api/Criptas")]
    [Authorize]
    [ApiController]
    public class CriptasController : ControllerBase
    {
        private readonly IBusCriptas _busCriptas;
        private readonly IBusVisitas _busVisitas;
        private readonly IBusFallecidos _busFallecidos;
        private readonly IBusBeneficiarios _busBeneficiarios;
        private readonly ILogger<CriptasController> _logger;

        public CriptasController(IBusCriptas busCriptas, ILogger<CriptasController> logger,
                             IBusVisitas busVisitas, IBusFallecidos busFallecidos,
                             IBusBeneficiarios busBeneficiarios)
        {
            _busCriptas = busCriptas;
            _logger = logger;
            _busVisitas = busVisitas;
            _busFallecidos = busFallecidos;
            _busBeneficiarios = busBeneficiarios;
        }

        #region Criptas
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
        #endregion

        #region Endpoints de Visitas

        [HttpPost("Visitas/Create")]
        [SwaggerOperation(Summary = "Crea una visita", Description = "Crea una nueva visita para una cripta.")]
        public async Task<Response<EntVisitas>> CreateVisita([FromBody] EntVisitasRequest visita)
        {
            _logger.LogInformation("Iniciando creación de visita.");
            var response = await _busVisitas.SaveVisit(visita);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear visita: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Visita creada exitosamente con ID: {Id}", response.Result?.uId);
            }
            return response;
        }

        [HttpDelete("Visitas/Delete/{id}")]
        [SwaggerOperation(Summary = "Elimina una visita por ID", Description = "Elimina una visita específica utilizando su ID.")]
        public async Task<Response<bool>> DeleteVisitaById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminación de visita con ID: {Id}", id);
            var response = await _busVisitas.DeleteVisit(id);
            if (response.HasError)
            {
                _logger.LogWarning("Visita no encontrada con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Visita eliminada con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("Visitas/{id}")]
        [SwaggerOperation(Summary = "Obtiene una visita por ID", Description = "Recupera una visita específica utilizando su ID.")]
        public async Task<Response<EntVisitas>> GetVisitaById(Guid id)
        {
            _logger.LogInformation("Iniciando búsqueda de visita con ID: {Id}", id);
            var response = await _busVisitas.GetVisitById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Visita no encontrada con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Visita encontrada con ID: {Id}", id);
            }
            return response;
        }

        #endregion

        #region Endpoints de Fallecidos

        [HttpPost("Fallecidos/Create")]
        [SwaggerOperation(Summary = "Crea un fallecido", Description = "Crea un nuevo registro de fallecido.")]
        public async Task<Response<EntFallecidos>> CreateFallecido([FromBody] EntFallecidosRequest fallecido)
        {
            _logger.LogInformation("Iniciando creación de fallecido.");
            var response = await _busFallecidos.SaveDeceased(fallecido);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear fallecido: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Fallecido creado exitosamente con ID: {Id}", response.Result?.uId);
            }
            return response;
        }

        [HttpPut("Fallecidos/Update")]
        [SwaggerOperation(Summary = "Actualiza un fallecido", Description = "Actualiza los datos de un fallecido existente.")]
        public async Task<Response<EntFallecidos>> UpdateFallecido([FromBody] EntFallecidosUpdateRequest fallecido)
        {
            _logger.LogInformation("Iniciando actualización de fallecido con ID: {Id}", fallecido.uId);
            var response = await _busFallecidos.UpdateDeceased(fallecido);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar fallecido con ID {Id}: {Error}", fallecido.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Fallecido actualizado exitosamente con ID: {Id}", fallecido.uId);
            }
            return response;
        }

        [HttpDelete("Fallecidos/Delete/{id}")]
        [SwaggerOperation(Summary = "Elimina un fallecido por ID", Description = "Elimina un fallecido específico utilizando su ID.")]
        public async Task<Response<bool>> DeleteFallecidoById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminación de fallecido con ID: {Id}", id);
            var response = await _busFallecidos.DeleteDeceased(id);
            if (response.HasError)
            {
                _logger.LogWarning("Fallecido no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Fallecido eliminado con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("Fallecidos/{id}")]
        [SwaggerOperation(Summary = "Obtiene un fallecido por ID", Description = "Recupera un fallecido específico utilizando su ID.")]
        public async Task<Response<EntFallecidos>> GetFallecidoById(Guid id)
        {
            _logger.LogInformation("Iniciando búsqueda de fallecido con ID: {Id}", id);
            var response = await _busFallecidos.GetDeceasedById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Fallecido no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Fallecido encontrado con ID: {Id}", id);
            }
            return response;
        }

        #endregion

        #region Endpoints de Beneficiarios

        [HttpPost("Beneficiarios/Create")]
        [SwaggerOperation(Summary = "Crea un beneficiario", Description = "Crea un nuevo beneficiario para una cripta.")]
        public async Task<Response<EntBeneficiarios>> CreateBeneficiario([FromBody] EntBeneficiariosRequest beneficiario)
        {
            _logger.LogInformation("Iniciando creación de beneficiario.");
            var response = await _busBeneficiarios.SaveBeneficiary(beneficiario);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear beneficiario: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Beneficiario creado exitosamente con ID: {Id}", response.Result?.uId);
            }
            return response;
        }

        [HttpPut("Beneficiarios/Update")]
        [SwaggerOperation(Summary = "Actualiza un beneficiario", Description = "Actualiza los datos de un beneficiario existente.")]
        public async Task<Response<EntBeneficiarios>> UpdateBeneficiario([FromBody] EntBeneficiariosUpdateRequest beneficiario)
        {
            _logger.LogInformation("Iniciando actualización de beneficiario con ID: {Id}", beneficiario.uId);
            var response = await _busBeneficiarios.UpdateBeneficiary(beneficiario);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar beneficiario con ID {Id}: {Error}", beneficiario.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Beneficiario actualizado exitosamente con ID: {Id}", beneficiario.uId);
            }
            return response;
        }

        [HttpDelete("Beneficiarios/Delete/{id}")]
        [SwaggerOperation(Summary = "Elimina un beneficiario por ID", Description = "Elimina un beneficiario específico utilizando su ID.")]
        public async Task<Response<bool>> DeleteBeneficiarioById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminación de beneficiario con ID: {Id}", id);
            var response = await _busBeneficiarios.DeleteBeneficiary(id);
            if (response.HasError)
            {
                _logger.LogWarning("Beneficiario no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Beneficiario eliminado con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("Beneficiarios/{id}")]
        [SwaggerOperation(Summary = "Obtiene un beneficiario por ID", Description = "Recupera un beneficiario específico utilizando su ID.")]
        public async Task<Response<EntBeneficiarios>> GetBeneficiarioById(Guid id)
        {
            _logger.LogInformation("Iniciando búsqueda de beneficiario con ID: {Id}", id);
            var response = await _busBeneficiarios.GetBeneficiaryById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Beneficiario no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Beneficiario encontrado con ID: {Id}", id);
            }
            return response;
        }

        #endregion
    }

}
