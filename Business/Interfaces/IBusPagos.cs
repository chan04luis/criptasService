using Entities.Models;
using Entities.Request.Pagos;
using Entities;

namespace Business.Interfaces
{
    public interface IBusPagos
    {
        Task<Response<EntPagos>> ValidateAndSavePago(EntPagosRequest pago);
        Task<Response<EntPagos>> ValidateAndUpdatePago(Guid uId, EntPagosRequest pago);
        Task<Response<EntPagos>> UpdatePagado(EntPagosUpdatePagadoRequest pago);
        Task<Response<EntPagos>> UpdatePagoStatus(EntPagosUpdateEstatusRequest pago);
        Task<Response<bool>> DeletePagoById(Guid uId);
        Task<Response<EntPagos>> GetPagoById(Guid uId);
        Task<Response<List<EntPagos>>> GetPagosByFilters(EntPagosSearchRequest filtros);
    }
}
