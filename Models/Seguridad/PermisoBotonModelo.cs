using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class PermisoBotonModelo
    {
        [JsonProperty("IdPermisoBoton")]
        public Guid IdPermisoBoton { get; set; }
        [JsonProperty("IdBoton")]
        public Guid IdBoton { get; set; }
        [JsonProperty("ClaveBoton")]
        public string? ClaveBoton { get; set; }
        [JsonProperty("NombreBoton")]
        public string? NombreBoton { get; set; }
        [JsonProperty("TienePermiso")]
        public bool? TienePermiso { get; set; }
    }
}
