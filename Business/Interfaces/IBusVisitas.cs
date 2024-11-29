using Entities.Request.Visitas;
using Entities;

public interface IBusVisitas
{
    Task<Response<EntVisitas>> SaveVisit(EntVisitasRequest visita);
    Task<Response<EntVisitas>> UpdateVisit(EntVisitasRequest visita);
    Task<Response<EntVisitas>> GetVisitById(Guid uId);
    Task<Response<List<EntVisitas>>> GetVisitsByFilters(EntVisitasSearchRequest filters);
    Task<Response<bool>> DeleteVisit(Guid uId);
}