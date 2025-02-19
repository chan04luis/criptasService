
namespace Data.cs.Entities.Catalogos
{
    public class Servicios
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string? Descripcion { get; set; }

        public bool Estatus { get; set; } = true;
        public bool Eliminado { get; set; } = false;
        public Guid? IdIglesia { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public string? Img { get; set; }
    }
}
