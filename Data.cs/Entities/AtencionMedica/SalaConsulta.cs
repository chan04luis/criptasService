namespace Data.cs.Entities.AtencionMedica
{
    public class SalaConsulta
    {
        public Guid Id { get; set; }
        public Guid IdSucursal { get; set; }
        public Guid IdDoctor { get; set; }
        public DateTime FechaEntrada { get; set; } = DateTime.Now;
        public DateTime? FechaSalida { get; set; }
        public bool Disponible => FechaSalida == null;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
