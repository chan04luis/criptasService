using Newtonsoft.Json;

namespace Models.Request.Usuarios
{
    public class EntUsuarioRequest
    {
        [JsonProperty("Id")]
        public string uId { get; set; }
        [JsonProperty("Nombres")]
        public string sNombres { get; set; }
        [JsonProperty("IdPerfil")]
        public Guid uIdPerfil { get; set; }
        [JsonProperty("Apellidos")]
        public string sApellidos { get; set; }

        [JsonProperty("Correo")]
        public string sCorreo { get; set; }

        [JsonProperty("Contra")]
        public string sContra { get; set; }

        [JsonProperty("Telefono")]
        public string sTelefono { get; set; }

    }
}
