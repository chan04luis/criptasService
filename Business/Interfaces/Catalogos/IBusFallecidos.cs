
using Utils;
using Models.Models;
using Models.Request.Fallecidos;

namespace Business.Interfaces.Catalogos
{
    public interface IBusFallecidos
    {
        Task<Response<EntFallecidos>> SaveDeceased(EntFallecidosRequest fallecido);
        Task<Response<EntFallecidos>> UpdateDeceased(EntFallecidosUpdateRequest fallecido);
        Task<Response<List<EntFallecidos>>> GetDeceasedById(Guid uId);
        Task<Response<List<EntFallecidos>>> GetDeceasedByFilters(EntFallecidosSearchRequest filters);
        Task<Response<bool>> DeleteDeceased(Guid uId);
    }
}