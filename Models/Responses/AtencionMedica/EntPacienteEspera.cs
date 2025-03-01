namespace Models.Responses.AtencionMedica
{
    public class EntPacienteEspera
    {
        public Guid IdSalaEspera { get; set; }
        public Guid? IdCita { get; set; }
        public string? Cliente { get; set; }
        public string? Servicio { get; set; }
        public int Turno { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
