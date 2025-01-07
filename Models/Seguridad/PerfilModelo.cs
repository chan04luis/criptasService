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

        [JsonPropertyName("FechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("UsuarioCreacion")]
        public string? IdUsuarioCreacion { get; set; }

        [JsonPropertyName("FechaModificacion")]
        public DateTime? FechaModificacion { get; set; }

        [JsonPropertyName("UsuarioModificacion")]
        public string? IdUsuarioModificacion { get; set; }

        [JsonPropertyName("Activo")]
        public bool? Activo { get; set; }
    }
}
