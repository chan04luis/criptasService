using Entities.Models;
using Entities.Request.Pagos;
using Entities;

namespace Business.Interfaces
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
        Task<Response<List<EntPagos>>> GetPagosByFilters(EntPagosSearchRequest filtros);
        Task<Response<List<EntPagosParciales>>> GetParcialidadesByIdPago(Guid id);
    }
}
