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
        [JsonPropertyName("IdPerfil")]
        public Guid? IdPerfil { get; set; }
        [JsonPropertyName("ClavePerfil")]
        public string ClavePerfil { get; set; }
        [JsonPropertyName("NombrePerfil")]
        public string NombrePerfil { get; set; }
        [JsonPropertyName("Eliminable")]
        public bool Eliminable { get; set; }
    }
}
