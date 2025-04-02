using Data.cs;
using Data.cs.Entities.Catalogos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace Api.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public GrupoController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<Response<dynamic>> GetAll()
        {
            Response<dynamic> response = new Response<dynamic>();
            try
            {
                var grupos = await _db.Grupo.AsNoTracking().ToListAsync();

                response.Result = grupos;
                response.Message = "Consulta exitosa";
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Error al obtener los horarios: {ex.Message}";
            }
            return response;
        }

        [HttpGet("dias")]
        public async Task<Response<dynamic>> GetAllDias()
        {
            Response<dynamic> response = new Response<dynamic>();
            try
            {
                var grupos = await _db.Dias.AsNoTracking().ToListAsync();

                response.Result = grupos;
                response.Message = "Consulta exitosa";
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Error al obtener los horarios: {ex.Message}";
            }
            return response;
        }

        [HttpGet("aulas")]
        public async Task<Response<dynamic>> GetAllAulas()
        {
            Response<dynamic> response = new Response<dynamic>();
            try
            {
                var grupos = await _db.Sucursal.Where(x=>!x.bEliminado).AsNoTracking().ToListAsync();

                response.Result = grupos;
                response.Message = "Consulta exitosa";
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Error al obtener los horarios: {ex.Message}";
            }
            return response;
        }

        [HttpGet("horarios")]
        public async Task<Response<dynamic>> GetAllHours()
        {
            Response<dynamic> response = new Response<dynamic>();
            try
            {
                var grupos = await _db.Horarios.AsNoTracking().ToListAsync();

                response.Result = grupos;
                response.Message = "Consulta exitosa";
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Error al obtener los horarios: {ex.Message}";
            }
            return response;
        }

        [HttpGet("maestros")]
        public async Task<Response<dynamic>> GetMaestros()
        {
            Response<dynamic> response = new Response<dynamic>();
            try
            {
                var grupos = await _db.Usuarios.Where(x=>x.uIdPerfil==new Guid("f579984f-8dd5-490f-bb7a-e99c994449c1")).AsNoTracking().ToListAsync();

                response.Result = grupos;
                response.Message = "Consulta exitosa";
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Error al obtener los horarios: {ex.Message}";
            }
            return response;
        }

        [HttpGet("relaciones/{id}")]
        public async Task<Response<dynamic>> GetGrupoRelaciones(Guid id)
        {
            Response<dynamic> response = new Response<dynamic>();
            try
            {
                var relaciones = await (
                    from gr in _db.GrupoRelacion

                    join d in _db.Dias on gr.IdDia equals d.Id
                    join a in _db.Sucursal on gr.IdAula equals a.uId
                    join h in _db.Horarios on gr.IdHorario equals h.Id
                    join g in _db.Grupo on gr.IdGrupo equals g.Id
                    join u in _db.Usuarios on gr.IdUsuario equals u.uId
                    where gr.IdGrupo == id
                    select new
                    {
                        IdRelacion = gr.Id,
                        IdDia = d.Id,
                        Dia = d.Nombre,

                        IdAula = a.uId,
                        Aula = a.sNombre,

                        IdHorario = h.Id,
                        Horario = h.Nombre,
                        HoraInicio = h.HoraInicio,
                        HoraFin = h.HoraFin,

                        IdGrupo = g.Id,
                        Grupo = g.Nombre,

                        IdMaestro = u.uId,
                        Maestro = u.sNombres + " " + u.sApellidos
                    }
                ).ToListAsync();


                response.Result = relaciones;
                response.Message = "Consulta exitosa";
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Error al obtener las relaciones de grupo: {ex.Message}";
            }

            return response;
        }


        [HttpGet("matriz/{idGrupo}")]
        public async Task<Response<dynamic>> GetHorarioMatriz(Guid idGrupo)
        {
            Response<dynamic> response = new Response<dynamic>();

            try
            {
                // Traer los días y bloques horarios
                var bloques = await (
                    from d in _db.Dias
                    from h in _db.Horarios
                    orderby h.HoraInicio
                    select new
                    {
                        IdDia = d.Id,
                        Dia = d.Nombre,
                        IdHorario = h.Id,
                        HoraInicio = h.HoraInicio,
                        HoraFin = h.HoraFin
                    }).ToListAsync();

                // Traer las relaciones del grupo
                var relaciones = await (
                    from gr in _db.GrupoRelacion
                    join d in _db.Dias on gr.IdDia equals d.Id
                    join a in _db.Sucursal on gr.IdAula equals a.uId
                    join h in _db.Horarios on gr.IdHorario equals h.Id
                    join g in _db.Grupo on gr.IdGrupo equals g.Id
                    join u in _db.Usuarios on gr.IdUsuario equals u.uId
                    where gr.IdGrupo == idGrupo
                    select new
                    {
                        IdDia = d.Id,
                        IdHorario = h.Id,
                        Detalle = new
                        {
                            id = gr.Id,
                            IdDia = d.Id,
                            Materia = _db.Servicios.FirstOrDefault(x=>x.Id==_db.ServiciosUsuario.FirstOrDefault(c=>c.IdUsuario==u.uId && c.Asignado).IdServicio).Nombre,
                            Aula = a.sNombre,
                            Maestro = u.sNombres + " " + u.sApellidos,
                            HoraInicio = h.HoraInicio,
                            HoraFin = h.HoraFin
                        }
                    }).ToListAsync();

                // Construir la matriz
                var matriz = bloques
                .GroupBy(b => new { b.HoraInicio, b.HoraFin, b.IdHorario })
                .Select(grupoBloque => new
                {
                    IdHora = grupoBloque.Key.IdHorario,
                    HoraInicio = grupoBloque.Key.HoraInicio,
                    HoraFin = grupoBloque.Key.HoraFin,
                    Dias = grupoBloque.Select(dia =>
                    {
                        var match = relaciones.FirstOrDefault(r =>
                            r.IdDia == dia.IdDia && r.IdHorario == dia.IdHorario);

                        return new
                        {
                            Dia = dia.Dia,
                            Contenido = match != null
                            ? match.Detalle
                            : new
                            {
                                id = Guid.Empty,
                                IdDia = dia.IdDia,
                                Materia = "",
                                Aula = "",
                                Maestro = "",
                                HoraInicio = "",
                                HoraFin = ""
                            }
                        };
                    }).ToDictionary(x => x.Dia, x => x.Contenido)
                }).ToList();


                response.Result = matriz;
                response.Message = "Matriz generada correctamente";
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Error al generar la matriz: {ex.Message}";
            }

            return response;
        }

        [HttpPost("GrupoRelacion")]
        public async Task<Response<bool>> SaveGrupoRelacion([FromBody] GrupoRelacion model)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                if (model.Id == Guid.Empty)
                {
                    model.Id = Guid.NewGuid(); // Nuevo registro
                    await _db.GrupoRelacion.AddAsync(model);
                }
                else
                {
                    var existente = await _db.GrupoRelacion.FindAsync(model.Id);
                    if (existente == null)
                    {
                        response.HasError = true;
                        response.Message = "No se encontró el registro para actualizar.";
                        return response;
                    }

                    // Actualiza los campos
                    existente.IdGrupo = model.IdGrupo;
                    existente.IdDia = model.IdDia;
                    existente.IdAula = model.IdAula;
                    existente.IdHorario = model.IdHorario;
                    existente.IdUsuario = model.IdUsuario;

                    _db.GrupoRelacion.Update(existente);
                }

                await _db.SaveChangesAsync();
                response.Result = true;
                response.Message = "Relación guardada exitosamente.";
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Error al guardar la relación: {ex.Message}";
            }

            return response;
        }

        [HttpDelete("GrupoRelacion/{id}")]
        public async Task<Response<dynamic>> detele(Guid id)
        {
            Response<dynamic> response = new Response<dynamic>();
            try
            {
                var existente = await _db.GrupoRelacion.Where(x => x.Id == id).ExecuteDeleteAsync();
                if (existente == 0)
                {
                    response.HasError = true;
                    response.SetError("No se encontró el registro para eliminar.");
                    return response;
                }
                ;
                response.SetSuccess(true);
                response.Message = "Eliminado";
            }
            catch (Exception ex)
            {
                response.SetError($"Error al obtener los horarios: {ex.Message}");
            }
            return response;
        }
    }
}
