using Newtonsoft.Json;

namespace Entities.Request.Usuarios
{
    public class EntUsuarioUpdateRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Nombres")]
        public string sNombres { get; set; }

        [JsonProperty("Apellidos")]
        public string sApellidos { get; set; }

        [JsonProperty("Telefono")]
        public string sTelefono { get; set; }
    }
}
