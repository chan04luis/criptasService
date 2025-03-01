
namespace Models.Request.AtencionMedica
{
    public class CitasFiltroRequest
    {
        public DateTime? Fecha { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Estado { get; set; }
    }
}
