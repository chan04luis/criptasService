using Newtonsoft.Json;

namespace Entities.Models
{
    public class EntIglesias
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
    }
}
