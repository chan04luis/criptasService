namespace Models.Request.AtencionMedica
{
    public class CitaRequest
    {
        public Guid IdCliente { get; set; }
        public Guid IdSucursal { get; set; }
        public Guid IdServicio { get; set; }
        public Guid? IdDoctor { get; set; }
        public DateTime FechaCita { get; set; }
    }
}
