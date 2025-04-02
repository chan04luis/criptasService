namespace Data.cs.Entities.Catalogos
{
    public class Tarjeta
    {
        public string Id { get; set; } = null!;

        public Guid IdUsuario { get; set; }

        public bool Estado { get; set; }
    }
}
