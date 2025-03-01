using Models.Models;
using Utils;

namespace Data.cs.Interfaces.AtencionMedica
{
    public interface ISalaConsultaRepositorio
    {
        Task<Response<List<EntSalaConsulta>>> DGetSalasDisponibles(Guid idSucursal);
        Task<Response<bool>> DRegistrarEntradaConsulta(Guid idDoctor, Guid idSucursal);
        Task<Response<bool>> DRegistrarSalidaConsulta(Guid idDoctor, Guid idSucursal);
    }
}
