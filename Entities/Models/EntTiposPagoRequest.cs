using Newtonsoft.Json;

namespace Entities.Models
{
    public class EntTiposPagoRequest
    {
        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("Descripcion")]
        public string? sDescripcion { get; set; }
    }
}
