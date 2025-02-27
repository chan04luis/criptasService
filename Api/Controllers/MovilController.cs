using Business.Implementation.Catalogos;
using Business.Interfaces.Catalogos;
using Business.Interfaces.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Request.Clientes;
using Models.Responses.Criptas;
using Models.Responses.Servicio;
using Models.Seguridad;
using Swashbuckle.AspNetCore.Annotations;
using Utils;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovilController : ControllerBase
    {
        private readonly IBusAutenticacion _busAutenticacion;
        private readonly IBusClientes _busClientes;
        private readonly ILogger<MovilController> _logger;
        private readonly IBusServicios _busServicios;
        private readonly IBusIglesias _busIglesias;
        private readonly IBusCriptas _busCriptas;

        public MovilController(IBusAutenticacion _busAutenticacion, IBusClientes busClientes, ILogger<MovilController> logger, 
            IBusServicios busServicios, IBusIglesias busIglesias, IBusCriptas busCriptas)
        {
            this._busAutenticacion = _busAutenticacion;
            _busClientes = busClientes;
            _logger = logger;
            _busServicios = busServicios;
            _busIglesias = busIglesias;
            _busCriptas = busCriptas;
        }

        [HttpGet("servicio/{id}")]
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

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Response<LoginClienteResponseModelo>>> LoginMovil(LoginModelo entLogin)
        {
            Response<LoginClienteResponseModelo> response = await _busAutenticacion.LoginMovil(entLogin);
            return StatusCode((int)response.HttpCode, response);

        }

        [HttpPost("nuevo")]
        [SwaggerOperation(Summary = "Crea un cliente", Description = "Valida los datos y guarda un nuevo cliente en la base de datos.")]
        public async Task<Response<bool>> CreateClient([FromBody] EntClienteRequest cliente)
        {
            var respuesta = new Response<bool>();
            _logger.LogInformation("Iniciando creación de cliente.");
            var response = await _busClientes.ValidateAndSaveClientA(cliente);
            if (response.HasError)
            {
                respuesta.SetError(response.Message);
                respuesta.HttpCode = response.HttpCode;
                _logger.LogWarning("Error al crear cliente: {Error}", response.Message);
            }
            else
            {
                respuesta.SetSuccess(true);
                _logger.LogInformation("Cliente creado exitosamente con ID: {Id}", response.Result?.uId);
            }
            return respuesta;
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

        [HttpGet("ListActive/{uIdIglesia}")]
        public async Task<Response<List<EntServiceItem>>> GetServicioListActive(Guid uIdIglesia)
        {
            _logger.LogInformation("Iniciando recuperación de lista de servicios activos.");
            var response = await _busServicios.GetListActive(uIdIglesia);
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

        [HttpGet("ListDisponible/Iglesia/{uId}")]
        [SwaggerOperation(Summary = "Obtiene la lista de criptas", Description = "Recupera una lista de todas las criptas.")]
        public async Task<Response<List<CriptasDisponibles>>> GetCriptaListDisponibleByIglesia(Guid uId)
        {
            _logger.LogInformation("Iniciando recuperación de lista de criptas.");
            var response = await _busCriptas.BGetListDisponibleByIglesia(uId);
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
