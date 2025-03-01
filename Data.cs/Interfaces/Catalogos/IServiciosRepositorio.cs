using Models.Models;
using Models.Responses.Servicio;
using Utils;

namespace Data.cs.Interfaces.Catalogos
{
    public interface IServiciosRepositorio
    {
        Task<Response<bool>> AnyExistKey(Guid pKey);
        Task<Response<bool>> AnyExitNameAndKey(EntServicios pEntity);
        Task<Response<bool>> DAnyExistName(string nombre);
        Task<Response<EntServicios>> DSave(EntServicios entity);
        Task<Response<EntServicios>> DUpdate(EntServicios entity);
        Task<Response<EntServicios>> DUpdateEstatus(EntServicios entity);
        Task<Response<bool>> DDelete(Guid uId);
        Task<Response<EntServicios>> DGetById(Guid iKey);
        Task<Response<List<EntServicios>>> DGetList();
        Task<Response<List<EntServicios>>> DGetListActive();
        Task<Response<List<EntServiceItem>>> DGetListActive(Guid uId);
        Task<Response<List<EntServiceItem>>> DGetListPreAssigment(Guid uId);
        Task<Response<bool>> DSaveToSucursal(List<EntServiceItem> entities, Guid uId);
        Task<Response<List<EntServiceItem>>> DGetListPreAssigmentUser(Guid uId);
        Task<Response<bool>> DSaveToUser(List<EntServiceItem> entities, Guid uId);
    }
}
