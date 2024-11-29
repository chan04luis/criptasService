using AutoMapper;
using Entities;
using Microsoft.Extensions.Logging;

namespace Business.Implementation
{
    public class BusBeneficiarios : IBusBeneficiarios
    {
        private readonly IBeneficiariosRepositorio _beneficiariosRepositorio;
        private readonly ILogger<BusBeneficiarios> _logger;
        private readonly IMapper _mapper;

        public BusBeneficiarios(IBeneficiariosRepositorio beneficiariosRepositorio, ILogger<BusBeneficiarios> logger, IMapper mapper)
        {
            _beneficiariosRepositorio = beneficiariosRepositorio;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<EntBeneficiarios>> SaveBeneficiary(EntBeneficiariosRequest beneficiario)
        {
            var response = new Response<EntBeneficiarios>();

            try
            {
                var newBeneficiary = _mapper.Map<EntBeneficiarios>(beneficiario);
                return await _beneficiariosRepositorio.DSave(newBeneficiary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el beneficiario");
                response.SetError("Hubo un error al guardar el beneficiario.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntBeneficiarios>> UpdateBeneficiary(EntBeneficiariosUpdateRequest beneficiario)
        {
            var response = new Response<EntBeneficiarios>();

            try
            {
                var updatedBeneficiary = _mapper.Map<EntBeneficiarios>(beneficiario);
                return await _beneficiariosRepositorio.DUpdate(updatedBeneficiary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el beneficiario");
                response.SetError("Hubo un error al actualizar el beneficiario.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntBeneficiarios>> GetBeneficiaryById(Guid uId)
        {
            try
            {
                return await _beneficiariosRepositorio.DGetById(uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el beneficiario por ID");
                var response = new Response<EntBeneficiarios>();
                response.SetError("Hubo un error al obtener el beneficiario.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntBeneficiarios>>> GetBeneficiariesByFilters(EntBeneficiariosSearchRequest filters)
        {
            try
            {
                return await _beneficiariosRepositorio.DGetByFilters(filters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los beneficiarios por filtros");
                var response = new Response<List<EntBeneficiarios>>();
                response.SetError("Hubo un error al obtener los beneficiarios.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteBeneficiary(Guid uId)
        {
            try
            {
                return await _beneficiariosRepositorio.DUpdateEliminado(uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el beneficiario");
                var response = new Response<bool>();
                response.SetError("Hubo un error al eliminar el beneficiario.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
