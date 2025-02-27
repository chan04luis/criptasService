using Models.Models;
using Models.Request.Pagos;
using Models.Responses.Pagos;
using Utils;

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
        Task<Response<PagedResult<EntPagosLista>>> DGetByFilters(EntPagosSearchRequest filtros);
        Task<Response<EntSolicitudPago>> DSaveInfoPago(EntSolicitudPago entity);
        Task<Response<EntSolicitudPago>> DUpdateStatusInfoPago(EntSolicitudPago entity);
        Task<Response<EntSolicitudPago>> DGetInfoById(Guid uId);
    }
}
