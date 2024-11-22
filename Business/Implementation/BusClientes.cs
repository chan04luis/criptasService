using Entities.Models;
using Entities;
using Utils.Interfaces;
using Business.Interfaces;
using Microsoft.Extensions.Logging;
using Entities.JsonRequest.Clientes;
using AutoMapper;

namespace Business.Implementation
{
    public class BusClientes : IBusClientes
    {
        private readonly IClientesRepositorio _clientesRepositorio;
        private readonly IFiltros _filtros;
        private readonly ILogger<BusClientes> _logger;
        private readonly IMapper _mapper;

        public BusClientes(IClientesRepositorio clientesRepositorio, IFiltros filtros, ILogger<BusClientes> logger, IMapper mapper)
        {
            _clientesRepositorio = clientesRepositorio;
            _filtros = filtros;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<EntClientes>> ValidateAndSaveClient(EntClienteRequest cliente)
        {
            var response = new Response<EntClientes>();

            try
            {
                if (string.IsNullOrWhiteSpace(cliente.sNombre) ||
                    string.IsNullOrWhiteSpace(cliente.sApellidos) ||
                    string.IsNullOrWhiteSpace(cliente.sEmail) ||
                    string.IsNullOrWhiteSpace(cliente.sTelefono) ||
                    string.IsNullOrWhiteSpace(cliente.sSexo) ||
                    string.IsNullOrWhiteSpace(cliente.sFechaNac))
                {
                    response.SetError("Los campos Nombre, Apellidos, Teléfono, Sexo, Fecha de nacimiento y Email son obligatorios.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                if (!_filtros.IsValidPhone(cliente.sTelefono))
                {
                    response.SetError("El número de teléfono debe tener 10 dígitos numéricos.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                if (!_filtros.IsValidEmail(cliente.sEmail))
                {
                    response.SetError("El formato del correo electrónico es inválido.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }
                if (!_filtros.IsValidFecha(cliente.sFechaNac))
                {
                    response.SetError("El formato de la fecha de nacimiento no es inválido.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }
                if (!_filtros.IsValidSexo(cliente.sSexo))
                {
                    response.SetError("El Sexo es inválido.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }
                var item = await _clientesRepositorio.DGetByEmail(cliente.sEmail);
                if (!item.HasError && item.Result.Count > 0)
                {
                    response.SetError("Email ya registrado.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }
                EntClientes nCliente = new EntClientes
                {
                    uId = Guid.NewGuid(),
                    sNombre = cliente.sNombre,
                    sApellidos = cliente.sApellidos,
                    sEmail = cliente.sEmail,
                    sTelefono = cliente.sTelefono,
                    sDireccion = cliente.sDireccion,
                    sSexo = cliente.sSexo,
                    sFechaNacimiento = cliente.sFechaNac,
                    bEstatus = true,
                    dtFechaActualizacion = DateTime.Now.ToLocalTime(),
                    dtFechaRegistro = DateTime.Now.ToLocalTime()
                };
                return await SaveClient(nCliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y guardar el cliente");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntClientes>> SaveClient(EntClientes cliente)
        {
            try
            {
                return await _clientesRepositorio.DSave(_mapper.Map<EntClientes>(cliente));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el cliente");
                var response = new Response<EntClientes>();
                response.SetError("Hubo un error al guardar el cliente.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntClientes>> ValidateAndUpdateClient(EntClienteUpdateRequest cliente)
        {
            var response = new Response<EntClientes>();

            try
            {
                if (string.IsNullOrWhiteSpace(cliente.sNombre) ||
                    string.IsNullOrWhiteSpace(cliente.sApellidos) ||
                    string.IsNullOrWhiteSpace(cliente.sEmail) ||
                    string.IsNullOrWhiteSpace(cliente.sTelefono) ||
                    string.IsNullOrWhiteSpace(cliente.sSexo) ||
                    string.IsNullOrWhiteSpace(cliente.sFechaNac))
                {
                    response.SetError("Los campos Nombre, Apellidos, Teléfono, Sexo, Fecha de nacimiento y Email son obligatorios.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                if (!_filtros.IsValidPhone(cliente.sTelefono))
                {
                    response.SetError("El número de teléfono debe tener 10 dígitos numéricos.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                if (!_filtros.IsValidEmail(cliente.sEmail))
                {
                    response.SetError("El formato del correo electrónico es inválido.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                if (!_filtros.IsValidFecha(cliente.sFechaNac))
                {
                    response.SetError("El formato de la fecha de nacimiento no es inválido.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }
                if (!_filtros.IsValidSexo(cliente.sSexo))
                {
                    response.SetError("El Sexo es inválido.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }
                var item = await _clientesRepositorio.DGetByEmail(cliente.sEmail);
                if (!item.HasError && item.Result.Where(x=>x.uId != cliente.uId).ToList().Count > 0)
                {
                    response.SetError("Email ya registrado.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }
                var nCliente = _mapper.Map<EntClientes>(cliente);
                return await UpdateClient(nCliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y actualizar el cliente");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntClientes>> UpdateClient(EntClientes cliente)
        {
            try
            {
                return await _clientesRepositorio.DUpdate(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el cliente");
                var response = new Response<EntClientes>();
                response.SetError("Hubo un error al actualizar el cliente.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntClientes>> UpdateClientStatus(EntClienteUpdateEstatusRequest cliente)
        {
            try
            {
                EntClientes nCliente = new EntClientes
                {
                    uId = cliente.uId,
                    bEstatus = cliente.bEstatus
                };
                return await _clientesRepositorio.DUpdateBoolean(nCliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado del cliente");
                var response = new Response<EntClientes>();
                response.SetError("Hubo un error al actualizar el estado del cliente.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteClientById(Guid id)
        {
            try
            {
                return await _clientesRepositorio.DUpdateEliminado(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el cliente por ID");
                var response = new Response<bool>();
                response.SetError("Hubo un error al eliminar el cliente.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntClientes>> GetClientById(Guid id)
        {
            try
            {
                return await _clientesRepositorio.DGetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el cliente por ID");
                var response = new Response<EntClientes>();
                response.SetError("Hubo un error al obtener el cliente.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntClientes>>> GetClientsByFilters(EntClienteSearchRequest filtros)
        {
            try
            {
                return await _clientesRepositorio.DGetByFilters(filtros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los clientes por filtros");
                var response = new Response<List<EntClientes>>();
                response.SetError("Hubo un error al obtener los clientes.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntClientes>>> GetClientList()
        {
            try
            {
                return await _clientesRepositorio.DGetList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de clientes");
                var response = new Response<List<EntClientes>>();
                response.SetError("Hubo un error al obtener la lista de clientes.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
