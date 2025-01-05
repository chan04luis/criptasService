using Models.Request.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class PerfilModelo:PerfilRequest
    {

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("usuarioCreacion")]
        public string? IdUsuarioCreacion { get; set; }

        [JsonPropertyName("fechaModificacio")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("usuarioModificacion")]
        public string? IdUsuarioModificacion { get; set; }

        [JsonPropertyName("activo")]
        public bool? Activo { get; set; }
    }
}
