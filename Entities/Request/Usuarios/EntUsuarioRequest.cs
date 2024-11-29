using Newtonsoft.Json;

namespace Entities.JsonRequest.Usuarios
{
    public class EntUsuarioRequest
    {
        [JsonProperty("Nombres")]
        public string sNombres { get; set; }

        [JsonProperty("Apellidos")]
        public string sApellidos { get; set; }

        [JsonProperty("Correo")]
        public string sCorreo { get; set; }

        [JsonProperty("Contra")]
        public string sContra { get; set; }

        [JsonProperty("Telefono")]
        public string sTelefono { get; set; }

        [JsonProperty("Activo")]
        public bool bActivo { get; set; }
    }
}
