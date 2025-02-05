using Utils;
using Models.Models;
using Models.Request.Clientes;
using Models.Request;

namespace Business.Interfaces.Catalogos
{
    public interface IBusClientes
    {
        Task<Response<EntClientes>> ValidateAndSaveClientW(EntClienteRequest cliente);
        Task<Response<EntClientes>> ValidateAndSaveClientA(EntClienteRequest cliente);
        Task<Response<EntClientes>> ValidateAndUpdateClient(EntClienteUpdateRequest cliente);
        Task<Response<EntClientes>> UpdateClientStatus(EntClienteUpdateEstatusRequest cliente);
        Task<Response<bool>> DeleteClientById(Guid id);
        Task<Response<EntClientes>> GetClientById(Guid id);
        Task<Response<List<EntClientes>>> GetClientsByFilters(EntClienteSearchRequest cliente);
        Task<Response<List<EntClientes>>> GetClientList();
    }
}
