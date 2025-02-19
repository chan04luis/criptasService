
using Utils;
using Models.Models;
using Models.Request.Fallecidos;
using Models.Responses.Criptas;

namespace Business.Interfaces.Catalogos
{
    public interface IBusFallecidos
    {
        Task<Response<EntFallecidos>> SaveDeceased(EntFallecidosRequest fallecido);
        Task<Response<EntFallecidos>> UpdateDeceased(EntFallecidosUpdateRequest fallecido);
        Task<Response<EntFallecidos>> UpdateDocs(EntFallecidos fallecido);
        Task<Response<List<EntFallecidos>>> GetDeceasedById(Guid uId);
        Task<Response<PagedResult<FallecidosBusqueda>>> BGetFallecidos(EntFallecidosSearchRequest fallecido);
        Task<Response<EntFallecidos>> GetSingleById(Guid uId);
        Task<Response<List<EntFallecidos>>> GetDeceasedByFilters(EntFallecidosSearchRequest filters);
        Task<Response<bool>> DeleteDeceased(Guid uId);
    }
}