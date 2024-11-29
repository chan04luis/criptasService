
using Newtonsoft.Json;

namespace Entities.Responses.Usuarios
{
    public class AuthLogin
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Nombres")]
        public string sNombres { get; set; }

        [JsonProperty("Apellidos")]
        public string sApellidos { get; set; }

        [JsonProperty("Correo")]
        public string sCorreo { get; set; }

        [JsonProperty("Token")]
        public string sToken { get; set; }
    }
}
