using Utils;
using Models.Models;
using Models.Request.Criptas;
using Models.Responses.Pagos;
using Models.Responses.Criptas;

namespace Business.Interfaces.Catalogos
{
    public interface IBusCriptas
    {
        Task<Response<EntCriptas>> ValidateAndSaveCripta(EntCriptaRequest cripta);
        Task<Response<EntCriptas>> ValidateAndUpdateCripta(EntCriptaUpdateRequest cripta);
        Task<Response<EntCriptas>> UpdateCriptaStatus(EntCriptaUpdateEstatusRequest cripta);
        Task<Response<bool>> DeleteCriptaById(Guid id);
        Task<Response<EntCriptas>> GetCriptaById(Guid id);
        Task<Response<PagedResult<EntCriptasLista>>> GetCriptasByFilters(EntCriptaSearchRequest filtros);
        Task<Response<List<EntCriptas>>> GetCriptaList(Guid uIdSeccion);
        Task<Response<List<EntCriptas>>> GetCriptaListDisponible(Guid uIdSeccion);
        Task<Response<List<CriptasDisponibles>>> BGetListDisponibleByIglesia(Guid uId);
        Task<Response<CriptasResumen>> GetResumenCriptas();
    }

}
