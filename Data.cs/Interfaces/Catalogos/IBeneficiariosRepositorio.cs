using Entities;

public interface IBeneficiariosRepositorio
{
    Task<Response<EntBeneficiarios>> DSave(EntBeneficiarios entity);
    Task<Response<EntBeneficiarios>> DUpdate(EntBeneficiarios entity);
    Task<Response<EntBeneficiarios>> DUpdateBoolean(EntBeneficiariosUpdateEstatusRequest entity);
    Task<Response<bool>> DUpdateEliminado(Guid uId);
    Task<Response<EntBeneficiarios>> DGetById(Guid iKey);
    Task<Response<List<EntBeneficiarios>>> DGetByFilters(EntBeneficiariosSearchRequest filtros);
    Task<Response<List<EntBeneficiarios>>> DGetList(Guid uIdCripta);
}