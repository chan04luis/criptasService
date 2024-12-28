using Entities.Models;
using Entities;
namespace Business.Data
{
    public interface IPagosParcialesRepositorio
    {
        Task<Response<List<EntPagosParciales>>> DGetByPagoId(Guid uIdPago);
        Task<Response<EntPagosParciales>> DSave(EntPagosParciales entity);
        Task<Response<EntPagosParciales>> DUpdate(EntPagosParciales entity);
        Task<Response<bool>> DUpdateEliminado(Guid uId);
        Task<Response<EntPagosParciales>> DGetById(Guid uId);
        Task<Response<bool>> DUpdateEliminadoByIdPago(Guid uId);
    }

}
