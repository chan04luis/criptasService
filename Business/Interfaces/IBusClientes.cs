using Entities.Models;
using Entities;
using Entities.JsonRequest.Clientes;

namespace Business.Interfaces
{
    public interface IBusClientes
    {
        Task<Response<EntClientes>> ValidateAndSaveClient(EntClienteRequest cliente);
        Task<Response<EntClientes>> ValidateAndUpdateClient(EntClienteUpdateRequest cliente);
        Task<Response<EntClientes>> UpdateClientStatus(EntClienteUpdateEstatusRequest cliente);
        Task<Response<bool>> DeleteClientById(Guid id);
        Task<Response<EntClientes>> GetClientById(Guid id);
        Task<Response<List<EntClientes>>> GetClientsByFilters(EntClienteSearchRequest cliente);
        Task<Response<List<EntClientes>>> GetClientList();
    }
}
