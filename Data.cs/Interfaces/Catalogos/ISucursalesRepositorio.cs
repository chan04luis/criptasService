using Models.Models;
using Models.Request.Catalogo.Sucursales;
using Utils;

namespace Business.Data
{
    public interface ISucursalesRepositorio
    {
        Task<Response<EntSucursal>> DSave(EntSucursal entity);
        Task<Response<EntSucursal>> DUpdate(EntSucursal entity);
        Task<Response<EntSucursal>> DUpdateMaps(EntSucursalMaps entity);
        Task<Response<EntSucursal>> DUpdateBoolean(EntSucursal entity);
        Task<Response<bool>> DUpdateEliminado(Guid uId);
        Task<Response<EntSucursal>> DGetById(Guid iKey);
        Task<Response<List<EntSucursal>>> DGetByFilters(EntSucursalSearchRequest filtros);
        Task<Response<List<EntSucursal>>> DGetList();
    }
}
