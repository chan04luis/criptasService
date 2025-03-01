namespace Data.cs.Entities.AtencionMedica
{
    public class SalaEspera
    {
        public Guid Id { get; set; }
        public Guid IdSucursal { get; set; }
        public Guid? IdCita { get; set; } // Puede ser null si es paciente sin cita
        public Guid IdCliente { get; set; }
        public int Turno { get; set; }
        public DateTime FechaIngreso { get; set; } = DateTime.Now;
        public DateTime? FechaLlamado { get; set; }
        public bool Atendido { get; set; } = false;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
