using Entities.Models;
using Entities.Request.Secciones;
using Entities;

namespace Business.Data
{
    public interface ISeccionesRepositorio
    {
        Task<Response<EntSecciones>> DSave(EntSecciones entity);
        Task<Response<EntSecciones>> DUpdate(EntSecciones entity);
        Task<Response<EntSecciones>> DUpdateBoolean(EntSecciones entity);
        Task<Response<bool>> DUpdateEliminado(Guid uId);
        Task<Response<EntSecciones>> DGetById(Guid iKey);
        Task<Response<List<EntSecciones>>> DGetByName(string sNombre, Guid uIdZona);
        Task<Response<List<EntSecciones>>> DGetByFilters(EntSeccionSearchRequest filtros);
        Task<Response<List<EntSecciones>>> DGetList(Guid uIdZona);
    }

}
