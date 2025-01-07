using Business.Interfaces.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Models.Request.Seguridad;
using Models.Seguridad;
using Utils;

namespace Api.Controllers.Seguridad
{
    [Route("api/seguridad/botones")]
    [ApiController]
    public class BotonesController : ControllerBase
    {
        private readonly IBusBoton busBoton;
        public BotonesController(IBusBoton busBoton)
        {
            this.busBoton = busBoton;
        }
        [HttpPost]
        public async Task<ActionResult<Response<BotonModelo>>> CrearBoton(BotonRequest entBoton)
        {
            Response<BotonModelo> response = await busBoton.BCreate(entBoton);
            return StatusCode((int)response.HttpCode, response);
        }

        [HttpPut("{idBoton}")]
        public async Task<ActionResult<Response<bool>>> ActualizarBoton(BotonRequest entBoton)
        {
            Response<bool> response = await busBoton.BUpdate(entBoton);
            return StatusCode((int)response.HttpCode, response);
        }
        [HttpDelete("{idBoton}")]
        public async Task<ActionResult<Response<bool>>> EliminarPagina(Guid idBoton)
        {
            Response<bool> response = await busBoton.BDelete(idBoton);
            return StatusCode((int)response.HttpCode, response);
        }
    }
}
