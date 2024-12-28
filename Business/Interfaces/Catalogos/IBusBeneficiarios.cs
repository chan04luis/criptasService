using Entities;

public interface IBusBeneficiarios
{
    Task<Response<EntBeneficiarios>> SaveBeneficiary(EntBeneficiariosRequest beneficiario);
    Task<Response<EntBeneficiarios>> UpdateBeneficiary(EntBeneficiariosUpdateRequest beneficiario);
    Task<Response<EntBeneficiarios>> GetBeneficiaryById(Guid uId);
    Task<Response<List<EntBeneficiarios>>> GetBeneficiariesByFilters(EntBeneficiariosSearchRequest filters);
    Task<Response<bool>> DeleteBeneficiary(Guid uId);
}
