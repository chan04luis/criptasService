using Modelos.Models;
using Modelos.Request.Iglesias;
using Modelos.Responses.Iglesia;
using Utils;

namespace Business.Data
{
    public interface IIglesiasRepositorio
    {
        Task<Response<EntIglesias>> DSave(EntIglesias entity);
        Task<Response<EntIglesias>> DUpdate(EntIglesias entity);
        Task<Response<EntIglesias>> DUpdateBoolean(EntIglesias entity);
        Task<Response<bool>> DUpdateEliminado(Guid uId);
        Task<Response<EntIglesiaResponse>> DGetById(Guid iKey);
        Task<Response<List<EntIglesias>>> DGetByFilters(EntIglesiaSearchRequest filtros);
        Task<Response<List<EntIglesias>>> DGetList();
    }
}
