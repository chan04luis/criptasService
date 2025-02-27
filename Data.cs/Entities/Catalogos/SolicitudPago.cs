namespace Data.cs.Entities.Catalogos
{
    public class SolicitudPago
    {
        public Guid uId { get; set; }
        public bool? bEstatus { get; set; }
        public Guid uIdPago { get; set; }
        public string? sEvidencia { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
    }
}
