using Utils;
using Models.Models;
using Models.Request.Iglesias;
using Models.Responses.Iglesia;

namespace Business.Interfaces.Catalogos
{
    public interface IBusIglesias
    {
        Task<Response<EntIglesias>> ValidateAndSaveIglesia(EntIglesiaRequest iglesia);
        Task<Response<EntIglesias>> SaveIglesia(EntIglesias iglesia);
        Task<Response<EntIglesias>> ValidateAndUpdateIglesia(EntIglesiaUpdateRequest iglesia);
        Task<Response<EntIglesias>> UpdateIglesia(EntIglesias iglesia);
        Task<Response<EntIglesias>> UpdateIglesiaStatus(EntIglesiaUpdateEstatusRequest iglesia);
        Task<Response<bool>> DeleteIglesiaById(Guid id);
        Task<Response<EntIglesiaResponse>> GetIglesiaById(Guid id);
        Task<Response<List<EntIglesias>>> GetIglesiasByFilters(EntIglesiaSearchRequest filtros);
        Task<Response<List<EntIglesias>>> GetIglesiaList();
    }
}
