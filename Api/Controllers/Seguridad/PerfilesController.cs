using Business.Interfaces.Seguridad;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.Seguridad;

namespace Api.Controllers.Seguridad
{
    [Route("api/Perfiles")]
    [ApiController]
    public class PerfilesController : ControllerBase
    {
        private readonly IBusPerfiles busPerfiles;
        public PerfilesController(IBusPerfiles busPerfiles)
        {
            this.busPerfiles = busPerfiles;
        }

        [HttpGet("obtenerPerfil/{IdPerfil}")]
        public async Task<ActionResult<Response<PerfilModelo>>> Get(Guid IdPerfil)
        {
            Response<PerfilModelo> response = new Response<PerfilModelo>();

            try
            {
                var temp = await busPerfiles.BGet(IdPerfil);
                response.Result = temp.Result;
                response.HasError = temp.HasError;
                response.HttpCode = temp.HttpCode;
                response.Message = temp.Message;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return StatusCode((int)response.HttpCode, response);
        }
        [HttpPost]
        public async Task<ActionResult<Response<PerfilModelo>>> CrearInvitado(PerfilModelo createModel)
        {
            Response<PerfilModelo> response = await busPerfiles.BCreate(createModel);
            return StatusCode((int)response.HttpCode, response);
        }
        [HttpPut("{IdPerfil}")]
        public async Task<ActionResult<Response<PerfilModelo>>> ActualizarInvitado(Guid IdPerfil, PerfilModelo perfilModelo)
        {
            Response<perfilModelo> response = await busPerfiles.BUpdate(perfilModelo);
            return StatusCode((int)response.HttpCode, response);
        }
    }
}
