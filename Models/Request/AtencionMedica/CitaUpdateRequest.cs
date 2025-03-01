namespace Models.Request.AtencionMedica
{
    public class CitaUpdateRequest
    {
        public Guid IdSucursal { get; set; }
        public Guid IdServicio { get; set; }
        public Guid? IdDoctor { get; set; }
        public DateTime FechaCita { get; set; }
        public string Estado { get; set; }
    }
}
