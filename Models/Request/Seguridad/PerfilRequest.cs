using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Request.Seguridad
{
    public class PerfilRequest
    {
        [JsonProperty("idPerfil")]
        public Guid? IdPerfil { get; set; }

        [JsonPropertyName("clavePerfil")]
        public string ClavePerfil { get; set; }

        [JsonPropertyName("nombrePerfil")]
        public string NombrePerfil { get; set; }

        [JsonPropertyName("eliminable")]
        public bool Eliminable { get; set; }
    }
}
