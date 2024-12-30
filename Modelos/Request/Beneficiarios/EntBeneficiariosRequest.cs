using Newtonsoft.Json;

public class EntBeneficiariosRequest
{
    [JsonProperty("IdCripta")]
    public Guid uIdCripta { get; set; }

    [JsonProperty("Nombre")]
    public string sNombre { get; set; }
}