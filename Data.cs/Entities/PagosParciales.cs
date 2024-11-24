
namespace Data.cs.Entities
{
    public class PagosParciales
    {
        public Guid uId { get; set; }
        public Guid uIdPago { get; set; }
        public Decimal dMonto { get; set; }  
        public DateTime dtFechaPago { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public DateTime dtFechaEliminado { get; set; }
        public bool bEstatus { get; set; }
        public bool bEliminado { get; set; }
    }
}
