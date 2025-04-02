using Data.cs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace Api.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public HorariosController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<Response<dynamic>> GetAll()
        {
            Response<dynamic> response = new Response<dynamic>();
            try
            {
                var horarios = await (from d in _db.Dias
                                      from h in _db.Horarios.DefaultIfEmpty()
                                      orderby d.Id, h.HoraInicio
                                      select new
                                      {
                                          Dia = d.Nombre,
                                          Modulo = h != null ? h.Nombre : "Sin módulo",
                                          HoraInicio = h != null ? h.HoraInicio : null,
                                          HoraFin = h != null ? h.HoraFin : null
                                      }).ToListAsync();

                response.Result = horarios;
                response.Message = "Consulta exitosa";
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Error al obtener los horarios: {ex.Message}";
            }
            return response;
        }
    }
}
