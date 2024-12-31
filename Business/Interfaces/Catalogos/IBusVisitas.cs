using Utils;
using Models.Models;
using Models.Request.Visitas;
namespace Business.Interfaces.Catalogos
{
    public interface IBusVisitas
    {
        Task<Response<EntVisitas>> SaveVisit(EntVisitasRequest visita);
        Task<Response<EntVisitas>> UpdateVisit(EntVisitasRequest visita);
        Task<Response<EntVisitas>> GetVisitById(Guid uId);
        Task<Response<List<EntVisitas>>> GetVisitsByFilters(EntVisitasSearchRequest filters);
        Task<Response<bool>> DeleteVisit(Guid uId);
    }
}