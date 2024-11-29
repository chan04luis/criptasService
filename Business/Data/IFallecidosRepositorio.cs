using Entities;
using Entities.Request.Fallecidos;

public interface IFallecidosRepositorio
{
    Task<Response<EntFallecidos>> DSave(EntFallecidos entity);
    Task<Response<EntFallecidos>> DUpdate(EntFallecidos entity);
    Task<Response<bool>> DUpdateEliminado(Guid uId);
    Task<Response<EntFallecidos>> DGetById(Guid uId);
    Task<Response<List<EntFallecidos>>> DGetByFilters(EntFallecidosSearchRequest filtros);
    Task<Response<EntFallecidos>> DUpdateBoolean(EntFallecidosUpdateEstatusRequest entity);
    Task<Response<List<EntFallecidos>>> DGetList(Guid uIdCripta);
}
