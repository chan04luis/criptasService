using AutoMapper;
using Business.Data;
using Microsoft.Extensions.Logging;
using Business.Interfaces.Catalogos;
using Utils;
using Models.Models;
using Models.Request.Catalogo.Sucursales;

namespace Business.Implementation.Catalogos
{
    public class BusSucursal : IBusSucursal
    {
        private readonly ISucursalesRepositorio _repositorio;
        private readonly ILogger<BusSucursal> _logger;
        private readonly IMapper _mapper;

        public BusSucursal(ISucursalesRepositorio repositorio, ILogger<BusSucursal> logger, IMapper mapper)
        {
            _repositorio = repositorio;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<EntSucursal>> ValidateAndSave(EntSucursalRequest entity)
        {
            var response = new Response<EntSucursal>();

            try
            {
                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(entity.sNombre) ||
                    string.IsNullOrWhiteSpace(entity.sDireccion) ||
                    string.IsNullOrWhiteSpace(entity.sTelefono))
                {
                    response.SetError("Los campos Nombre, Télefono y Dirección son obligatorios.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                // Crear un nuevo objeto iglesia
                EntSucursal nEntity = new EntSucursal
                {
                    uId = Guid.NewGuid(),
                    sNombre = entity.sNombre,
                    sTelefono = entity.sTelefono,
                    sDireccion = entity.sDireccion,
                    sCiudad = entity.sCiudad,
                    bEstatus = true,
                    dtFechaRegistro = DateTime.Now.ToLocalTime(),
                    dtFechaActualizacion = DateTime.Now.ToLocalTime()
                };

                return await Save(nEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y guardar el registro");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntSucursal>> Save(EntSucursal entity)
        {
            try
            {
                return await _repositorio.DSave(_mapper.Map<EntSucursal>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar");
                var response = new Response<EntSucursal>();
                response.SetError("Hubo un error al guardar.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntSucursal>> ValidateAndUpdate(EntSucursalUpdateRequest entity)
        {
            var response = new Response<EntSucursal>();

            try
            {
                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(entity.sNombre) ||
                    string.IsNullOrWhiteSpace(entity.sTelefono) ||
                    string.IsNullOrWhiteSpace(entity.sDireccion))
                {
                    response.SetError("Los campos Nombre, Télefono y Dirección son obligatorios.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }
                var nEntity = _mapper.Map<EntSucursal>(entity);
                return await Update(nEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y actualizar");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntSucursal>> Update(EntSucursal entity)
        {
            try
            {
                return await _repositorio.DUpdate(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar");
                var response = new Response<EntSucursal>();
                response.SetError("Hubo un error al actualizar.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntSucursal>> UpdateMaps(EntSucursalMaps entity)
        {
            try
            {
                return await _repositorio.DUpdateMaps(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar");
                var response = new Response<EntSucursal>();
                response.SetError("Hubo un error al actualizar.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntSucursal>> UpdateStatus(EntSucursalUpdateEstatusRequest entity)
        {
            try
            {
                EntSucursal nEntity = new EntSucursal
                {
                    uId = entity.uId,
                    bEstatus = entity.bEstatus
                };
                return await _repositorio.DUpdateBoolean(nEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado");
                var response = new Response<EntSucursal>();
                response.SetError("Hubo un error al actualizar el estado.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteById(Guid id)
        {
            try
            {
                return await _repositorio.DUpdateEliminado(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar por ID");
                var response = new Response<bool>();
                response.SetError("Hubo un error al eliminar.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntSucursal>> GetById(Guid id)
        {
            try
            {
                return await _repositorio.DGetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener por ID");
                var response = new Response<EntSucursal>();
                response.SetError("Hubo un error al obtener.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntSucursal>>> GetByFilters(EntSucursalSearchRequest filtros)
        {
            try
            {
                return await _repositorio.DGetByFilters(filtros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener por filtros");
                var response = new Response<List<EntSucursal>>();
                response.SetError("Hubo un error al obtener.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntSucursal>>> GetList()
        {
            try
            {
                return await _repositorio.DGetList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista");
                var response = new Response<List<EntSucursal>>();
                response.SetError("Hubo un error al obtener la lista.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
