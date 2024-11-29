using Newtonsoft.Json;
namespace Entities.Request.Usuarios
{
    public class EntUsuarioSearchRequest
    {
        [JsonProperty("Id")]
        public Guid? uId { get; set; }

        [JsonProperty("Nombres")]
        public string? sNombres { get; set; }

        [JsonProperty("Apellidos")]
        public string? sApellidos { get; set; }

        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
