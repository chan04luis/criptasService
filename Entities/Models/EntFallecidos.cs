using Newtonsoft.Json;

public class EntFallecidos
{
    [JsonProperty("Id")]
    public Guid uId { get; set; }

    [JsonProperty("Nombre")]
    public string sNombre { get; set; }

    [JsonProperty("Estatus")]
    public bool bEstatus { get; set; }

    [JsonProperty("IdCripta")]
    public Guid uIdCripta { get; set; }

    [JsonProperty("FechaRegistro")]
    public DateTime dtFechaRegistro { get; set; }

    [JsonProperty("FechaActualizacion")]
    public DateTime dtFechaActualizacion { get; set; }
}