using Models.Models;
using Models.Request.Clientes;
using Models.Responses.Criptas;
using Utils;

public interface IClientesRepositorio
{
    Task<Response<EntClientes>> DSave(EntClientes newItem);
    Task<Response<EntClientes>> DGetById(Guid iKey);
    Task<Response<PagedResult<EntClientes>>> DGetByFilters(EntClienteSearchRequest filtros);
    Task<Response<List<EntClientes>>> DGetList();
    Task<Response<EntClientes>> DUpdate(EntClientes entity);
    Task<Response<EntClientes>> DUpdateBoolean(EntClientes entity);
    Task<Response<EntClientes>> DUpdateToken(Guid uId, String? sTokenFireBase);
    Task<Response<bool>> DUpdateEliminado(Guid uId);
    Task<Response<EntClientes>> DGetByEmail(string sEmail, string? Contra = null);
    Task<Response<List<MisCriptas>>> DGetMisCriptas(Guid uIdCliente);
    Task<Response<MisCriptas>> DGetMisCripta(Guid uIdCripta);
}