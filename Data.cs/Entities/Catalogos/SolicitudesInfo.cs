
namespace Data.cs.Entities.Catalogos
{
    public class SolicitudesInfo
    {
        public Guid Id { get; set; }

        public Guid IdCliente { get; set; }

        public Guid IdServicio { get; set; }

        public string? Mensaje { get; set; }

        public bool Visto { get; set; } = false;

        public bool Atendido { get; set; } = false;

        public bool Eliminado { get; set; } = false;

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}
