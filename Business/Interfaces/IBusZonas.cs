using Entities.JsonRequest.Zonas;
using Entities.Models;
using Entities;
namespace Business.Interfaces
{
    public interface IBusZonas
    {
        Task<Response<EntZonas>> ValidateAndSaveZone(EntZonaRequest zona);
        Task<Response<EntZonas>> ValidateAndUpdateZone(EntZonaUpdateRequest zona);
        Task<Response<EntZonas>> UpdateZoneStatus(EntZonaUpdateEstatusRequest zona);
        Task<Response<bool>> DeleteZoneById(Guid id);
        Task<Response<EntZonas>> GetZoneById(Guid id);
        Task<Response<List<EntZonas>>> GetZonesByFilters(EntZonaSearchRequest filtros);
        Task<Response<List<EntZonas>>> GetZoneList(Guid uIdIglesia);
    }
}
