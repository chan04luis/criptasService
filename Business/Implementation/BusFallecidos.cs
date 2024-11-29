using AutoMapper;
using Entities.Request.Fallecidos;
using Entities;
using Microsoft.Extensions.Logging;

namespace Business.Implementation
{
    public class BusFallecidos : IBusFallecidos
    {
        private readonly IFallecidosRepositorio _fallecidosRepositorio;
        private readonly ILogger<BusFallecidos> _logger;
        private readonly IMapper _mapper;

        public BusFallecidos(IFallecidosRepositorio fallecidosRepositorio, ILogger<BusFallecidos> logger, IMapper mapper)
        {
            _fallecidosRepositorio = fallecidosRepositorio;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<EntFallecidos>> SaveDeceased(EntFallecidosRequest fallecido)
        {
            var response = new Response<EntFallecidos>();

            try
            {
                var newDeceased = _mapper.Map<EntFallecidos>(fallecido);
                return await _fallecidosRepositorio.DSave(newDeceased);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el fallecido");
                response.SetError("Hubo un error al guardar el fallecido.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntFallecidos>> UpdateDeceased(EntFallecidosUpdateRequest fallecido)
        {
            var response = new Response<EntFallecidos>();

            try
            {
                var updatedDeceased = _mapper.Map<EntFallecidos>(fallecido);
                return await _fallecidosRepositorio.DUpdate(updatedDeceased);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el fallecido");
                response.SetError("Hubo un error al actualizar el fallecido.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntFallecidos>> GetDeceasedById(Guid uId)
        {
            try
            {
                return await _fallecidosRepositorio.DGetById(uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el fallecido por ID");
                var response = new Response<EntFallecidos>();
                response.SetError("Hubo un error al obtener el fallecido.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntFallecidos>>> GetDeceasedByFilters(EntFallecidosSearchRequest filters)
        {
            try
            {
                return await _fallecidosRepositorio.DGetByFilters(filters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los fallecidos por filtros");
                var response = new Response<List<EntFallecidos>>();
                response.SetError("Hubo un error al obtener los fallecidos.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteDeceased(Guid uId)
        {
            try
            {
                return await _fallecidosRepositorio.DUpdateEliminado(uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el fallecido");
                var response = new Response<bool>();
                response.SetError("Hubo un error al eliminar el fallecido.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}