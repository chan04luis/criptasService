using Entities.Models;
using Entities.Request.TipoPagos;
using Entities;

namespace Business.Interfaces
{
    public interface IBusTiposPago
    {
        Task<Response<EntTiposPago>> ValidateAndSaveTipoPago(EntTiposPagoRequest tipoPago);
        Task<Response<EntTiposPago>> ValidateAndUpdateTipoPago(Guid uId, EntTiposPagoRequest tipoPago);
        Task<Response<EntTiposPago>> UpdateTipoPagoStatus(Guid uId, bool? bEstatus);
        Task<Response<bool>> DeleteTipoPagoById(Guid uId);
        Task<Response<EntTiposPago>> GetTipoPagoById(Guid uId);
        Task<Response<List<EntTiposPago>>> GetTiposPagoByFilters(EntTiposPagoSearchRequest filtros);
        Task<Response<List<EntTiposPago>>> GetList();
    }
}
