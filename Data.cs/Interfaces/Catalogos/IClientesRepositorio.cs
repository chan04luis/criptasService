
using Entities;
using Entities.JsonRequest.Clientes;
using Entities.Models;

public interface IClientesRepositorio
{
    Task<Response<EntClientes>> DSave(EntClientes newItem);
    Task<Response<EntClientes>> DGetById(Guid iKey);
    Task<Response<List<EntClientes>>> DGetByFilters(EntClienteSearchRequest filtros);
    Task<Response<List<EntClientes>>> DGetList();
    Task<Response<EntClientes>> DUpdate(EntClientes entity);
    Task<Response<EntClientes>> DUpdateBoolean(EntClientes entity);
    Task<Response<bool>> DUpdateEliminado(Guid uId);
    Task<Response<List<EntClientes>>> DGetByEmail(string sEmail);
}