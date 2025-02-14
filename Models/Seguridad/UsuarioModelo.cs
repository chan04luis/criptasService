using Newtonsoft.Json;
using System;

namespace Models.Seguridad
{
    public class UsuarioModelo
    {
        [JsonProperty("IdUsuario")]
        public Guid? uIdUsuario { get; set; }
        [JsonProperty("IdPerfil")]
        public Guid? uIdPerfil { get; set; }
        [JsonProperty("Correo")]
        public string sCorreo { get; set; }
        [JsonProperty("Password")]
        public string? sPassword { get; set; }
        [JsonProperty("Nombres")]
        public string? sNombres { get; set; }
        [JsonProperty("Apellidos")]
        public string? sApellidos { get; set; }
        [JsonProperty("Telefono")]
        public string? sTelefono { get; set; }
        [JsonProperty("Eliminable")]
        public bool? bEliminable { get; set; }
        [JsonProperty("FechaCreacion")]
        public DateTime? dtFechaCreacion { get; set; }
        [JsonProperty("Activo")]
        public bool? bActivo { get; set; }
    }
}
