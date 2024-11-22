namespace Data.cs.Entities
{
    public class Iglesias
    {
        public Guid uId { get; set; }
        public string sNombre { get; set; }
        public string sDireccion { get; set; }
        public string sCiudad { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public bool? bEstatus { get; set; }
        public bool bEliminado { get; set; }
        public DateTime dtFechaEliminado { get; set; }
        public List<Zonas> listZonas { get; set; }
    }
}
