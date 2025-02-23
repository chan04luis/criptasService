
namespace Models.Responses.Servicio
{
    public class EntServiceItem
    {
        public Guid? Id { get; set; }

        public string Nombre { get; set; }
        public string? ImgPreview { get; set; }
        public Guid? IdAsignado { get; set; }
        public bool Asignado { get; set; } = false;
    }
}
