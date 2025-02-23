namespace Data.cs.Entities.Catalogos
{
    public class Sucursales
    {
        public Guid uId { get; set; }
        public string sNombre { get; set; }
        public string? sTelefono { get; set; }
        public string sDireccion { get; set; }
        public string sCiudad { get; set; }
        public string? sLatitud { get; set; }
        public string? sLongitud { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public bool? bEstatus { get; set; }
        public bool bEliminado { get; set; }
        public DateTime dtFechaEliminado { get; set; }
    }
}
