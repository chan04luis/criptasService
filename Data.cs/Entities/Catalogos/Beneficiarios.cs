namespace Data.cs.Entities.Catalogos
{
    public class Beneficiarios
    {
        public Guid uId { get; set; }
        public Guid uIdCripta { get; set; }
        public string sNombre { get; set; }
        public string? sIneFrente { get; set; }
        public string? sIneReverso { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public DateTime dtFechaEliminado { get; set; }
        public bool bEstatus { get; set; }
        public bool bEliminado { get; set; }
    }
}
