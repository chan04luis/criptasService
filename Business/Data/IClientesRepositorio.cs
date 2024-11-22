
using Entities;
using Entities.Models;

public interface IClientesRepositorio
{
    Task<Response<EntClientes>> DSave(EntClientes newItem);
    Task<Response<EntClientes>> DGetById(Guid iKey);
    Task<Response<List<EntClientes>>> DGetByName(string nombre);
    Task<Response<List<EntClientes>>> DGetList();
    Task<Response<EntClientes>> DUpdate(EntClientes entity);
    Task<Response<EntClientes>> DUpdateBoolean(EntClientes entity);
    Task<Response<List<EntClientes>>> DGetByEmail(string sEmail);
}