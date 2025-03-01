namespace Data.cs.Entities.Catalogos
{
    public class SucursalesUsuario
    {
        public Guid Id { get; set; }
        public Guid IdSucursal { get; set; }
        public Guid IdUsuario { get; set; }
        public bool Asignado { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }
}
