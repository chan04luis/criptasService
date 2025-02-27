using Models.Models;
using Utils;
using Models.Request.Pagos;
using Models.Responses.Pagos;

namespace Business.Interfaces.Catalogos
{
    public interface IBusPagos
    {
        Task<Response<EntPagos>> ValidateAndSavePago(EntPagosRequest pago);
        Task<Response<EntPagos>> ValidateAndUpdatePago(Guid uId, EntPagosRequest pago);
        Task<Response<EntPagos>> UpdatePagado(ReadPagosRequest pago);
        Task<Response<EntPagos>> UpdateCancelarPagado(Guid uIdPago);
        Task<Response<EntPagos>> UpdatePagoStatus(EntPagosUpdateEstatusRequest pago);
        Task<Response<bool>> DeletePagoById(Guid uId);
        Task<Response<bool>> DeletePagoParcialById(Guid uId);
        Task<Response<EntPagos>> GetPagoById(Guid uId);
        Task<Response<PagedResult<EntPagosLista>>> GetPagosByFilters(EntPagosSearchRequest filtros);
        Task<Response<List<EntPagosParciales>>> GetParcialidadesByIdPago(Guid id);
        Task<Response<List<EntPagos>>> DGetByClienteId(Guid uIdCliente, Guid uIdCripta);
        Task<Response<EntSolicitudPago>> SaveInfoPago(EntSolicitudPago entity);
        Task<Response<EntSolicitudPago>> UpdateInfoPago(EntSolicitudPago entity);
    }
}
