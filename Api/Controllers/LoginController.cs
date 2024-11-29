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
        public async Task<Response<AuthLogin>> getLogin([FromBody] EntUsuarioLoginRequest login)
        {
            _logger.LogInformation("Iniciando login de usuarios.");
            Response<AuthLogin> response = new Response<AuthLogin>();
            var consulta = await _busUsuarios.getLogin(login);
            response.HttpCode = consulta.HttpCode;
            response.HasError = consulta.HasError;
            response.Message = consulta.Message;
            response.Result = null;
            if (consulta.HasError)
            {
                _logger.LogWarning("Error al recuperar login de usuarios: {Error}", consulta.Message);
            }
            else
            {
                var token = GenerateJwtToken(consulta.Result);
                response.Result = _mapper.Map<AuthLogin>(consulta.Result);
                response.Result.sToken = token;
                _logger.LogInformation("login de usuarios recuperada exitosamente. Total: {consulta.Result}", consulta.Result);
            }
            return response;
        }

        private string GenerateJwtToken(EntUsuarios usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.uId.ToString()),
                new Claim(ClaimTypes.Name, usuario.sContra)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
