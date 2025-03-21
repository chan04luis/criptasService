namespace Models.Models.AtencionMedica
{
    public class EntCitas
    {
        public Guid Id { get; set; }
        public string Cliente { get; set; }
        public string Sucursal { get; set; }
        public string Servicio { get; set; }
        public string Doctor { get; set; }
        public DateTime FechaCita { get; set; }
        public string Estado { get; set; }
        public bool? RegistradoEnPiso { get; set; }

        public Guid? IdCliente { get; set; }
        public Guid? IdSucursal { get; set; }
        public Guid? IdServicio { get; set; }
        public Guid? IdDoctor { get; set; }
    }
}
