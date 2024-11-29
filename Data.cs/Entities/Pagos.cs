
namespace Data.cs.Entities
{
    public class Pagos
    {
        public Guid uId { get; set; }
        public Guid uIdClientes { get; set; }
        public Guid uIdCripta {  get; set; }
        public Guid uIdTipoPago { get; set; }
        public decimal dMontoTotal { get; set; }
        public decimal? dMontoPagado { get; set; }
        public DateTime dtFechaLimite { get; set; }
        public DateTime? dtFechaPago { get; set; }
        public bool bPagado {  get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public DateTime dtFechaEliminado { get; set; }
        public bool? bEstatus { get; set; }
        public bool bEliminado { get; set; }

        public Clientes? Cliente { get; set; }
        public Criptas? Cripta { get; set; }
        public TiposDePago? TipoPago { get; set; }
    }
}
