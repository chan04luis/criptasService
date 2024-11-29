namespace Data.cs.Entities
{
    public class Visitas
    {
        public Guid uId { get; set; }
        public string sNombreVisitante { get; set; }
        public Guid uIdCriptas { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public DateTime dtFechaEliminado { get; set; }
        public bool bEstatus {  get; set; }
        public bool bEliminado { get; set; }
        public Criptas? cripta { get; set; }
    }
}
