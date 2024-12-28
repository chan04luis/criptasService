using Entities.JsonRequest.Zonas;
using Entities.Models;
using Entities;

namespace Business.Data
{
    public interface IZonasRepositorio
    {
        Task<Response<EntZonas>> DSave(EntZonas newItem);
        Task<Response<EntZonas>> DGetById(Guid iKey);
        Task<Response<List<EntZonas>>> DGetByFilters(EntZonaSearchRequest filtros);
        Task<Response<List<EntZonas>>> DGetByName(string name, Guid uIdIglesia);
        Task<Response<List<EntZonas>>> DGetList(Guid uIdIglesia);
        Task<Response<EntZonas>> DUpdate(EntZonas entity);
        Task<Response<EntZonas>> DUpdateBoolean(EntZonas entity);
        Task<Response<bool>> DUpdateEliminado(Guid uId);
        Task<Response<List<EntZonas>>> DGetByIglesiaId(Guid iglesiaId);
    }
}
