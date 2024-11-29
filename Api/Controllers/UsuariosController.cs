using Business.Interfaces;
using Entities;
using Entities.JsonRequest.Usuarios;
using Entities.Request.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
namespace Api.Controllers
{
    [Route("api/Usuarios")]
    [Authorize]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IBusUsuarios _busUsuarios;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(IBusUsuarios busUsuarios, ILogger<UsuariosController> logger)
        {
            _busUsuarios = busUsuarios;
            _logger = logger;
        }

        [HttpPost("Create")]
        [SwaggerOperation(Summary = "Crea un usuario", Description = "Valida los datos y guarda un nuevo usuario en la base de datos.")]
        public async Task<Response<EntUsuarios>> CreateUser([FromBody] EntUsuarioRequest usuario)
        {
            _logger.LogInformation("Iniciando creación de usuario.");
            var response = await _busUsuarios.ValidateAndSaveUser(usuario);
            if (response.HasError)
            {
                _logger.LogWarning("Error al crear usuario: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Usuario creado exitosamente con ID: {Id}", response.Result?.uId);
            }
            return response;
        }

        [HttpPut("Update")]
        [SwaggerOperation(Summary = "Actualiza un usuario", Description = "Valida y actualiza los datos de un usuario existente.")]
        public async Task<Response<EntUsuarios>> UpdateUser([FromBody] EntUsuarioUpdateRequest usuario)
        {
            _logger.LogInformation("Iniciando actualización de usuario con ID: {Id}", usuario.uId);
            var response = await _busUsuarios.ValidateAndUpdateUser(usuario);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar usuario con ID {Id}: {Error}", usuario.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Usuario actualizado exitosamente con ID: {Id}", usuario.uId);
            }
            return response;
        }

        [HttpPut("UpdateStatus")]
        [SwaggerOperation(Summary = "Actualiza el estado de un usuario", Description = "Actualiza el estado booleano de un usuario.")]
        public async Task<Response<EntUsuarios>> UpdateUserStatus([FromBody] EntUsuarioUpdateEstatusRequest usuario)
        {
            _logger.LogInformation("Iniciando actualización de estado para usuario con ID: {Id}", usuario.uId);
            var response = await _busUsuarios.UpdateUserStatus(usuario);
            if (response.HasError)
            {
                _logger.LogWarning("Error al actualizar estado de usuario con ID {Id}: {Error}", usuario.uId, response.Message);
            }
            else
            {
                _logger.LogInformation("Estado de usuario actualizado exitosamente con ID: {Id}", usuario.uId);
            }
            return response;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un usuario por ID", Description = "Elimina un usuario específico utilizando su ID.")]
        public async Task<Response<bool>> DeleteUserById(Guid id)
        {
            _logger.LogInformation("Iniciando eliminado de usuario con ID: {Id}", id);
            var response = await _busUsuarios.DeleteUserById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Usuario no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Usuario eliminado exitosamente con ID: {Id}", id);
            }
            return response;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un usuario por ID", Description = "Recupera un usuario específico utilizando su ID.")]
        public async Task<Response<EntUsuarios>> GetUserById(Guid id)
        {
            _logger.LogInformation("Iniciando búsqueda de usuario con ID: {Id}", id);
            var response = await _busUsuarios.GetUserById(id);
            if (response.HasError)
            {
                _logger.LogWarning("Usuario no encontrado con ID {Id}: {Error}", id, response.Message);
            }
            else
            {
                _logger.LogInformation("Usuario encontrado con ID: {Id}", id);
            }
            return response;
        }

        [HttpPost("ByFilters")]
        [SwaggerOperation(Summary = "Obtiene usuarios por filtros", Description = "Recupera una lista de usuarios que coincidan con los filtros proporcionados.")]
        public async Task<Response<List<EntUsuarios>>> GetUsersByFilters([FromBody] EntUsuarioSearchRequest usuario)
        {
            _logger.LogInformation("Iniciando búsqueda de usuarios con los filtros: {usuario}", usuario);
            var response = await _busUsuarios.GetUsersByFilters(usuario);
            if (response.HasError)
            {
                _logger.LogWarning("No se encontraron usuarios con los filtros {usuario}: {Error}", usuario, response.Message);
            }
            else
            {
                _logger.LogInformation("Usuarios encontrados con los filtros: {usuario}", usuario);
            }
            return response;
        }

        [HttpGet("List")]
        [SwaggerOperation(Summary = "Obtiene la lista de usuarios", Description = "Recupera una lista de todos los usuarios.")]
        public async Task<Response<List<EntUsuarios>>> GetUserList()
        {
            _logger.LogInformation("Iniciando recuperación de lista de usuarios.");
            var response = await _busUsuarios.GetUserList();
            if (response.HasError)
            {
                _logger.LogWarning("Error al recuperar lista de usuarios: {Error}", response.Message);
            }
            else
            {
                _logger.LogInformation("Lista de usuarios recuperada exitosamente. Total: {Count}", response.Result?.Count);
            }
            return response;
        }

       
    }
}
