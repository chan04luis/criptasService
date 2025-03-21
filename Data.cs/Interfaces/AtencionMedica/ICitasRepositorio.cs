using Models.Models.AtencionMedica;
using Models.Request.AtencionMedica;
using Models.Responses.AtencionMedica;
using Utils;

namespace Data.cs.Interfaces.AtencionMedica
{
    public interface ICitasRepositorio
    {
        Task<Response<List<EntCitas>>> DGetList(CitasFiltroRequest filtro);
        Task<Response<EntCitaEditable>> DSave(CitaRequest entity);
        Task<Response<bool>> DUpdateCita(Guid id, CitaUpdateRequest request);
        Task<Response<bool>> DAtenderTurno(Guid idSucursal, Guid uIdSucursal);
        Task<Response<bool>> DRegistrarLlegada(Guid idCita);
        Task<Response<bool>> DRegistrarSalida(Guid idCita);
        Task<Response<EntCitaEditable>> DGetTurnoActual(Guid idSucursal);
        Task<Response<List<EntCitaEditable>>> DGetSiguientesTurnos(Guid idSucursal, int cantidad);
        Task<Response<bool>> DReagendarCita(Guid id, DateTime nuevaFecha);
        Task<Response<bool>> DCancelarCita(Guid id);
        Task<Response<bool>> DReasignarTurnos(Guid idSucursal, DateTime fecha);
        Task<Response<int>> DFinalizarCitasAtoradas(int tiempoLimiteMinutos);
        Task<Response<bool>> DAsignarPacienteSinCita(Guid idSucursal, Guid idCliente);
        Task<Response<bool>> DActualizarEstadoCita(Guid idCita, string nuevoEstado);
    }
}
