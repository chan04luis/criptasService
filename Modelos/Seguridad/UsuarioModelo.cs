using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Seguridad
{
    public class UsuarioModelo
    {
        [JsonProperty("Id")]
        public Guid? uIdUsuario { get; set; }
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
