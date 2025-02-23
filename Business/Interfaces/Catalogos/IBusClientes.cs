using Utils;
using Models.Models;
using Models.Request;
using Models.Request.Catalogo.Clientes;

namespace Business.Interfaces.Catalogos
{
    public interface IBusClientes
    {
        Task<Response<EntClientes>> ValidateAndSaveClient(EntClienteRequest cliente);
        Task<Response<EntClientes>> ValidateAndUpdateClient(EntClienteUpdateRequest cliente);
        Task<Response<EntClientes>> UpdateClientStatus(EntClienteUpdateEstatusRequest cliente);
        Task<Response<bool>> DeleteClientById(Guid id);
        Task<Response<EntClientes>> GetClientById(Guid id);
        Task<Response<PagedResult<EntClientes>>> GetClientsByFilters(EntClienteSearchRequest cliente);
        Task<Response<List<EntClientes>>> GetClientList();
    }
}
