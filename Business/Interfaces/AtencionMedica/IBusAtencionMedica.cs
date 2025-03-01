using Models.Models.AtencionMedica;
using Models.Models;
using Models.Request.AtencionMedica;
using Models.Responses.AtencionMedica;
using Utils;

namespace Business.Interfaces.AtencionMedica
{
    public interface IBusAtencionMedica
    {
        Task<Response<List<EntCitas>>> ObtenerCitas(CitasFiltroRequest filtro);
        Task<Response<EntCitaEditable>> GuardarCita(CitaRequest request);
        Task<Response<bool>> ActualizarCita(Guid id, CitaUpdateRequest request);
        Task<Response<bool>> AtenderTurno(Guid idSucursal);
        Task<Response<bool>> RegistrarLlegada(Guid idCita);
        Task<Response<bool>> RegistrarSalida(Guid idCita);
        Task<Response<bool>> CancelarCita(Guid id);
        Task<Response<bool>> ReagendarCita(Guid id, DateTime nuevaFecha);
        Task<Response<int>> FinalizarCitasAtoradas(int tiempoLimiteMinutos);
        Task<Response<bool>> AsignarPacienteSinCita(Guid idSucursal, Guid idCliente);
        Task<Response<EntCitaEditable>> ObtenerTurnoActual(Guid idSucursal);
        Task<Response<List<EntCitaEditable>>> ObtenerSiguientesTurnos(Guid idSucursal, int cantidad);
        Task<Response<List<EntSalaConsulta>>> ObtenerSalasDisponibles(Guid idSucursal);
        Task<Response<bool>> RegistrarEntradaConsulta(Guid idDoctor, Guid idSucursal);
        Task<Response<bool>> RegistrarSalidaConsulta(Guid idDoctor, Guid idSucursal);
        Task<Response<List<EntPacienteEspera>>> ObtenerPacientesEnEspera(Guid idSucursal);
        Task<Response<bool>> RegistrarPacienteEnEspera(Guid idSucursal, Guid idCliente, Guid? idCita);
        Task<Response<bool>> ActualizarEstadoEspera(Guid idSalaEspera, bool atendido);
    }
}
