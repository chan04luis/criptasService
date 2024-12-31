using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses.Iglesia
{
    public class EntIglesiaResponse
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("Nombre")]
        public string sNombre { get; set; }
        [JsonProperty("Direccion")]
        public string sDireccion { get; set; }
        [JsonProperty("Ciudad")]
        public string sCiudad { get; set; }
        [JsonProperty("FechaRegistro")]
        public DateTime? dtFechaRegistro { get; set; }
        [JsonProperty("FechaActualizacion")]
        public DateTime? dtFechaActualizacion { get; set; }
        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
        [JsonProperty("Zonas")]
        public List<EntZonas>? Items { get; set; }
    }
}
