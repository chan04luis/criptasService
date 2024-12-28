using Business.Interfaces.Seguridad;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.Seguridad;

namespace Api.Controllers.Seguridad
{
    [Route("api/seguridad/modulos/{idModulo}/paginas")]
    [ApiController]
    public class PaginasController : ControllerBase
    {
        private readonly IBusPagina busPagina;
        public PaginasController(IBusPagina busPagina)
        {
            this.busPagina = busPagina;
        }
        [HttpPost]
        public async Task<ActionResult<Response<PaginaModelo>>> CrearPagina(PaginaModelo entBDPagina)
        {
            Response<PaginaModelo> response = await busPagina.BCreate(entBDPagina);
            return StatusCode((int)response.HttpCode, response);
        }

        [HttpPut("{idPagina}")]
        public async Task<ActionResult<Response<PaginaModelo>>> ActualizarPagina(PaginaModelo entBDPagina)
        {
            Response<PaginaModelo> response = await busPagina.BUpdate(entBDPagina);
            return StatusCode((int)response.HttpCode, response);
        }
        [HttpDelete("{idPagina}")]
        public async Task<ActionResult<Response<bool>>> EliminarPagina(Guid idPagina)
        {
            Response<bool> response = await busPagina.BDelete(idPagina);

        }
    }
