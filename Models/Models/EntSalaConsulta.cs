
namespace Models.Models
{
    public class EntSalaConsulta
    {
        public Guid Id { get; set; }
        public Guid IdSucursal { get; set; }
        public Guid IdDoctor { get; set; }
        public DateTime FechaEntrada { get; set; }
        public bool Disponible { get; set; }
    }

}
