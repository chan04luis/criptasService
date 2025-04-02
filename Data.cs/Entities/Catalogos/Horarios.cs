namespace Data.cs.Entities.Catalogos
{
    public class Horarios
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string HoraInicio { get; set; } = null!;

        public string HoraFin { get; set; } = null!;
    }
}
