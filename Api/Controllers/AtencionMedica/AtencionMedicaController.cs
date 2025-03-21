using Business.Interfaces.AtencionMedica;
using Utils;
using Models.Models.AtencionMedica;
using Models.Request.AtencionMedica;
using Models.Responses.AtencionMedica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Models.Models;
using Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Api.Controllers.AtencionMedica
{
    [Route("api/AtencionMedica")]
    [Authorize]
    [ApiController]
    public class AtencionMedicaController : ControllerBase
    {
        private readonly IBusAtencionMedica _busAtencionMedica;
        private readonly ILogger<AtencionMedicaController> _logger;
        private readonly IHubContext<SalaEsperaHub> _hubContext;

        public AtencionMedicaController(IBusAtencionMedica busAtencionMedica, ILogger<AtencionMedicaController> logger, IHubContext<SalaEsperaHub> hubContext)
        {
            _busAtencionMedica = busAtencionMedica;
            _logger = logger;
            _hubContext = hubContext;
        }

        [HttpPost("Citas/Crear")]
        [SwaggerOperation(Summary = "Crea una cita médica", Description = "Registra una nueva cita en el sistema.")]
        public async Task<Response<EntCitaEditable>> CrearCita([FromBody] CitaRequest request)
        {
            _logger.LogInformation("Iniciando creación de cita.");
            var response = await _busAtencionMedica.GuardarCita(request);
            return response;
        }

        [HttpPut("Citas/Actualizar/{id}")]
        [SwaggerOperation(Summary = "Actualiza una cita", Description = "Modifica los detalles de una cita existente.")]
        public async Task<Response<bool>> ActualizarCita(Guid id, [FromBody] CitaUpdateRequest request)
        {
            _logger.LogInformation("Actualizando cita con ID: {Id}", id);
            return await _busAtencionMedica.ActualizarCita(id, request);
        }

        [HttpPost("Citas/Filtrar")]
        [SwaggerOperation(Summary = "Obtiene citas por filtros", Description = "Recupera una lista de citas filtradas por los parámetros dados.")]
        public async Task<Response<List<EntCitas>>> ObtenerCitas([FromBody] CitasFiltroRequest filtro)
        {
            var idDoctor = new Guid(User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);
            filtro.uIdDoctor = idDoctor;
            _logger.LogInformation("Obteniendo citas con filtros.");
            return await _busAtencionMedica.ObtenerCitas(filtro);
        }

        [HttpPut("Citas/Reagendar/{id}")]
        [SwaggerOperation(Summary = "Reagenda una cita", Description = "Cambia la fecha de una cita existente.")]
        public async Task<Response<bool>> ReagendarCita(Guid id, [FromBody] DateTime nuevaFecha)
        {
            _logger.LogInformation("Reagendando cita con ID: {Id}", id);
            return await _busAtencionMedica.ReagendarCita(id, nuevaFecha);
        }

        [HttpPut("Citas/Cancelar/{id}")]
        [SwaggerOperation(Summary = "Cancela una cita", Description = "Marca una cita como cancelada.")]
        public async Task<Response<bool>> CancelarCita(Guid id)
        {
            _logger.LogInformation("Cancelando cita con ID: {Id}", id);
            var response = await _busAtencionMedica.CancelarCita(id);

            if (!response.HasError)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveUpdate", $"La cita con ID {id} ha sido cancelada.");
            }

            return response;
        }

        [HttpPut("Citas/Atender")]
        [SwaggerOperation(Summary = "Atiende una cita", Description = "Asigna un doctor disponible y marca la cita como en proceso.")]
        public async Task<Response<bool>> AtenderTurno([FromBody] CitasGenericIdsRequest entity)
        {
            _logger.LogInformation("Atendiendo turno en sucursal: {IdSucursal}", entity.idSucursal);
            var response = await _busAtencionMedica.AtenderTurno(entity);

            if (!response.HasError)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveUpdate", "Un paciente ha sido atendido.");
            }

            return response;
        }

        [HttpPut("Citas/Llegada/{idCita}")]
        [SwaggerOperation(Summary = "Registra la llegada de un paciente", Description = "Marca la llegada de un paciente a su cita.")]
        public async Task<Response<bool>> RegistrarLlegada(Guid idCita)
        {
            _logger.LogInformation("Registrando llegada de paciente a cita con ID: {IdCita}", idCita);
            var response = await _busAtencionMedica.RegistrarLlegada(idCita);

            if (!response.HasError)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveUpdate", $"El paciente con cita {idCita} ha llegado.");
            }

            return response;
        }

        [HttpPut("Citas/Salida/{idCita}")]
        [SwaggerOperation(Summary = "Registra la salida de un paciente", Description = "Marca la salida de un paciente después de su cita.")]
        public async Task<Response<bool>> RegistrarSalida(Guid idCita)
        {
            _logger.LogInformation("Registrando salida de paciente de cita con ID: {IdCita}", idCita);
            return await _busAtencionMedica.RegistrarSalida(idCita);
        }

        [HttpGet("Citas/TurnoActual/{idSucursal}")]
        [SwaggerOperation(Summary = "Obtiene el turno actual", Description = "Devuelve el turno actual en la sucursal especificada.")]
        public async Task<Response<EntCitaEditable>> ObtenerTurnoActual(Guid idSucursal)
        {
            _logger.LogInformation("Obteniendo turno actual en sucursal: {IdSucursal}", idSucursal);
            return await _busAtencionMedica.ObtenerTurnoActual(idSucursal);
        }

        [HttpPost("SalaEspera/Registrar")]
        [SwaggerOperation(Summary = "Registra un paciente en espera", Description = "Agrega un paciente a la lista de espera.")]
        public async Task<Response<bool>> RegistrarPacienteEnEspera([FromBody] Guid idSucursal, Guid idCliente, Guid? idCita)
        {
            _logger.LogInformation("Registrando paciente en espera en sucursal: {IdSucursal}", idSucursal);
            return await _busAtencionMedica.RegistrarPacienteEnEspera(idSucursal, idCliente, idCita);
        }

        [HttpGet("SalaEspera/Pacientes/{idSucursal}")]
        [SwaggerOperation(Summary = "Obtiene pacientes en espera", Description = "Devuelve la lista de pacientes en espera en una sucursal.")]
        public async Task<Response<List<EntPacienteEspera>>> ObtenerPacientesEnEspera(Guid idSucursal)
        {
            _logger.LogInformation("Obteniendo pacientes en espera en sucursal: {IdSucursal}", idSucursal);
            return await _busAtencionMedica.ObtenerPacientesEnEspera(idSucursal);
        }

        [HttpPut("SalaEspera/ActualizarEstado/{idSalaEspera}")]
        [SwaggerOperation(Summary = "Actualiza el estado de espera", Description = "Marca un paciente como atendido en la sala de espera.")]
        public async Task<Response<bool>> ActualizarEstadoEspera(Guid idSalaEspera, [FromBody] bool atendido)
        {
            _logger.LogInformation("Actualizando estado de paciente en espera con ID: {IdSalaEspera}", idSalaEspera);
            return await _busAtencionMedica.ActualizarEstadoEspera(idSalaEspera, atendido);
        }

        [HttpGet("SalasDisponibles/{idSucursal}")]
        [SwaggerOperation(Summary = "Obtiene salas disponibles", Description = "Devuelve la lista de salas de consulta disponibles.")]
        public async Task<Response<List<EntSalaConsulta>>> ObtenerSalasDisponibles(Guid idSucursal)
        {
            _logger.LogInformation("Obteniendo salas disponibles en sucursal: {IdSucursal}", idSucursal);
            return await _busAtencionMedica.ObtenerSalasDisponibles(idSucursal);
        }

        [HttpPost("Salas/EntradaConsulta")]
        [SwaggerOperation(Summary = "Registra la entrada a una sala de consulta", Description = "Asigna un doctor a una sala de consulta.")]
        public async Task<Response<bool>> RegistrarEntradaConsulta([FromBody] CitasGenericIdsRequest entity)
        {
            var idDoctor = new Guid(User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);
            _logger.LogInformation("Registrando entrada de doctor con ID: {IdDoctor} en sucursal: {IdSucursal}", idDoctor, entity.idSucursal);
            return await _busAtencionMedica.RegistrarEntradaConsulta(idDoctor, entity.idSucursal.Value);
        }

        [HttpPut("Salas/SalidaConsulta")]
        [SwaggerOperation(Summary = "Registra la salida de una sala de consulta", Description = "Libera una sala de consulta asignada a un doctor.")]
        public async Task<Response<bool>> RegistrarSalidaConsulta([FromBody] Guid idDoctor, Guid idSucursal)
        {
            _logger.LogInformation("Registrando salida de doctor con ID: {IdDoctor} en sucursal: {IdSucursal}", idDoctor, idSucursal);
            return await _busAtencionMedica.RegistrarSalidaConsulta(idDoctor, idSucursal);
        }
    }
}
