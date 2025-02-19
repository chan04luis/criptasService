using Newtonsoft.Json;

namespace Models.Request.Fallecidos
{
    public class EntFallecidosSearchRequest
    {
        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("Apellidos")]
        public string sApellidos { get; set; }

        [JsonProperty("IdIglesia")]
        public Guid? uIdIglesia { get; set; }
        [JsonProperty("NumPag")]
        public int iNumPag { get; set; }
        [JsonProperty("NumReg")]
        public int iNumReg { get; set; }
    }
}
