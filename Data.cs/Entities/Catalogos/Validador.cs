namespace Data.cs.Entities.Catalogos
{
    public class Validador
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; } = null!;

        public bool Estado { get; set; }

        public DateTime? UltimaConexion { get; set; }

        public bool Eliminado { get; set; } = false;

        public Guid? IdAula { get; set; }
    }
}
