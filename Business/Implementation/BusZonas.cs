using AutoMapper;
using Business.Data;
using Business.Interfaces;
using Entities.JsonRequest.Zonas;
using Entities.Models;
using Entities;
using Microsoft.Extensions.Logging;
using Utils.Interfaces;

namespace Business.Implementation
{
    public class BusZonas : IBusZonas
    {
        private readonly IZonasRepositorio _zonasRepositorio;
        private readonly IFiltros _filtros;
        private readonly ILogger<BusZonas> _logger;
        private readonly IMapper _mapper;

        public BusZonas(IZonasRepositorio zonasRepositorio, IFiltros filtros, ILogger<BusZonas> logger, IMapper mapper)
        {
            _zonasRepositorio = zonasRepositorio;
            _filtros = filtros;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<EntZonas>> ValidateAndSaveZone(EntZonaRequest zona)
        {
            var response = new Response<EntZonas>();

            try
            {
                if (string.IsNullOrWhiteSpace(zona.sNombre))
                {
                    response.SetError("Los campos Nombre es obligatorio.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var item = await _zonasRepositorio.DGetByName(zona.sNombre, zona.uIdIglesia);
                if (!item.HasError && item.Result.Count > 0)
                {
                    response.SetError("Zona ya registrada con ese nombre.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                EntZonas nuevaZona = new EntZonas
                {
                    uId = Guid.NewGuid(),
                    uIdIglesia = zona.uIdIglesia,
                    sNombre = zona.sNombre,
                    bEstatus = true,
                    dtFechaRegistro = DateTime.Now.ToLocalTime(),
                    dtFechaActualizacion = DateTime.Now.ToLocalTime()
                };

                return await SaveZone(nuevaZona);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y guardar la zona");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntZonas>> SaveZone(EntZonas zona)
        {
            try
            {
                return await _zonasRepositorio.DSave(_mapper.Map<EntZonas>(zona));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la zona");
                var response = new Response<EntZonas>();
                response.SetError("Hubo un error al guardar la zona.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntZonas>> ValidateAndUpdateZone(EntZonaUpdateRequest zona)
        {
            var response = new Response<EntZonas>();

            try
            {
                if (string.IsNullOrWhiteSpace(zona.sNombre))
                {
                    response.SetError("Los campos Nombre es obligatorio.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var item = await _zonasRepositorio.DGetByName(zona.sNombre, zona.uIdIglesia);
                if (!item.HasError && item.Result.Where(x => x.uId != zona.uId).ToList().Count > 0)
                {
                    response.SetError("Zona ya registrada con ese nombre.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var zonaActualizada = _mapper.Map<EntZonas>(zona);
                return await UpdateZone(zonaActualizada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y actualizar la zona");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntZonas>> UpdateZone(EntZonas zona)
        {
            try
            {
                return await _zonasRepositorio.DUpdate(zona);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la zona");
                var response = new Response<EntZonas>();
                response.SetError("Hubo un error al actualizar la zona.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntZonas>> UpdateZoneStatus(EntZonaUpdateEstatusRequest zona)
        {
            try
            {
                EntZonas zonaActualizada = new EntZonas
                {
                    uId = zona.uId,
                    bEstatus = zona.bEstatus
                };
                return await _zonasRepositorio.DUpdateBoolean(zonaActualizada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado de la zona");
                var response = new Response<EntZonas>();
                response.SetError("Hubo un error al actualizar el estado de la zona.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteZoneById(Guid id)
        {
            try
            {
                return await _zonasRepositorio.DUpdateEliminado(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la zona por ID");
                var response = new Response<bool>();
                response.SetError("Hubo un error al eliminar la zona.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntZonas>> GetZoneById(Guid id)
        {
            try
            {
                return await _zonasRepositorio.DGetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la zona por ID");
                var response = new Response<EntZonas>();
                response.SetError("Hubo un error al obtener la zona.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntZonas>>> GetZonesByFilters(EntZonaSearchRequest filtros)
        {
            try
            {
                return await _zonasRepositorio.DGetByFilters(filtros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las zonas por filtros");
                var response = new Response<List<EntZonas>>();
                response.SetError("Hubo un error al obtener las zonas.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntZonas>>> GetZoneList(Guid uIdIglesia)
        {
            try
            {
                return await _zonasRepositorio.DGetList(uIdIglesia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de zonas");
                var response = new Response<List<EntZonas>>();
                response.SetError("Hubo un error al obtener la lista de zonas.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }

}
