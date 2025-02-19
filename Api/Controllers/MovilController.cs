using Business.Implementation.Catalogos;
using Business.Interfaces.Catalogos;
using Business.Interfaces.Seguridad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Models;
using Models.Request.Clientes;
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
        public MovilController(IBusAutenticacion _busAutenticacion, IBusClientes busClientes, ILogger<MovilController> logger, IBusServicios busServicios)
        {
            this._busAutenticacion = _busAutenticacion;
            _busClientes = busClientes;
            _logger = logger;
            _busServicios = busServicios;
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
    }
}
