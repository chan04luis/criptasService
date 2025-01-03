using Business.Interfaces.Seguridad;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<Response<BotonModelo>>> CrearBoton(BotonModelo entBDBoton)
        {
            Response<BotonModelo> response = await busBoton.BCreate(entBDBoton);
            return StatusCode((int)response.HttpCode, response);
        }

        [HttpPut("{idBoton}")]
        public async Task<ActionResult<Response<bool>>> ActualizarBoton(BotonModelo entBDBoton)
        {
            Response<bool> response = await busBoton.BUpdate(entBDBoton);
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
