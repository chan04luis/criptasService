using Entities;
using Entities.Models;
using Entities.JsonRequest;

namespace Business.Data
{
    public interface IBeneficiariosRepositorio
    {
        Task<Response<EntBeneficiarios>> DSave (EntBeneficiarios entBeneficiario);
        Task<Response<EntBeneficiarios>> DUpdate (EntBeneficiarios entBeneficiarios);
        Task<Response<EntBeneficiarios>> DUpdateBolean (EntBeneficiarios entBeneficiarios);
        Task<Response<bool>> DUpdateEliminado (Guid id);
        Task<Response<EntBeneficiarios>> DGetById(Guid id);
        Task DeleteById (Guid id);
    }
}
