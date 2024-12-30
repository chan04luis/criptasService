using Business.Interfaces.Seguridad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Modelos.Seguridad;
using Utils;

namespace Api.Controllers.Seguridad
{
    [Route("api/autenticacion")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IBusAutenticacion _busAutenticacion;
        public AutenticacionController( IBusAutenticacion _busAutenticacion)
        {
            this._busAutenticacion = _busAutenticacion;
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Response<LoginResponseModelo>>> Login(LoginModelo entLogin)
        {
            Response<LoginResponseModelo> response = await _busAutenticacion.Login(entLogin);
            return StatusCode((int)response.HttpCode, response);

        }
    }
}
