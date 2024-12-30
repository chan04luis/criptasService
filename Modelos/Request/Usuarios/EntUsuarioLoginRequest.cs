using Newtonsoft.Json;

namespace Modelos.Request.Usuarios
{
    public class EntUsuarioLoginRequest
    {
        [JsonProperty("Correo")]
        public string? sCorreo { get; set; }

        [JsonProperty("Contra")]
        public string? sContra { get; set; }
    }
}
