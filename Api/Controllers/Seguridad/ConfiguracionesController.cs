using Business.Interfaces.Seguridad;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.Seguridad;

namespace Api.Controllers.Seguridad
{
    [Route("api/seguridad/configuraciones")]
    [ApiController]
    public class ConfiguracionesController : ControllerBase
    {
        private readonly IBusConfiguracion busConfiguracion;
        public ConfiguracionesController(IBusConfiguracion busConfiguracion)
        {
            this.busConfiguracion = busConfiguracion;
        }

        [HttpGet("elementos/sistema")]
        public async Task<ActionResult<Response<List<ModuloModelo>>>> ObtenerElementosSistema()
        {
            Response<List<ModuloModelo>> response = await busConfiguracion.ObtenerElementosSistema();
            return StatusCode((int)response.HttpCode, response);
        }
        [HttpGet]
        public async Task<ActionResult<Response<ConfiguracionModelo>>> ObtenerConfiguracion()
        {
            Response<ConfiguracionModelo> response = await busConfiguracion.BObtenerConfiguracion();
            return StatusCode((int)response.HttpCode, response);
        }
        [HttpPost]
        public async Task<ActionResult<Response<bool>>> GuardarConfiguracion(ConfiguracionModelo entConfiguracion)
        {
            Response<bool> response = await busConfiguracion.bCrearConfiguracion(entConfiguracion);
            return StatusCode((int)response.HttpCode, response);
        }
    }
}
