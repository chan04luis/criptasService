using AutoMapper;
using Business.Data;
using Entities.JsonRequest.Iglesias;
using Entities.Models;
using Entities;
using Microsoft.Extensions.Logging;
using Utils.Interfaces;
using Business.Interfaces;
using Entities.Responses.Iglesia;

namespace Business.Implementation
{
    public class BusIglesias : IBusIglesias
    {
        private readonly IIglesiasRepositorio _iglesiasRepositorio;
        private readonly IFiltros _filtros;
        private readonly ILogger<BusIglesias> _logger;
        private readonly IMapper _mapper;

        public BusIglesias(IIglesiasRepositorio iglesiasRepositorio, IFiltros filtros, ILogger<BusIglesias> logger, IMapper mapper)
        {
            _iglesiasRepositorio = iglesiasRepositorio;
            _filtros = filtros;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<EntIglesias>> ValidateAndSaveIglesia(EntIglesiaRequest iglesia)
        {
            var response = new Response<EntIglesias>();

            try
            {
                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(iglesia.sNombre) ||
                    string.IsNullOrWhiteSpace(iglesia.sDireccion))
                {
                    response.SetError("Los campos Nombre y Dirección son obligatorios.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                // Crear un nuevo objeto iglesia
                EntIglesias nIglesia = new EntIglesias
                {
                    uId = Guid.NewGuid(),
                    sNombre = iglesia.sNombre,
                    sDireccion = iglesia.sDireccion,
                    sCiudad = iglesia.sCiudad,
                    bEstatus = true, 
                    dtFechaRegistro = DateTime.Now.ToLocalTime(),
                    dtFechaActualizacion = DateTime.Now.ToLocalTime()
                };

                return await SaveIglesia(nIglesia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y guardar la iglesia");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntIglesias>> SaveIglesia(EntIglesias iglesia)
        {
            try
            {
                return await _iglesiasRepositorio.DSave(_mapper.Map<EntIglesias>(iglesia));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la iglesia");
                var response = new Response<EntIglesias>();
                response.SetError("Hubo un error al guardar la iglesia.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntIglesias>> ValidateAndUpdateIglesia(EntIglesiaUpdateRequest iglesia)
        {
            var response = new Response<EntIglesias>();

            try
            {
                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(iglesia.sNombre) ||
                    string.IsNullOrWhiteSpace(iglesia.sDireccion))
                {
                    response.SetError("Los campos Nombre y Dirección son obligatorios.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                // Crear el objeto iglesia actualizado
                var nIglesia = _mapper.Map<EntIglesias>(iglesia);
                return await UpdateIglesia(nIglesia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y actualizar la iglesia");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntIglesias>> UpdateIglesia(EntIglesias iglesia)
        {
            try
            {
                return await _iglesiasRepositorio.DUpdate(iglesia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la iglesia");
                var response = new Response<EntIglesias>();
                response.SetError("Hubo un error al actualizar la iglesia.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntIglesias>> UpdateIglesiaStatus(EntIglesiaUpdateEstatusRequest iglesia)
        {
            try
            {
                EntIglesias nIglesia = new EntIglesias
                {
                    uId = iglesia.uId,
                    bEstatus = iglesia.bEstatus
                };
                return await _iglesiasRepositorio.DUpdateBoolean(nIglesia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado de la iglesia");
                var response = new Response<EntIglesias>();
                response.SetError("Hubo un error al actualizar el estado de la iglesia.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteIglesiaById(Guid id)
        {
            try
            {
                return await _iglesiasRepositorio.DUpdateEliminado(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la iglesia por ID");
                var response = new Response<bool>();
                response.SetError("Hubo un error al eliminar la iglesia.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntIglesiaResponse>> GetIglesiaById(Guid id)
        {
            try
            {
                return await _iglesiasRepositorio.DGetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la iglesia por ID");
                var response = new Response<EntIglesiaResponse>();
                response.SetError("Hubo un error al obtener la iglesia.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntIglesias>>> GetIglesiasByFilters(EntIglesiaSearchRequest filtros)
        {
            try
            {
                return await _iglesiasRepositorio.DGetByFilters(filtros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las iglesias por filtros");
                var response = new Response<List<EntIglesias>>();
                response.SetError("Hubo un error al obtener las iglesias.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntIglesias>>> GetIglesiaList()
        {
            try
            {
                return await _iglesiasRepositorio.DGetList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de iglesias");
                var response = new Response<List<EntIglesias>>();
                response.SetError("Hubo un error al obtener la lista de iglesias.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
