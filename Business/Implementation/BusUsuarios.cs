using AutoMapper;
using Business.Data;
using Business.Interfaces;
using Entities.JsonRequest.Usuarios;
using Entities.Request.Usuarios;
using Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Interfaces;

namespace Business.Implementation
{
    public class BusUsuarios : IBusUsuarios
    {
        private readonly IUsuariosRepositorio _usuariosRepositorio;
        private readonly IFiltros _filtros;
        private readonly ILogger<BusUsuarios> _logger;
        private readonly IMapper _mapper;

        public BusUsuarios(IUsuariosRepositorio usuariosRepositorio, IFiltros filtros, ILogger<BusUsuarios> logger, IMapper mapper)
        {
            _usuariosRepositorio = usuariosRepositorio;
            _filtros = filtros;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<EntUsuarios>> ValidateAndSaveUser(EntUsuarioRequest usuario)
        {
            var response = new Response<EntUsuarios>();

            try
            {
                if (string.IsNullOrWhiteSpace(usuario.sNombres) ||
                    string.IsNullOrWhiteSpace(usuario.sApellidos) ||
                    string.IsNullOrWhiteSpace(usuario.sCorreo) ||
                    string.IsNullOrWhiteSpace(usuario.sTelefono) ||
                    string.IsNullOrWhiteSpace(usuario.sContra))
                {
                    response.SetError("Los campos Nombres, Apellidos, Teléfono, Coreo y Contraseña son obligatorios.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                if (!_filtros.IsValidPhone(usuario.sTelefono))
                {
                    response.SetError("El número de teléfono debe tener 10 dígitos numéricos.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                if (!_filtros.IsValidEmail(usuario.sCorreo))
                {
                    response.SetError("El formato del correo electrónico es inválido.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var item = await _usuariosRepositorio.DGetByEmail(usuario.sCorreo);
                if (!item.HasError && item.Result != null)
                {
                    response.SetError("Email ya registrado.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                EntUsuarios nUsuario = new EntUsuarios
                {
                    uId = Guid.NewGuid(),
                    sNombres = usuario.sNombres,
                    sApellidos = usuario.sApellidos,
                    sCorreo = usuario.sCorreo,
                    sTelefono = usuario.sTelefono,
                    sContra = usuario.sContra,
                    bActivo = true,
                    dtFechaActualizacion = DateTime.Now.ToLocalTime(),
                    dtFechaRegistro = DateTime.Now.ToLocalTime()
                };

                return await SaveUser(nUsuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y guardar el usuario");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntUsuarios>> SaveUser(EntUsuarios usuario)
        {
            try
            {
                return await _usuariosRepositorio.DSave(_mapper.Map<EntUsuarios>(usuario));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el usuario");
                var response = new Response<EntUsuarios>();
                response.SetError("Hubo un error al guardar el usuario.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntUsuarios>> ValidateAndUpdateUser(EntUsuarioUpdateRequest usuario)
        {
            var response = new Response<EntUsuarios>();

            try
            {
                if (string.IsNullOrWhiteSpace(usuario.sNombres) ||
                    string.IsNullOrWhiteSpace(usuario.sApellidos) ||
                    string.IsNullOrWhiteSpace(usuario.sTelefono))
                {
                    response.SetError("Los campos Nombres, Apellidos y Teléfono son obligatorios.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                if (!_filtros.IsValidPhone(usuario.sTelefono))
                {
                    response.SetError("El número de teléfono debe tener 10 dígitos numéricos.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var nUsuario = _mapper.Map<EntUsuarios>(usuario);
                return await UpdateUser(nUsuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y actualizar el usuario");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntUsuarios>> UpdateUser(EntUsuarios usuario)
        {
            try
            {
                return await _usuariosRepositorio.DUpdate(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el usuario");
                var response = new Response<EntUsuarios>();
                response.SetError("Hubo un error al actualizar el usuario.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntUsuarios>> UpdateUserStatus(EntUsuarioUpdateEstatusRequest usuario)
        {
            try
            {
                EntUsuarios nUsuario = new EntUsuarios
                {
                    uId = usuario.uId,
                    bActivo = usuario.bEstatus
                };
                return await _usuariosRepositorio.DUpdateBoolean(nUsuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado del usuario");
                var response = new Response<EntUsuarios>();
                response.SetError("Hubo un error al actualizar el estado del usuario.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteUserById(Guid id)
        {
            try
            {
                return await _usuariosRepositorio.DUpdateEliminado(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el usuario por ID");
                var response = new Response<bool>();
                response.SetError("Hubo un error al eliminar el usuario.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntUsuarios>> GetUserById(Guid id)
        {
            try
            {
                return await _usuariosRepositorio.DGetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario por ID");
                var response = new Response<EntUsuarios>();
                response.SetError("Hubo un error al obtener el usuario.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntUsuarios>>> GetUsersByFilters(EntUsuarioSearchRequest filtros)
        {
            try
            {
                return await _usuariosRepositorio.DGetByFilters(filtros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los usuarios por filtros");
                var response = new Response<List<EntUsuarios>>();
                response.SetError("Hubo un error al obtener los usuarios.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntUsuarios>>> GetUserList()
        {
            try
            {
                return await _usuariosRepositorio.DGetList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de usuarios");
                var response = new Response<List<EntUsuarios>>();
                response.SetError("Hubo un error al obtener la lista de usuarios.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntUsuarios>> getLogin(EntUsuarioLoginRequest usuario)
        {
            var response = new Response<EntUsuarios>();

            try
            {
                if (string.IsNullOrWhiteSpace(usuario.sCorreo) ||
                    string.IsNullOrWhiteSpace(usuario.sContra))
                {
                    response.SetError("Los campos Correo y contraseña son obligatorios.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                return await _usuariosRepositorio.DLogin(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y actualizar el usuario");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
