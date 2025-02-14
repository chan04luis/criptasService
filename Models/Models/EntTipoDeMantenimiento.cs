
namespace Models.Models
{
    public class EntTipoDeMantenimiento
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public bool Estatus { get; set; }
        public string? Img { get; set; }
    }
}
