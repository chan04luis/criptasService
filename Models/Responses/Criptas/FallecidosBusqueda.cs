
using Newtonsoft.Json;

namespace Models.Responses.Criptas
{
    public class FallecidosBusqueda
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("IdIglesia")]
        public Guid uIdIglesia { get; set; }
        [JsonProperty("Nombres")]
        public string sNombres { get; set; }
        [JsonProperty("Apellidos")]
        public string sApellidos { get; set; }
        [JsonProperty("Edad")]
        public int iEdad { get; set; }
        [JsonProperty("Nacido")]
        public string sFechaNacido { get; set; }
        [JsonProperty("Fallecido")]
        public string sFechaFallecido { get; set; }
        [JsonProperty("Cripta")]
        public string sNombre { get; set; }
        [JsonProperty("Seccion")]
        public string sNombreSeccion { get; set; }
        [JsonProperty("Zona")]
        public string sNombreZona { get; set; }
        [JsonProperty("Iglesia")]
        public string sIglesia { get; set; }
    }
}
