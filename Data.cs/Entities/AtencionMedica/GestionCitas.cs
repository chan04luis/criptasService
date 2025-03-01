namespace Data.cs.Entities.AtencionMedica
{
    public class Citas
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public Guid IdSucursal { get; set; }
        public Guid IdServicio { get; set; }
        public Guid? IdDoctor { get; set; } // Opcional
        public DateTime FechaCita { get; set; }
        public DateTime? FechaLlegada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string Estado { get; set; } // pendiente, en proceso, finalizada, cancelada, no presentado
        public int Turno { get; set; }
        public bool RegistradoEnPiso { get; set; } = false;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }
}
