using Business.Implementation.Seguridad;
using Business.Interfaces.Seguridad;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.Seguridad;

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
        public async Task<ActionResult<Response<PerfilModelo>>> ObtenerPermisos(Guid idPerfil)
        {
            Response<PerfilModelo> response = await busPermisos.ObtenerPermisos(idPerfil);
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
