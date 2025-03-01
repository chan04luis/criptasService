using Models.Responses.AtencionMedica;
using Utils;

namespace Data.cs.Interfaces.AtencionMedica
{
    public interface ISalaEsperaRepositorio
    {
        Task<Response<List<EntPacienteEspera>>> DGetPacientesEnEspera(Guid idSucursal);
        Task<Response<bool>> DRegistrarEnEspera(Guid idSucursal, Guid idCliente, Guid? idCita);
        Task<Response<bool>> DActualizarEstadoEspera(Guid idSalaEspera, bool atendido);
    }
}
