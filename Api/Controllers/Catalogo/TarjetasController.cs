using Data.cs;
using Data.cs.Entities.Catalogos;
using Data.cs.Entities.Control;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace Api.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetasController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public TarjetasController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<Response<Tarjeta>> Create([FromBody] Tarjeta entity)
        {
            Response<Tarjeta> response = new Response<Tarjeta>();
            var i = await _db.Tarjeta.Where(x => x.Id == entity.Id).CountAsync();
            if(i > 0)
            {
                response.SetError("Ya existe");
            }
            else
            {
                _db.Tarjeta.Add(entity);
                if ((await _db.SaveChangesAsync()) > 0)
                {
                    response.SetSuccess(entity);
                }
                else
                {
                    response.SetError("Error al guardar");
                }
            }
            return response;
        }

        [HttpPost("Entrada")]
        public async Task<Response<dynamic>> Validar([FromBody] Tarjeta entity)
        {
            var response = new Response<dynamic>();
            var tarjeta = await _db.Tarjeta.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (tarjeta == null)
                return response.GetError("No existe Tarjeta");

            if (!tarjeta.Estado)
                return response.GetError("Tarjeta no activa");

            var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.uId == tarjeta.IdUsuario);
            if (usuario == null)
                return response.GetError("Usuario no asignado");

            bool esMaestro = usuario.uIdPerfil == new Guid("f579984f-8dd5-490f-bb7a-e99c994449c1");

            var ahora = DateTime.Now;
            var diaSemana = (int)ahora.DayOfWeek;

            // Buscar clases activas para hoy
            var relaciones = await _db.GrupoRelacion
            .Where(r => r.IdDia == diaSemana)
            .ToListAsync();

            // Obtener horarios relacionados
            var horariosIds = relaciones.Select(r => r.IdHorario).Distinct().ToList();
            var horarios = await _db.Horarios
                .Where(h => horariosIds.Contains(h.Id))
                .ToListAsync();


            if (esMaestro)
            {
                var activa = relaciones.FirstOrDefault(r =>
                {
                    var horario = horarios.FirstOrDefault(h => h.Id == r.IdHorario);
                    return horario != null &&
                           r.IdUsuario == usuario.uId &&
                           ahora.TimeOfDay >= TimeSpan.Parse(horario.HoraInicio) &&
                           ahora.TimeOfDay <= TimeSpan.Parse(horario.HoraFin);
                });


                if (activa == null)
                    return response.GetError("No tiene clase asignada en este horario");

                var asistenciasHoy = await _db.Asistencia
                    .Where(a => a.IdUsuario == usuario.uId &&
                                a.IdGrupo == activa.Id &&
                                a.Fecha.Date == ahora.Date)
                    .OrderBy(a => a.HoraRegistro)
                    .ToListAsync();

                var entradas = asistenciasHoy.Count(a => a.Tipo == "Entrada");
                var ActRetardos = asistenciasHoy.Count(a => a.Tipo == "ActRetardos");
                var cierres = asistenciasHoy.Count(a => a.Tipo == "Finalizado");

                string tipo = (entradas==0)?"Entrada" : (entradas==1 && ActRetardos==0 ? "ActRetardos" : ((entradas == cierres) ? "Entrada" : "Finalizado"));

                _db.Asistencia.Add(new Asistencia
                {
                    Id = Guid.NewGuid(),
                    IdUsuario = usuario.uId,
                    IdGrupo = activa.Id,
                    Fecha = ahora.Date,
                    Tipo = tipo,
                    HoraRegistro = ahora
                });

                await _db.SaveChangesAsync();

                return response.GetSuccess(new
                {
                    NombreCompleto = $"{usuario.sNombres} {usuario.sApellidos}",
                    isOpen = esMaestro,
                    TipoAsistencia = tipo
                });
            }
            else
            {
                if (usuario.uIdGrupo == null)
                    return response.GetError("Alumno sin grupo asignado");

                var relacionGrupo = relaciones.FirstOrDefault(r =>
                {
                    var horario = horarios.FirstOrDefault(h => h.Id == r.IdHorario);
                    return r.IdGrupo == usuario.uIdGrupo &&
                           horario != null &&
                           ahora.TimeOfDay >= TimeSpan.Parse(horario.HoraInicio) &&
                           ahora.TimeOfDay <= TimeSpan.Parse(horario.HoraFin);
                });


                if (relacionGrupo == null)
                    return response.GetError("No hay clase activa en este horario para tu grupo");

                var pasesMaestro = await _db.Asistencia
                    .Where(a => a.IdGrupo == relacionGrupo.Id &&
                                a.Fecha.Date == ahora.Date &&
                                a.IdUsuario == relacionGrupo.IdUsuario)
                    .OrderBy(a => a.HoraRegistro)
                    .ToListAsync();

                var aperturas = pasesMaestro.Count(a => a.Tipo == "Entrada");
                var actRetardos = pasesMaestro.Count(a => a.Tipo == "ActRetardos");
                var cierres = pasesMaestro.Count(a => a.Tipo == "Finalizado");

                if (aperturas == 0)
                    return response.GetError("Clase no iniciada por el maestro");

                if (aperturas <= cierres)
                    return response.GetError("Clase finalizada. Espera a que el maestro reabra la clase.");

                var yaPaso = await _db.Asistencia.AnyAsync(a =>
                    a.IdUsuario == usuario.uId &&
                    a.Fecha.Date == ahora.Date && a.IdGrupo == relacionGrupo.Id
                );

                if (yaPaso)
                    return response.GetError("Ya registraste tu asistencia hoy");

                string tipoAlumno = actRetardos > 0 ? "Retardo" : "Asistencia";

                _db.Asistencia.Add(new Asistencia
                {
                    Id = Guid.NewGuid(),
                    IdUsuario = usuario.uId,
                    IdGrupo = relacionGrupo.Id,
                    Fecha = ahora.Date,
                    Tipo = tipoAlumno,
                    HoraRegistro = ahora
                });

                await _db.SaveChangesAsync();

                return response.GetSuccess(new
                {
                    NombreCompleto = $"{usuario.sNombres} {usuario.sApellidos}",
                    isOpen = true,
                    TipoAsistencia = tipoAlumno
                });
            }
        }

        [HttpGet("asistencias")]
        public async Task<Response<dynamic>> GetAsistencias(Guid idGrupo, Guid idMaestro, DateTime? fecha = null)
        {
            var response = new Response<dynamic>();
            try
            {
                var dia = (fecha ?? DateTime.Now).Date;
                var diaSemana = (int)dia.DayOfWeek;

                // Obtener grupo
                var grupo = await _db.Grupo.FirstOrDefaultAsync(g => g.Id == idGrupo);
                if (grupo == null)
                    return response.GetError("Grupo no encontrado");

                // Verificar si el maestro tiene clase con ese grupo en ese día
                var relacion = await _db.GrupoRelacion.OrderByDescending(x=>x.IdHorario)
                    .FirstOrDefaultAsync(r => r.IdGrupo == idGrupo && r.IdUsuario == idMaestro && r.IdDia == diaSemana);

                if (relacion == null)
                    return response.GetError("El maestro no tiene clase asignada ese día con ese grupo");

                // Obtener maestro
                var maestro = await _db.Usuarios.FirstOrDefaultAsync(u => u.uId == idMaestro);
                if (maestro == null)
                    return response.GetError("Maestro no encontrado");

                // Obtener materia (servicio) asignada al maestro
                var servicioAsignado = await _db.ServiciosUsuario
                    .FirstOrDefaultAsync(c => c.IdUsuario == idMaestro && c.Asignado);

                var materia = await _db.Servicios.FirstOrDefaultAsync(s => s.Id == servicioAsignado.IdServicio);

                // Asistencia del maestro
                var asistenciaMaestro = await _db.Asistencia
                    .Where(a => a.IdGrupo == relacion.Id && a.IdUsuario == idMaestro && a.Fecha == dia)
                    .OrderBy(a => a.HoraRegistro)
                    .ToListAsync();

                // Obtener alumnos del grupo
                var alumnos = await _db.Usuarios
                    .Where(u => u.uIdGrupo == idGrupo)
                    .ToListAsync();

                // Asistencias de alumnos
                var asistenciasAlumnos = await _db.Asistencia
                    .Where(a => a.IdGrupo == relacion.Id && a.Fecha == dia && a.IdUsuario != idMaestro)
                    .ToListAsync();

                var listaAlumnos = alumnos.Select(a =>
                {
                    var asistencia = asistenciasAlumnos.FirstOrDefault(x => x.IdUsuario == a.uId);
                    return new
                    {
                        Nombre = $"{a.sNombres} {a.sApellidos}",
                        TipoAsistencia = asistencia?.Tipo ?? "Sin registro",
                        Hora = asistencia?.HoraRegistro.ToString("HH:mm:ss") ?? ""
                    };
                });

                response.Result = new
                {
                    Fecha = dia.ToString("yyyy-MM-dd"),
                    Grupo = grupo.Nombre,
                    Materia = materia?.Nombre ?? "Sin materia asignada",
                    Maestro = new
                    {
                        Nombre = $"{maestro.sNombres} {maestro.sApellidos}",
                        TipoAsistencia = asistenciaMaestro.LastOrDefault()?.Tipo ?? "Sin registro",
                        Hora = asistenciaMaestro.LastOrDefault()?.HoraRegistro.ToString("HH:mm:ss") ?? ""
                    },
                    Alumnos = listaAlumnos
                };

                response.Message = "Consulta exitosa";
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Error al consultar asistencias: {ex.Message}";
            }

            return response;
        }

        [HttpPut]
        public async Task<Response<Tarjeta>> Update([FromBody] Tarjeta entity)
        {
            Response<Tarjeta> response = new Response<Tarjeta>();
            var i = await _db.Tarjeta.Where(x => x.Id == entity.Id).AsNoTracking().FirstOrDefaultAsync();
            if (i != null)
            {
                var ant = await _db.Tarjeta.Where(x => x.Id != entity.Id && x.IdUsuario==entity.IdUsuario).AsNoTracking().FirstOrDefaultAsync();
                if(ant != null)
                {
                    ant.IdUsuario = Guid.Empty;
                    ant.Estado = false;
                    _db.Update(ant);
                }
                i.IdUsuario = entity.IdUsuario;
                i.Estado = entity.Estado;
                _db.Update(i);
                if ((await _db.SaveChangesAsync()) > 0)
                {
                    response.SetSuccess(entity);
                }
                else
                {
                    response.SetError("Error al actualizar");
                }
            }
            else
            {
                response.SetError("No existe");
            }
            return response;
        }

        [HttpGet]
        public async Task<Response<List<Tarjeta>>> GetAll()
        {
            Response<List<Tarjeta>> response = new Response<List<Tarjeta>>();
            var i = await _db.Tarjeta.AsNoTracking().ToListAsync();
            if (i.Count > 0)
            {
                response.SetSuccess(i);
            }
            else
            {
                response.SetError("Sin datos");
            }
            return response;
        }

        [HttpGet("Disponibles/{id}")]
        public async Task<Response<List<Tarjeta>>> GetAllTrue(Guid id)
        {
            Response<List<Tarjeta>> response = new Response<List<Tarjeta>>();
            var i = await _db.Tarjeta.Where(x=>!x.Estado || x.IdUsuario == id).AsNoTracking().ToListAsync();
            if (i.Count > 0)
            {
                response.SetSuccess(i);
            }
            else
            {
                response.SetError("Sin datos");
            }
            return response;
        }
    }
}
