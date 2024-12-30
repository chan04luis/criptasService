using Business.Interfaces.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Modelos.Seguridad;
using Utils;

namespace Api.Controllers.Seguridad
{
    [Route("api/seguridad/perfiles")]
    [ApiController]
    public class PerfilesController : ControllerBase
    {
        private readonly IBusPerfiles busPerfiles;
        public PerfilesController(IBusPerfiles busPerfiles)
        {
            this.busPerfiles = busPerfiles;
        }

        [HttpPost]
        public async Task<ActionResult<Response<PerfilModelo>>> CrearPerfil(PerfilModelo entPerfilCreacion)
        {
            Response<PerfilModelo> response = await busPerfiles.BCreate(entPerfilCreacion);
            return StatusCode((int)response.HttpCode, response);
        }

        [HttpPut("{idPerfil}")]
        public async Task<ActionResult<Response<PerfilModelo>>> ActualizarPerfil(Guid idPerfil, PerfilModelo entPerfilCreacion)
        {
            Response<PerfilModelo> response = await busPerfiles.BUpdate(entPerfilCreacion);
            return StatusCode((int)response.HttpCode, response);
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<PerfilModelo>>>> ObtenerPerfiles()
        {
            Response<List<PerfilModelo>> response = await busPerfiles.BGetAll();
            return StatusCode((int)response.HttpCode, response);
        }

        [HttpDelete("{idPerfil}")]
        public async Task<ActionResult<Response<bool>>> ELiminarPerfil(Guid idPerfil)
        {
            Response<bool> response = await busPerfiles.BDelete(idPerfil);
            return StatusCode((int)response.HttpCode, response);
        }
    }
}
