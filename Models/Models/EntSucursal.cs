using Newtonsoft.Json;

namespace Models.Models
{
    public class EntSucursal
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("Nombre")]
        public string sNombre { get; set; }
        [JsonProperty("Telefono")]
        public string sTelefono { get; set; }
        [JsonProperty("Direccion")]
        public string sDireccion { get; set; }
        [JsonProperty("Latitud")]
        public string? sLatitud { get; set; }
        [JsonProperty("Longitud")]
        public string? sLongitud { get; set; }
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
