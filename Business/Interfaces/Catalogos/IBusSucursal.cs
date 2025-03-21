using Utils;
using Models.Models;
using Models.Request.Catalogo.Sucursales;
using Models.Responses.Servicio;

namespace Business.Interfaces.Catalogos
{
    public interface IBusSucursal
    {
        Task<Response<EntSucursal>> ValidateAndSave(EntSucursalRequest entity);
        Task<Response<EntSucursal>> Save(EntSucursal entity);
        Task<Response<EntSucursal>> ValidateAndUpdate(EntSucursalUpdateRequest entity);
        Task<Response<EntSucursal>> Update(EntSucursal entity);
        Task<Response<EntSucursal>> UpdateMaps(EntSucursalMaps entity);
        Task<Response<EntSucursal>> UpdateStatus(EntSucursalUpdateEstatusRequest entity);
        Task<Response<bool>> DeleteById(Guid id);
        Task<Response<EntSucursal>> GetById(Guid id);
        Task<Response<List<EntSucursal>>> GetByFilters(EntSucursalSearchRequest entity);
        Task<Response<List<EntSucursal>>> GetList();
        Task<Response<List<EntServiceItem>>> BGetListPreAssigmentUser(Guid uId);
        Task<Response<bool>> BSaveToUser(List<EntServiceItem> entities, Guid uId);
        Task<Response<List<EntSucursal>>> GetByIdUser(Guid id);
    }
}
