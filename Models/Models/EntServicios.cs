
namespace Models.Models
{
    public class EntServicios
    {
        public Guid? Id { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public bool? Estatus { get; set; } = true;

        public string? Img { get; set; }
        public Guid? IdIglesia { get; set; }
        public DateTime FechaRegistro { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}
