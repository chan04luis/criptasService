using Business.Interfaces.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Modelos.Seguridad;
using Utils;

namespace Api.Controllers.Seguridad
{
    [Route("api/seguridad")]
    [ApiController]
    public class PermisosController : ControllerBase
    {
        private readonly IBusPermiso busPermisos;
        public PermisosController(IBusPermiso busPermisos)
        {
            this.busPermisos = busPermisos;
        }
        [HttpGet("perfiles/{idPerfil}/permisos")]
        public async Task<ActionResult<Response<PerfilPermisosModelo>>> ObtenerPermisos(Guid idPerfil)
        {
            Response<PerfilPermisosModelo> response = await busPermisos.ObtenerPermisos(idPerfil);
            return StatusCode((int)response.HttpCode, response);
        }

        [HttpPost("permisos")]
        public async Task<ActionResult<Response<bool>>> GuardarPermisos(IFormCollection form)
        {
            Response<bool> response = await busPermisos.GuardarPermisos(form);
            return StatusCode((int)response.HttpCode, response);
        }
    }
}
