using Data.cs.Entities.AtencionMedica;
using Data.cs.Interfaces.AtencionMedica;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Models;
using Utils;

namespace Data.cs.Commands.AtencionMedica
{
    public class SalaConsultaRepositorio : ISalaConsultaRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<SalaConsultaRepositorio> _logger;

        public SalaConsultaRepositorio(ApplicationDbContext dbContext, ILogger<SalaConsultaRepositorio> logger)
        {
            this.dbContext = dbContext;
            this._logger = logger;
        }

        public async Task<Response<List<EntSalaConsulta>>> DGetSalasDisponibles(Guid idSucursal)
        {
            var response = new Response<List<EntSalaConsulta>>();

            try
            {
                var salas = await dbContext.SalaConsulta
                    .Where(sc => sc.IdSucursal == idSucursal && sc.FechaSalida == null)
                    .Select(sc => new EntSalaConsulta
                    {
                        Id = sc.Id,
                        IdSucursal = sc.IdSucursal,
                        IdDoctor = sc.IdDoctor,
                        FechaEntrada = sc.FechaEntrada,
                        Disponible = sc.FechaSalida == null
                    })
                    .ToListAsync();

                response.SetSuccess(salas.Any() ? salas : null, salas.Any() ? "" : "No hay salas disponibles.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DGetSalasDisponibles));
                response.SetError(ex);
            }

            return response;
        }

        public async Task<Response<bool>> DRegistrarEntradaConsulta(Guid idDoctor, Guid idSucursal)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var salaExistente = await dbContext.SalaConsulta
                    .FirstOrDefaultAsync(sc => sc.IdDoctor == idDoctor && sc.FechaSalida == null);

                if (salaExistente != null)
                {
                    response.SetError("El doctor ya tiene una sala activa en otra sucursal.");
                    return response;
                }

                var nuevaSala = new SalaConsulta
                {
                    Id = Guid.NewGuid(),
                    IdSucursal = idSucursal,
                    IdDoctor = idDoctor,
                    FechaEntrada = DateTime.UtcNow,
                    FechaRegistro = DateTime.UtcNow
                };

                dbContext.SalaConsulta.Add(nuevaSala);
                await dbContext.SaveChangesAsync();

                response.SetSuccess(true, "Doctor registrado en la sala de consulta.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DRegistrarEntradaConsulta));
                response.SetError(ex);
            }

            return response;
        }

        public async Task<Response<bool>> DRegistrarSalidaConsulta(Guid idDoctor, Guid idSucursal)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var sala = await dbContext.SalaConsulta
                    .FirstOrDefaultAsync(sc => sc.IdDoctor == idDoctor && sc.IdSucursal == idSucursal && sc.FechaSalida == null);

                if (sala == null)
                {
                    response.SetError("No se encontró una sala activa para el doctor.");
                    return response;
                }

                // Validar si hay citas en proceso con este doctor
                var citasEnProceso = await dbContext.Citas
                    .AnyAsync(c => c.IdDoctor == idDoctor && c.Estado == "en proceso");

                if (citasEnProceso)
                {
                    response.SetError("No puedes salir de la consulta mientras haya pacientes en proceso.");
                    return response;
                }

                sala.FechaSalida = DateTime.UtcNow;
                dbContext.Update(sala);
                await dbContext.SaveChangesAsync();

                response.SetSuccess(true, "Doctor ha salido de la sala de consulta.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en {MethodName}", nameof(DRegistrarSalidaConsulta));
                response.SetError(ex);
            }

            return response;
        }

    }
}
