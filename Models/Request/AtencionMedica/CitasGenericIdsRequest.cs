namespace Models.Request.AtencionMedica
{
    public class CitasGenericIdsRequest
    {
        public Guid? idSucursal { get; set; }
        public Guid? idCliente { get; set; }
        public Guid? idCita { get; set; }
        public Guid? idDoctor { get; set; }
    }
}
