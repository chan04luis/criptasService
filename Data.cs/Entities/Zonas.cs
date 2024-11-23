namespace Data.cs.Entities
{
    public class Zonas
    {
        public Guid uId { get; set; }
        public Guid uIdIglesia { get; set; }
        public string sNombre { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public bool? bEstatus { get; set; }
        public bool bEliminado { get; set; }
        public DateTime dtFechaEliminado { get; set; }
        public Iglesias Iglesia { get; set; }
        public List<Secciones> listSecciones { get; set; }
    }
}
