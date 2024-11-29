
using AutoMapper;
using Entities.Request.Visitas;
using Entities;
using Microsoft.Extensions.Logging;

namespace Business.Implementation
{
    public class BusVisitas : IBusVisitas
    {
        private readonly IVisitasRepositorio _visitasRepositorio;
        private readonly ILogger<BusVisitas> _logger;
        private readonly IMapper _mapper;

        public BusVisitas(IVisitasRepositorio visitasRepositorio, ILogger<BusVisitas> logger, IMapper mapper)
        {
            _visitasRepositorio = visitasRepositorio;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<EntVisitas>> SaveVisit(EntVisitasRequest visita)
        {
            var response = new Response<EntVisitas>();

            try
            {
                EntVisitas entVisita = new EntVisitas
                {
                    uId = Guid.NewGuid(),
                    uIdCriptas = visita.uIdCriptas,
                    bEstatus = true,
                    dtFechaRegistro = DateTime.Now.ToLocalTime(),
                    sNombreVisitante = visita.sNombreVisitante
                };

                return await _visitasRepositorio.DSave(entVisita);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la visita");
                response.SetError("Hubo un error al guardar la visita.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntVisitas>> UpdateVisit(EntVisitasRequest visita)
        {
            var response = new Response<EntVisitas>();

            try
            {
                var updatedVisit = _mapper.Map<EntVisitas>(visita);
                return await _visitasRepositorio.DUpdate(updatedVisit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la visita");
                response.SetError("Hubo un error al actualizar la visita.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntVisitas>> GetVisitById(Guid uId)
        {
            try
            {
                return await _visitasRepositorio.DGetById(uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la visita por ID");
                var response = new Response<EntVisitas>();
                response.SetError("Hubo un error al obtener la visita.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntVisitas>>> GetVisitsByFilters(EntVisitasSearchRequest filters)
        {
            try
            {
                return await _visitasRepositorio.DGetByFilters(filters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las visitas por filtros");
                var response = new Response<List<EntVisitas>>();
                response.SetError("Hubo un error al obtener las visitas.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteVisit(Guid uId)
        {
            try
            {
                return await _visitasRepositorio.DUpdateEliminado(uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la visita");
                var response = new Response<bool>();
                response.SetError("Hubo un error al eliminar la visita.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
