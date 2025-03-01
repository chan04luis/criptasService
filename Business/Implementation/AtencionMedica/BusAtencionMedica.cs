using Data.cs.Interfaces.AtencionMedica;
using Microsoft.Extensions.Logging;
using Models.Models.AtencionMedica;
using Models.Models;
using Models.Request.AtencionMedica;
using Models.Responses.AtencionMedica;
using Utils;
using Business.Interfaces.AtencionMedica;

namespace Business.Implementation.AtencionMedica
{
    public class BusAtencionMedica : IBusAtencionMedica
    {
        private readonly ICitasRepositorio _citasRepositorio;
        private readonly ISalaConsultaRepositorio _salaConsultaRepositorio;
        private readonly ISalaEsperaRepositorio _salaEsperaRepositorio;
        private readonly ILogger<BusAtencionMedica> _logger;

        public BusAtencionMedica(
            ICitasRepositorio citasRepositorio,
            ISalaConsultaRepositorio salaConsultaRepositorio,
            ISalaEsperaRepositorio salaEsperaRepositorio,
            ILogger<BusAtencionMedica> logger)
        {
            _citasRepositorio = citasRepositorio;
            _salaConsultaRepositorio = salaConsultaRepositorio;
            _salaEsperaRepositorio = salaEsperaRepositorio;
            _logger = logger;
        }

        public async Task<Response<List<EntCitas>>> ObtenerCitas(CitasFiltroRequest filtro)
        {
            return await _citasRepositorio.DGetList(filtro);
        }

        public async Task<Response<EntCitaEditable>> GuardarCita(CitaRequest request)
        {
            return await _citasRepositorio.DSave(request);
        }

        public async Task<Response<bool>> ActualizarCita(Guid id, CitaUpdateRequest request)
        {
            return await _citasRepositorio.DUpdateCita(id, request);
        }

        public async Task<Response<bool>> AtenderTurno(Guid idSucursal)
        {
            var salaDisponible = await _salaConsultaRepositorio.DGetSalasDisponibles(idSucursal);
            if (salaDisponible.Result == null || !salaDisponible.Result.Any())
            {
                var resp = new Response<bool>();
                resp.SetError("No hay salas de consulta disponibles.");
                return resp;
            }

            return await _citasRepositorio.DAtenderTurno(idSucursal);
        }

        public async Task<Response<bool>> RegistrarLlegada(Guid idCita)
        {
            return await _citasRepositorio.DRegistrarLlegada(idCita);
        }

        public async Task<Response<bool>> RegistrarSalida(Guid idCita)
        {
            return await _citasRepositorio.DRegistrarSalida(idCita);
        }

        public async Task<Response<bool>> CancelarCita(Guid id)
        {
            try
            {
                var cita = await _citasRepositorio.DCancelarCita(id);
                if (cita.HasError)
                {
                    var resp = new Response<bool>();
                    resp.SetError("Error al cancelar la cita: " + cita.Message);
                    return resp;
                }

                return cita;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(CancelarCita));
                var resp = new Response<bool>();
                resp.SetError("Hubo un error al procesar la cancelación de la cita.");
                return resp;
            }
        }

        public async Task<Response<bool>> ReagendarCita(Guid id, DateTime nuevaFecha)
        {
            return await _citasRepositorio.DReagendarCita(id, nuevaFecha);
        }

        public async Task<Response<int>> FinalizarCitasAtoradas(int tiempoLimiteMinutos)
        {
            return await _citasRepositorio.DFinalizarCitasAtoradas(tiempoLimiteMinutos);
        }

        public async Task<Response<bool>> AsignarPacienteSinCita(Guid idSucursal, Guid idCliente)
        {
            return await _citasRepositorio.DAsignarPacienteSinCita(idSucursal, idCliente);
        }

        public async Task<Response<EntCitaEditable>> ObtenerTurnoActual(Guid idSucursal)
        {
            return await _citasRepositorio.DGetTurnoActual(idSucursal);
        }

        public async Task<Response<List<EntCitaEditable>>> ObtenerSiguientesTurnos(Guid idSucursal, int cantidad)
        {
            return await _citasRepositorio.DGetSiguientesTurnos(idSucursal, cantidad);
        }

        public async Task<Response<List<EntSalaConsulta>>> ObtenerSalasDisponibles(Guid idSucursal)
        {
            return await _salaConsultaRepositorio.DGetSalasDisponibles(idSucursal);
        }

        public async Task<Response<bool>> RegistrarEntradaConsulta(Guid idDoctor, Guid idSucursal)
        {
            return await _salaConsultaRepositorio.DRegistrarEntradaConsulta(idDoctor, idSucursal);
        }

        public async Task<Response<bool>> RegistrarSalidaConsulta(Guid idDoctor, Guid idSucursal)
        {
            return await _salaConsultaRepositorio.DRegistrarSalidaConsulta(idDoctor, idSucursal);
        }

        public async Task<Response<List<EntPacienteEspera>>> ObtenerPacientesEnEspera(Guid idSucursal)
        {
            return await _salaEsperaRepositorio.DGetPacientesEnEspera(idSucursal);
        }

        public async Task<Response<bool>> RegistrarPacienteEnEspera(Guid idSucursal, Guid idCliente, Guid? idCita)
        {
            if (idSucursal == Guid.Empty || idCliente == Guid.Empty)
            {
                var resp = new Response<bool>();
                resp.SetError("Sucursal y cliente son obligatorios.");
                return resp;
            }

            return await _salaEsperaRepositorio.DRegistrarEnEspera(idSucursal, idCliente, idCita);
        }

        public async Task<Response<bool>> ActualizarEstadoEspera(Guid idSalaEspera, bool atendido)
        {
            return await _salaEsperaRepositorio.DActualizarEstadoEspera(idSalaEspera, atendido);
        }
    }
}
