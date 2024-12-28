using Business.Interfaces.Seguridad;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.Seguridad;

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
        public async Task<ActionResult<Response<ModuloModelo>>> CrearModulo(ModuloModelo entBDModulo)
        {
            Response<ModuloModelo> response = await busModulo.BCreate(entBDModulo);
            return StatusCode((int)response.HttpCode, response);
        }
        [HttpPut("{idModulo}")]
        public async Task<ActionResult<Response<ModuloModelo>>> ActualizarModulo(ModuloModelo entBDModulo)
        {
            Response<ModuloModelo> response = await busModulo.BUpdate(entBDModulo);
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
