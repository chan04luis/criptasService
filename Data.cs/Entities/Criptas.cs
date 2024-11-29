
namespace Data.cs.Entities
{
    public class Criptas
    {
        public Guid uId { get; set; }
        public Guid uIdSeccion { get; set; }
        public Guid uIdCliente { get; set; }
        public string sNumero { get; set; }
        public string sUbicacionEspecifica { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public DateTime dtFechaEliminado { get; set; }
        public bool bEstatus { get; set; }
        public bool bEliminado { get; set; }
        public Secciones? Seccion { get; set; }
        public Clientes? Cliente { get; set; }
        public List<Pagos>? listPagos { get; set; }
        public List<Fallecidos>? listFallecidos { get; set; }
        public List<Visitas>? listVisitas { get; set; }
    }
}
