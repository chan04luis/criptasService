
namespace Data.cs.Entities
{
    public class Clientes
    {
        public Guid uId { get; set; }
        public string sNombre { get; set; }
        public string sApellidos { get; set; }
        public string sDireccion { get; set; }
        public string sTelefono { get; set; }
        public string sEmail { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public bool? bEstatus { get; set; }
    }
}
