using Newtonsoft.Json;

public class EntVisitas
{
    [JsonProperty("Id")]
    public Guid uId { get; set; }

    [JsonProperty("NombreVisitante")]
    public string sNombreVisitante { get; set; }

    [JsonProperty("IdCriptas")]
    public Guid uIdCriptas { get; set; }

    [JsonProperty("FechaRegistro")]
    public DateTime dtFechaRegistro { get; set; }

    [JsonProperty("FechaActualizacion")]
    public DateTime? dtFechaActualizacion { get; set; }

    [JsonProperty("Estatus")]
    public bool bEstatus { get; set; }
}