
using Entities.Models;
using Entities.Request.Pagos;
using Entities;

namespace Business.Data
{
    public interface IPagosRepositorio
    {
        Task<Response<List<EntPagos>>> DGetByClienteId(Guid uIdCliente);
        Task<Response<EntPagos>> DSave(EntPagos entity);
        Task<Response<EntPagos>> DUpdate(EntPagos entity);
        Task<Response<EntPagos>> DUpdatePagado(EntPagosUpdatePagadoRequest pago);
        Task<Response<EntPagos>> DUpdateStatus(EntPagosUpdateEstatusRequest entity);
        Task<Response<bool>> DUpdateEliminado(Guid uId);
        Task<Response<EntPagos>> DGetById(Guid uId);
        Task<Response<List<EntPagos>>> DGetByFilters(EntPagosSearchRequest filtros);
    }
}
