
namespace Data.cs.Entities
{
    public class Pagos
    {
        public Guid uId { get; set; }
        public Guid uIdClientes { get; set; }
        public Guid uIdCripta {  get; set; }
        public Guid uIdTipoPago { get; set; }
        public Decimal montoTotal { get; set; }
        public DateTime dtFechaLimite { get; set; }
        public bool bPagado {  get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public bool? bEstatus { get; set; }

    }
}
