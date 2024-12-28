using Entities.Request.Fallecidos;
using Entities;

public interface IBusFallecidos
{
    Task<Response<EntFallecidos>> SaveDeceased(EntFallecidosRequest fallecido);
    Task<Response<EntFallecidos>> UpdateDeceased(EntFallecidosUpdateRequest fallecido);
    Task<Response<EntFallecidos>> GetDeceasedById(Guid uId);
    Task<Response<List<EntFallecidos>>> GetDeceasedByFilters(EntFallecidosSearchRequest filters);
    Task<Response<bool>> DeleteDeceased(Guid uId);
}