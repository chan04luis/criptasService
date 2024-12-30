using Modelos.Models;
using Modelos.Request.Criptas;
using Utils;

namespace Business.Data
{
    public interface ICriptasRepositorio
    {
        Task<Response<EntCriptas>> DSave(EntCriptas entity);
        Task<Response<EntCriptas>> DUpdate(EntCriptas entity);
        Task<Response<EntCriptas>> DUpdateBoolean(EntCriptas entity);
        Task<Response<bool>> DUpdateEliminado(Guid uId);
        Task<Response<EntCriptas>> DGetById(Guid iKey);
        Task<Response<List<EntCriptas>>> DGetByFilters(EntCriptaSearchRequest filtros);
        Task<Response<List<EntCriptas>>> DGetList(Guid uIdSeccion);
        Task<Response<List<EntCriptas>>> DGetByName(string nombre, Guid uIdSeccion);
    }

}
