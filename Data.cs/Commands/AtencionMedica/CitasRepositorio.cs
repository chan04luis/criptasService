using Data.cs.Entities.AtencionMedica;
using Data.cs.Interfaces.AtencionMedica;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Models.AtencionMedica;
using Models.Request.AtencionMedica;
using Models.Responses.AtencionMedica;
using Utils;

namespace Data.cs.Commands.AtencionMedica
{
    public class CitasRepositorio : ICitasRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<CitasRepositorio> _logger;

        public CitasRepositorio(ApplicationDbContext dbContext, ILogger<CitasRepositorio> logger)
        {
            this.dbContext = dbContext;
            this._logger = logger;
        }

        public async Task<Response<List<EntCitas>>> DGetList(CitasFiltroRequest filtro)
        {
            var response = new Response<List<EntCitas>>();

            try
            {
                var citasQuery = from c in dbContext.Citas.AsNoTracking()
                                 join cli in dbContext.Clientes.AsNoTracking() on c.IdCliente equals cli.uId
                                 join s in dbContext.Sucursal.AsNoTracking() on c.IdSucursal equals s.uId
                                 join srv in dbContext.Servicios.AsNoTracking() on c.IdServicio equals srv.Id
                                 join d in dbContext.Usuarios.AsNoTracking() on c.IdDoctor equals d.uId into docJoin
                                 from d in docJoin.DefaultIfEmpty()
                                 where (filtro.Fecha == null || c.FechaCita.Date == filtro.Fecha.Value.Date) &&
                                       (filtro.FechaInicio == null || c.FechaCita >= filtro.FechaInicio) &&
                                       (filtro.FechaFin == null || c.FechaCita <= filtro.FechaFin) &&
                                       (filtro.Estado == null || c.Estado == filtro.Estado)
                                 orderby c.FechaCita
                                 select new EntCitas
                                 {
                                     Id = c.Id,
                                     Cliente = cli.sNombre + " " + cli.sApellidos,
                                     Sucursal = s.sNombre,
                                     Servicio = srv.Nombre,
                                     Doctor = d != null ? d.sNombres + " " + d.sApellidos : "Sin doctor asignado",
                                     FechaCita = c.FechaCita,
                                     Estado = c.Estado,
                                     IdCliente=cli.uId,
                                     IdDoctor=d.uId,
                                     IdServicio=srv.Id,
                                     IdSucursal=s.uId
                                 };

                var citas = await citasQuery.ToListAsync();
                response.SetSuccess(citas.Any() ? citas : null, citas.Any() ? "" : "Sin registros");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DGetList));
                response.SetError(ex);
            }

            return response;
        }

        public async Task<Response<EntCitaEditable>> DSave(CitaRequest entity)
        {
            var response = new Response<EntCitaEditable>();

            try
            {
                var turnoMax = await dbContext.Citas
                    .Where(c => c.IdSucursal == entity.IdSucursal && c.FechaCita.Date == entity.FechaCita.Date)
                    .MaxAsync(c => (int?)c.Turno) ?? 0;

                var newCita = new Citas
                {
                    Id = Guid.NewGuid(),
                    IdCliente = entity.IdCliente,
                    IdSucursal = entity.IdSucursal,
                    IdServicio = entity.IdServicio,
                    IdDoctor = entity.IdDoctor,
                    FechaCita = entity.FechaCita,
                    Estado = "pendiente",
                    Turno = turnoMax + 1,
                    FechaRegistro = DateTime.Now.ToLocalTime(),
                    FechaActualizacion = DateTime.Now.ToLocalTime()
                };

                dbContext.Citas.Add(newCita);
                await dbContext.SaveChangesAsync();

                var citaResponse = new EntCitaEditable
                {
                    Id = newCita.Id,
                    IdCliente = newCita.IdCliente,
                    IdSucursal = newCita.IdSucursal,
                    IdServicio = newCita.IdServicio,
                    IdDoctor = newCita.IdDoctor,
                    FechaCita = newCita.FechaCita,
                    Estado = newCita.Estado,
                    Turno = newCita.Turno,
                    RegistradoEnPiso = newCita.RegistradoEnPiso,
                    FechaRegistro = newCita.FechaRegistro,
                    FechaActualizacion = newCita.FechaActualizacion,
                    Cliente = await dbContext.Clientes.Where(c => c.uId == newCita.IdCliente).Select(c => c.sNombre + " " + c.sApellidos).FirstOrDefaultAsync(),
                    Sucursal = await dbContext.Sucursal.Where(s => s.uId == newCita.IdSucursal).Select(s => s.sNombre).FirstOrDefaultAsync(),
                    Servicio = await dbContext.Servicios.Where(s => s.Id == newCita.IdServicio).Select(s => s.Nombre).FirstOrDefaultAsync(),
                    Doctor = newCita.IdDoctor.HasValue
                        ? await dbContext.Usuarios.Where(d => d.uId == newCita.IdDoctor).Select(d => d.sNombres + " " + d.sApellidos).FirstOrDefaultAsync()
                        : "Sin doctor asignado"
                };

                response.SetSuccess(citaResponse, "Cita guardada con éxito");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DSave));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<bool>> DUpdateCita(Guid id, CitaUpdateRequest request)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var cita = await dbContext.Citas.FindAsync(id);
                if (cita == null)
                {
                    response.SetError("Cita no encontrada.");
                    return response;
                }

                cita.IdSucursal = request.IdSucursal;
                cita.IdServicio = request.IdServicio;
                cita.IdDoctor = request.IdDoctor;
                cita.FechaCita = request.FechaCita;
                cita.Estado = request.Estado;
                cita.FechaActualizacion = DateTime.UtcNow;

                dbContext.Update(cita);
                await dbContext.SaveChangesAsync();
                response.SetSuccess(true, "Cita actualizada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DUpdateCita));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<bool>> DAtenderTurno(Guid idSucursal)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var citaPendiente = await dbContext.Citas
                    .Where(x => x.IdSucursal == idSucursal && x.Estado == "pendiente")
                    .OrderBy(x => x.FechaCita)
                    .FirstOrDefaultAsync();

                if (citaPendiente == null)
                {
                    response.SetError("No hay citas pendientes.");
                    return response;
                }

                // Verificar si tiene médico asignado
                if (citaPendiente.IdDoctor == null)
                {
                    var doctorDisponible = await dbContext.SalaConsulta
                        .Where(sc => sc.IdSucursal == idSucursal && sc.FechaSalida == null)
                        .Select(sc => sc.IdDoctor)
                        .FirstOrDefaultAsync();

                    if (doctorDisponible == null)
                    {
                        response.SetError("No hay doctores disponibles.");
                        return response;
                    }

                    // Asignar el doctor disponible
                    citaPendiente.IdDoctor = doctorDisponible;
                }

                // Marcar la cita como "en proceso"
                citaPendiente.Estado = "en proceso";
                citaPendiente.FechaActualizacion = DateTime.UtcNow;

                dbContext.Update(citaPendiente);
                await dbContext.SaveChangesAsync();
                response.SetSuccess(true, "Cita en proceso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DAtenderTurno));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<bool>> DRegistrarLlegada(Guid idCita)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var cita = await dbContext.Citas.FindAsync(idCita);
                if (cita == null)
                {
                    response.SetError("Cita no encontrada.");
                    return response;
                }

                if (cita.Estado != "pendiente")
                {
                    response.SetError("La cita ya ha sido atendida o cancelada.");
                    return response;
                }

                cita.FechaLlegada = DateTime.UtcNow;
                cita.RegistradoEnPiso = true;
                cita.FechaActualizacion = DateTime.UtcNow;

                dbContext.Update(cita);
                await dbContext.SaveChangesAsync();
                response.SetSuccess(true, "Llegada registrada con éxito.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DRegistrarLlegada));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<bool>> DRegistrarSalida(Guid idCita)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var cita = await dbContext.Citas.FindAsync(idCita);
                if (cita == null)
                {
                    response.SetError("Cita no encontrada.");
                    return response;
                }

                if (cita.Estado != "en proceso")
                {
                    response.SetError("La cita no está en consulta.");
                    return response;
                }

                cita.FechaSalida = DateTime.UtcNow;
                cita.Estado = "finalizada";
                cita.FechaActualizacion = DateTime.UtcNow;

                dbContext.Update(cita);
                await dbContext.SaveChangesAsync();
                response.SetSuccess(true, "Salida registrada con éxito.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DRegistrarSalida));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntCitaEditable>> DGetTurnoActual(Guid idSucursal)
        {
            Response<EntCitaEditable> response = new Response<EntCitaEditable>();

            try
            {
                var citaActual = await dbContext.Citas
                    .Where(c => c.IdSucursal == idSucursal && c.Estado == "en proceso")
                    .OrderBy(c => c.FechaCita)
                    .Select(c => new EntCitaEditable
                    {
                        Id = c.Id,
                        IdCliente = c.IdCliente,
                        IdSucursal = c.IdSucursal,
                        IdServicio = c.IdServicio,
                        IdDoctor = c.IdDoctor,
                        FechaCita = c.FechaCita,
                        Estado = c.Estado,
                        Turno = c.Turno,
                        RegistradoEnPiso = c.RegistradoEnPiso,
                        FechaRegistro = c.FechaRegistro,
                        FechaActualizacion = c.FechaActualizacion,
                        Cliente = dbContext.Clientes.Where(cli => cli.uId == c.IdCliente).Select(cli => cli.sNombre + " " + cli.sApellidos).FirstOrDefault(),
                        Sucursal = dbContext.Sucursal.Where(s => s.uId == c.IdSucursal).Select(s => s.sNombre).FirstOrDefault(),
                        Servicio = dbContext.Servicios.Where(srv => srv.Id == c.IdServicio).Select(srv => srv.Nombre).FirstOrDefault(),
                        Doctor = c.IdDoctor.HasValue ? dbContext.Usuarios.Where(d => d.uId == c.IdDoctor).Select(d => d.sNombres + " " + d.sApellidos).FirstOrDefault() : "Sin doctor asignado"
                    })
                    .FirstOrDefaultAsync();

                response.SetSuccess(citaActual ?? null, citaActual != null ? "" : "No hay citas en proceso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DGetTurnoActual));
                response.SetError(ex);
            }

            return response;
        }

        public async Task<Response<List<EntCitaEditable>>> DGetSiguientesTurnos(Guid idSucursal, int cantidad)
        {
            Response<List<EntCitaEditable>> response = new Response<List<EntCitaEditable>>();

            try
            {
                var citasQuery = await dbContext.Citas
                    .Where(c => c.IdSucursal == idSucursal && c.Estado == "pendiente")
                    .OrderBy(c => c.Turno)
                    .Take(cantidad)
                    .Select(c => new EntCitaEditable
                    {
                        Id = c.Id,
                        IdCliente = c.IdCliente,
                        IdSucursal = c.IdSucursal,
                        IdServicio = c.IdServicio,
                        IdDoctor = c.IdDoctor,
                        FechaCita = c.FechaCita,
                        Estado = c.Estado,
                        Turno = c.Turno,
                        RegistradoEnPiso = c.RegistradoEnPiso,
                        FechaRegistro = c.FechaRegistro,
                        FechaActualizacion = c.FechaActualizacion,
                        Cliente = dbContext.Clientes.Where(cli => cli.uId == c.IdCliente).Select(cli => cli.sNombre + " " + cli.sApellidos).FirstOrDefault(),
                        Sucursal = dbContext.Sucursal.Where(s => s.uId == c.IdSucursal).Select(s => s.sNombre).FirstOrDefault(),
                        Servicio = dbContext.Servicios.Where(srv => srv.Id == c.IdServicio).Select(srv => srv.Nombre).FirstOrDefault(),
                        Doctor = c.IdDoctor.HasValue ? dbContext.Usuarios.Where(d => d.uId == c.IdDoctor).Select(d => d.sNombres + " " + d.sApellidos).FirstOrDefault() : "Sin doctor asignado"
                    })
                    .ToListAsync();

                response.SetSuccess(citasQuery.Any() ? citasQuery : null, citasQuery.Any() ? "" : "No hay citas en espera.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DGetSiguientesTurnos));
                response.SetError(ex);
            }

            return response;
        }

        public async Task<Response<bool>> DReagendarCita(Guid id, DateTime nuevaFecha)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var cita = await dbContext.Citas.FindAsync(id);
                if (cita == null)
                {
                    response.SetError("Cita no encontrada.");
                    return response;
                }

                if (cita.Estado != "pendiente")
                {
                    response.SetError("Solo se pueden reagendar citas pendientes.");
                    return response;
                }

                var turnoMax = await dbContext.Citas
                    .Where(c => c.IdSucursal == cita.IdSucursal && c.FechaCita.Date == nuevaFecha.Date)
                    .MaxAsync(c => (int?)c.Turno) ?? 0;

                cita.FechaCita = nuevaFecha;
                cita.Turno = turnoMax + 1;
                cita.FechaActualizacion = DateTime.UtcNow;

                dbContext.Update(cita);
                await dbContext.SaveChangesAsync();
                response.SetSuccess(true, "Cita reagendada correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DReagendarCita));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<bool>> DCancelarCita(Guid id)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var cita = await dbContext.Citas.FindAsync(id);
                if (cita == null)
                {
                    response.SetError("Cita no encontrada.");
                    return response;
                }

                if (cita.Estado != "pendiente")
                {
                    response.SetError("Solo se pueden cancelar citas pendientes.");
                    return response;
                }

                // Si la hora ya pasó y el paciente no llegó, se marca como "no presentado"
                cita.Estado = cita.FechaCita < DateTime.UtcNow ? "no presentado" : "cancelada";
                cita.FechaActualizacion = DateTime.UtcNow;

                dbContext.Update(cita);
                await dbContext.SaveChangesAsync();

                // Llamar al método para reasignar turnos
                await DReasignarTurnos(cita.IdSucursal, cita.FechaCita.Date);

                response.SetSuccess(true, "Cita cancelada correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DCancelarCita));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<bool>> DReasignarTurnos(Guid idSucursal, DateTime fecha)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var citasPendientes = await dbContext.Citas
                    .Where(c => c.IdSucursal == idSucursal && c.FechaCita.Date == fecha && c.Estado == "pendiente")
                    .OrderBy(c => c.Turno)
                    .ToListAsync();

                int nuevoTurno = 1;
                foreach (var cita in citasPendientes)
                {
                    cita.Turno = nuevoTurno++;
                    cita.FechaActualizacion = DateTime.UtcNow;
                    dbContext.Update(cita);
                }

                await dbContext.SaveChangesAsync();
                response.SetSuccess(true, "Turnos reasignados correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DReasignarTurnos));
                response.SetError(ex);
            }

            return response;
        }

        public async Task<Response<int>> DFinalizarCitasAtoradas(int tiempoLimiteMinutos)
        {
            Response<int> response = new Response<int>();

            try
            {
                var citasAtoradas = await dbContext.Citas
                    .Where(c => c.Estado == "en proceso" && c.FechaActualizacion < DateTime.UtcNow.AddMinutes(-tiempoLimiteMinutos))
                    .ToListAsync();

                foreach (var cita in citasAtoradas)
                {
                    cita.Estado = "finalizada";
                    cita.FechaSalida = DateTime.UtcNow;
                    dbContext.Update(cita);
                }

                await dbContext.SaveChangesAsync();
                response.SetSuccess(citasAtoradas.Count, $"{citasAtoradas.Count} citas finalizadas automáticamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DFinalizarCitasAtoradas));
                response.SetError(ex);
            }

            return response;
        }

        public async Task<Response<bool>> DAsignarPacienteSinCita(Guid idSucursal, Guid idCliente)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var doctorDisponible = await dbContext.SalaConsulta
                    .Where(sc => sc.IdSucursal == idSucursal && sc.FechaSalida == null)
                    .Select(sc => sc.IdDoctor)
                    .FirstOrDefaultAsync();

                if (doctorDisponible == null)
                {
                    response.SetError("No hay doctores disponibles en este momento.");
                    return response;
                }

                var nuevaCita = new Citas
                {
                    Id = Guid.NewGuid(),
                    IdCliente = idCliente,
                    IdSucursal = idSucursal,
                    IdServicio = Guid.Empty,  // Servicio desconocido
                    IdDoctor = doctorDisponible,
                    FechaCita = DateTime.UtcNow,
                    Estado = "en proceso",
                    FechaRegistro = DateTime.UtcNow,
                    FechaActualizacion = DateTime.UtcNow
                };

                dbContext.Citas.Add(nuevaCita);
                await dbContext.SaveChangesAsync();

                response.SetSuccess(true, "Paciente sin cita asignado a consulta.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DAsignarPacienteSinCita));
                response.SetError(ex);
            }

            return response;
        }

    }
}
