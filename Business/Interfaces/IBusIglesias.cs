using Entities.JsonRequest.Iglesias;
using Entities.Models;
using Entities;

namespace Business.Interfaces
{
    public interface IBusIglesias
    {
        Task<Response<EntIglesias>> ValidateAndSaveIglesia(EntIglesiaRequest iglesia);
        Task<Response<EntIglesias>> SaveIglesia(EntIglesias iglesia);
        Task<Response<EntIglesias>> ValidateAndUpdateIglesia(EntIglesiaUpdateRequest iglesia);
        Task<Response<EntIglesias>> UpdateIglesia(EntIglesias iglesia);
        Task<Response<EntIglesias>> UpdateIglesiaStatus(EntIglesiaUpdateEstatusRequest iglesia);
        Task<Response<bool>> DeleteIglesiaById(Guid id);
        Task<Response<EntIglesias>> GetIglesiaById(Guid id);
        Task<Response<List<EntIglesias>>> GetIglesiasByFilters(EntIglesiaSearchRequest filtros);
        Task<Response<List<EntIglesias>>> GetIglesiaList();
    }
}
