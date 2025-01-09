using Business.Interfaces.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Seguridad;
using Utils;

namespace Api.Controllers.Seguridad
{
    [Route("api/seguridad")]
    [Authorize]
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
        public async Task<ActionResult<Response<bool>>> GuardarPermisos(GuardarPermisosModelo lstPermisosElementos)
        {
            Response<bool> response = await busPermisos.GuardarPermisos(lstPermisosElementos);
            return StatusCode((int)response.HttpCode, response);
        }
    }
}
