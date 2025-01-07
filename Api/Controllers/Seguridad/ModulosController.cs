using Business.Interfaces.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Models.Request.Seguridad;
using Models.Seguridad;
using Utils;

namespace Api.Controllers.Seguridad
{
    [Route("api/seguridad/modulos")]
    [ApiController]
    public class ModulosController : ControllerBase
    {
        private readonly IBusModulo busModulo;
        public ModulosController(IBusModulo busModulo)
        {
            this.busModulo = busModulo;
        }
        [HttpPost]
        public async Task<ActionResult<Response<ModuloModelo>>> CrearModulo(ModuloRequest moduloRequest)
        {
            Response<ModuloModelo> response = await busModulo.BCreate(moduloRequest);
            return StatusCode((int)response.HttpCode, response);
        }
        [HttpPut("{idModulo}")]
        public async Task<ActionResult<Response<bool>>> ActualizarModulo(ModuloRequest moduloRequest)
        {
            Response<bool> response = await busModulo.BUpdate(moduloRequest);
            return StatusCode((int)response.HttpCode, response);
        }
        [HttpDelete("{idModulo}")]
        public async Task<ActionResult<Response<bool>>> EliminarModulo(Guid idModulo)
        {
            Response<bool> response = await busModulo.BDelete(idModulo);
            return StatusCode((int)response.HttpCode, response);
        }
    }
}
