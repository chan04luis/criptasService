using Newtonsoft.Json;

public class EntBeneficiariosUpdateRequest
{
    [JsonProperty("Id")]
    public Guid uId { get; set; }

    [JsonProperty("IdCripta")]
    public Guid uIdCripta { get; set; }

    [JsonProperty("Nombre")]
    public string sNombre { get; set; }
}
