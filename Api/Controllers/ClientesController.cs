using Business.Interfaces;
using Entities;
using Entities.JsonRequest.Clientes;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [Route("api/Clientes")]
    [Authorize]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IBusClientes _busClientes;
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(IBusClientes busClientes, ILogger<ClientesController> logger)
        {
            _busClientes = busClientes;
            _logger = logger;
        }

        [HttpPost("Create")]
        [SwaggerOperation(Summary = "Crea un cliente", Description = "Valida los datos y guarda un nuevo cliente en la base de datos.")]
        public async Task<Response<EntClientes>> CreateClient([FromBody] EntClienteRequest cliente)
        {
            _logger.LogInformation("Iniciando creación de cliente.");
            var response = await _busClientes.ValidateAndSaveClient(cliente);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear cliente: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Cliente creado exitosamente con ID: {Id}", response.Result?.uId);
            }
            return response;
        }

        [HttpPut("Update")]
        [SwaggerOperation(Summary = "Actualiza un cliente", Description = "Valida y actualiza los datos de un cliente existente.")]
        public async Task<Response<EntClientes>> UpdateClient([FromBody] EntClienteUpdateRequest cliente)
        {
            _logger.LogInformation("Iniciando actualización de cliente con ID: {Id}", cliente.uId);
            var response = await _busClientes.ValidateAndUpdateClient(cliente);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar cliente con ID {Id}: {Error}", cliente.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Cliente actualizado exitosamente con ID: {Id}", cliente.uId);
            }
            return response;
        }

        [HttpPut("UpdateStatus")]
        [SwaggerOperation(Summary = "Actualiza el estado de un cliente", Description = "Actualiza el estado booleano de un cliente.")]
        public async Task<Response<EntClientes>> UpdateClientStatus([FromBody] EntClienteUpdateEstatusRequest cliente)
        {
            _logger.LogInformation("Iniciando actualización de estado para cliente con ID: {Id}", cliente.uId);
            var response = await _busClientes.UpdateClientStatus(cliente);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar estado de cliente con ID {Id}: {Error}", cliente.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Estado de cliente actualizado exitosamente con ID: {Id}", cliente.uId);
            }
            return response;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un cliente por ID", Description = "Elimina un cliente específico utilizando su ID.")]
        public async Task<Response<bool>> DeleteClientById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminado de cliente con ID: {Id}", id);
            var response = await _busClientes.DeleteClientById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Cliente no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Cliente encontrado con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un cliente por ID", Description = "Recupera un cliente específico utilizando su ID.")]
        public async Task<Response<EntClientes>> GetClientById(Guid id)
        {
            _logger.LogInformation("Iniciando búsqueda de cliente con ID: {Id}", id);
            var response = await _busClientes.GetClientById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Cliente no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Cliente encontrado con ID: {Id}", id);
            }
            return response;
        }

        [HttpPost("ByFilters")]
        [SwaggerOperation(Summary = "Obtiene clientes por filtros", Description = "Recupera una lista de clientes que coincidan con los filtros proporcionado.")]
        public async Task<Response<List<EntClientes>>> GetClientsByFilters([FromBody] EntClienteSearchRequest cliente)
        {
            _logger.LogInformation("Iniciando búsqueda de clientes con los filtros: {cliente}", cliente);
            var response = await _busClientes.GetClientsByFilters(cliente);
            if (response.HasError)
            {
                _logger.LogWarning("No se encontraron clientes con los filtros {cliente}: {Error}", cliente, response.Message);
            }
            else
            {
                _logger.LogInformation("Clientes encontrados con los filtros: {cliente}", cliente);
            }
            return response;
        }

        [HttpGet("List")]
        [SwaggerOperation(Summary = "Obtiene la lista de clientes", Description = "Recupera una lista de todos los clientes.")]
        public async Task<Response<List<EntClientes>>> GetClientList()
        {
            _logger.LogInformation("Iniciando recuperación de lista de clientes.");
            var response = await _busClientes.GetClientList();
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar lista de clientes: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Lista de clientes recuperada exitosamente. Total: {Count}", response.Result?.Count);
            }
            return response;
        }
    }
}
