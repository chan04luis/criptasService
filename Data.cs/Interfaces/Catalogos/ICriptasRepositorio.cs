using Models.Models;
using Models.Request.Criptas;
using Models.Responses.Pagos;
using Utils;

namespace Business.Data
{
    public interface ICriptasRepositorio
    {
        Task<Response<EntCriptas>> DSave(EntCriptas entity);
        Task<Response<EntCriptas>> DUpdate(EntCriptas entity);
        Task<Response<EntCriptas>> DUpdateBoolean(EntCriptas entity);
        Task<Response<EntCriptas>> DUpdateDisponible(EntCriptas entity);
        Task<Response<bool>> DUpdateEliminado(Guid uId);
        Task<Response<EntCriptas>> DGetById(Guid iKey);
        Task<Response<PagedResult<EntCriptasLista>>> DGetByFilters(EntCriptaSearchRequest filtros);
        Task<Response<List<EntCriptas>>> DGetList(Guid uIdSeccion);
        Task<Response<List<EntCriptas>>> DGetListDisponible(Guid uIdSeccion);
        Task<Response<List<EntCriptas>>> DGetByName(string nombre, Guid uIdSeccion);
    }

}
