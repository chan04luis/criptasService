using AutoMapper;
using Business.Interfaces;
using Entities;
using Entities.Request.Usuarios;
using Entities.Responses.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IBusUsuarios _busUsuarios;
        private readonly ILogger<UsuariosController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public LoginController(IBusUsuarios busUsuarios, ILogger<UsuariosController> logger, IConfiguration configuration, IMapper mapper)
        {
            _busUsuarios = busUsuarios;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
        }
        [HttpPatch]
        [SwaggerOperation(Summary = "Obtiene el token", Description = "Inicia tu sesióm")]
        public async Task<ActionResult<Response<AuthLogin>>> getLogin([FromBody] EntUsuarioLoginRequest login)
        {
            _logger.LogInformation("Iniciando login de usuarios.");
            Response<AuthLogin> response = await _busUsuarios.getLogin(login);
            return StatusCode((int)response.HttpCode, response);
        }

      
    }
}
