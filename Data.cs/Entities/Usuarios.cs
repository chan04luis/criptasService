
namespace Data.cs.Entities
{
    public class Usuarios
    {
        public Guid uId { get; set; }
        public string sNombres { get; set; }
        public string sApellidos { get; set; }
        public string sCorreo { get; set; }
        public string sContra { get; set; }
        public string sTelefono { get; set; }
        public bool bActivo { get; set; }
        public bool bEliminado { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public DateTime dtFechaEliminado { get; set; }
    }
}
