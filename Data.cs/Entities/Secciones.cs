namespace Data.cs.Entities
{
    public class Secciones
    {
        public Guid uId { get; set; }
        public Guid uIdZona { get; set; }
        public string sNombre { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public bool bEstatus { get; set; }
        public bool bEliminado { get; set; }
        public DateTime dtFechaEliminado { get; set; }
        public Zonas Zona { get; set; }
        public List<Criptas>? listCriptas { get; set; }
    }
}
