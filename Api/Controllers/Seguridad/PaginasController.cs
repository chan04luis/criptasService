using Business.Interfaces.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Models.Request.Seguridad;
using Models.Seguridad;
using Utils;

namespace Api.Controllers.Seguridad
{
    [Route("api/seguridad/modulos/paginas")]
    [ApiController]
    public class PaginasController : ControllerBase
    {
        private readonly IBusPagina busPagina;
        public PaginasController(IBusPagina busPagina)
        {
            this.busPagina = busPagina;
        }
        [HttpPost]
        public async Task<ActionResult<Response<PaginaModelo>>> CrearPagina(PaginaRequest entPagina)
        {
            Response<PaginaModelo> response = await busPagina.BCreate(entPagina);
            return StatusCode((int)response.HttpCode, response);
        }

        [HttpPut("{idPagina}")]
        public async Task<ActionResult<Response<bool>>> ActualizarPagina(PaginaRequest entPagina)
        {
            Response<bool> response = await busPagina.BUpdate(entPagina);
            return StatusCode((int)response.HttpCode, response);
        }
        [HttpDelete("{idPagina}")]
        public async Task<ActionResult<Response<bool>>> EliminarPagina(Guid idPagina)
        {
            Response<bool> response = await busPagina.BDelete(idPagina);
            return StatusCode((int)response.HttpCode, response);
        }
    }
}
