using Data.cs.Entities.AtencionMedica;
using Data.cs.Interfaces.AtencionMedica;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Responses.AtencionMedica;
using Utils;
namespace Data.cs.Commands.AtencionMedica
{
    public class SalaEsperaRepositorio : ISalaEsperaRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<SalaEsperaRepositorio> _logger;

        public SalaEsperaRepositorio(ApplicationDbContext dbContext, ILogger<SalaEsperaRepositorio> logger)
        {
            this.dbContext = dbContext;
            this._logger = logger;
        }

        public async Task<Response<List<EntPacienteEspera>>> DGetPacientesEnEspera(Guid idSucursal)
        {
            var response = new Response<List<EntPacienteEspera>>();

            try
            {
                var pacientes = await dbContext.SalaEspera
                    .Where(se => se.IdSucursal == idSucursal && !se.Atendido)
                    .OrderBy(se => se.Turno)
                    .Select(se => new EntPacienteEspera
                    {
                        IdSalaEspera = se.Id,
                        IdCita = se.IdCita,
                        Cliente = dbContext.Clientes
                            .Where(cli => cli.uId == se.IdCliente)
                            .Select(cli => cli.sNombre + " " + cli.sApellidos)
                            .FirstOrDefault(),
                        Servicio = dbContext.Servicios
                            .Where(s => dbContext.Citas.Any(c => c.Id == se.IdCita && c.IdServicio == s.Id))
                            .Select(s => s.Nombre)
                            .FirstOrDefault(),
                        Turno = se.Turno,
                        FechaIngreso = se.FechaIngreso
                    })
                    .ToListAsync();

                response.SetSuccess(pacientes.Any() ? pacientes : null, pacientes.Any() ? "" : "No hay pacientes en espera.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DGetPacientesEnEspera));
                response.SetError(ex);
            }

            return response;
        }

        public async Task<Response<bool>> DRegistrarEnEspera(Guid idSucursal, Guid idCliente, Guid? idCita)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                int turnoMax;
                using (var transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    turnoMax = await dbContext.SalaEspera
                        .Where(se => se.IdSucursal == idSucursal)
                        .OrderByDescending(se => se.Turno)
                        .Select(se => se.Turno)
                        .FirstOrDefaultAsync();

                    var salaEspera = new SalaEspera
                    {
                        Id = Guid.NewGuid(),
                        IdSucursal = idSucursal,
                        IdCita = idCita,
                        IdCliente = idCliente,
                        Turno = turnoMax + 1,
                        FechaIngreso = DateTime.UtcNow,
                        FechaRegistro = DateTime.UtcNow
                    };
                    var pacienteEnEspera = await dbContext.SalaEspera
                        .AnyAsync(se => se.IdSucursal == idSucursal && se.IdCliente == idCliente && !se.Atendido);

                    if (pacienteEnEspera)
                    {
                        response.SetError("El paciente ya tiene un turno en espera.");
                        return response;
                    }


                    dbContext.SalaEspera.Add(salaEspera);
                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }

                response.SetSuccess(true, "Paciente registrado en sala de espera.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DRegistrarEnEspera));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<bool>> DActualizarEstadoEspera(Guid idSalaEspera, bool atendido)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var salaEspera = await dbContext.SalaEspera.FindAsync(idSalaEspera);
                if (salaEspera == null)
                {
                    response.SetError("Registro en sala de espera no encontrado.");
                    return response;
                }

                salaEspera.Atendido = atendido;
                salaEspera.FechaLlamado = atendido ? DateTime.UtcNow : null;
                salaEspera.FechaRegistro = DateTime.UtcNow;

                dbContext.Update(salaEspera);
                await dbContext.SaveChangesAsync();
                response.SetSuccess(true, "Estado de sala de espera actualizado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DActualizarEstadoEspera));
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<bool>> DNegotiateSalaEspera(Guid idSucursal)
        {
            var response = new Response<bool>();
            try
            {
                // Verificar si la sucursal existe antes de continuar
                var sucursalExiste = await dbContext.Sucursal.AnyAsync(s => s.uId == idSucursal);
                if (!sucursalExiste)
                {
                    response.SetError("La sucursal especificada no existe.");
                    return response;
                }

                // Aquí se debe agregar la lógica para la negociación con SignalR si es necesario

                response.SetSuccess(true, "Negociación de sala de espera completada con éxito.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DNegotiateSalaEspera));
                response.SetError(ex);
            }

            return response;
        }
    }
}
