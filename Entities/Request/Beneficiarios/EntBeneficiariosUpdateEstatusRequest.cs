using Newtonsoft.Json;

public class EntBeneficiariosUpdateEstatusRequest
{
    [JsonProperty("Id")]
    public Guid uId { get; set; }

    [JsonProperty("Estatus")]
    public bool bEstatus { get; set; }
}