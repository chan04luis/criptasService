using Entities.Models;
using Entities.Request.Criptas;
using Entities;

namespace Business.Interfaces
{
    public interface IBusCriptas
    {
        Task<Response<EntCriptas>> ValidateAndSaveCripta(EntCriptaRequest cripta);
        Task<Response<EntCriptas>> ValidateAndUpdateCripta(EntCriptaUpdateRequest cripta);
        Task<Response<EntCriptas>> UpdateCriptaStatus(EntCriptaUpdateEstatusRequest cripta);
        Task<Response<bool>> DeleteCriptaById(Guid id);
        Task<Response<EntCriptas>> GetCriptaById(Guid id);
        Task<Response<List<EntCriptas>>> GetCriptasByFilters(EntCriptaSearchRequest filtros);
        Task<Response<List<EntCriptas>>> GetCriptaList(Guid uIdSeccion);
    }

}
