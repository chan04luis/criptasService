using Newtonsoft.Json;

namespace Entities.Request.TipoPagos
{
    public class EntTiposPagoSearchRequest
    {
        [JsonProperty("Id")]
        public Guid? Id { get; set; }

        [JsonProperty("Nombre")]
        public string? sNombre { get; set; }

        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
