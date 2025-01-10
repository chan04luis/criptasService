using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class PermisoBotonModelo
    {
        [JsonPropertyName("IdPermisoBoton")]
        public Guid IdPermisoBoton { get; set; }
        [JsonPropertyName("IdBoton")]
        public Guid IdBoton { get; set; }
        [JsonPropertyName("ClaveBoton")]
        public string? ClaveBoton { get; set; }
        [JsonPropertyName("NombreBoton")]
        public string? NombreBoton { get; set; }
        [JsonPropertyName("TienePermiso")]
        public bool? TienePermiso { get; set; }
    }
}
