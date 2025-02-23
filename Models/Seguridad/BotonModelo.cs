using Newtonsoft.Json;
namespace Models.Seguridad
{
    public class BotonModelo
    {
        [JsonProperty("IdBoton")]
        public Guid? uIdBoton { get; set; }
        [JsonProperty("IdPagina")]
        public Guid? uIdPagina { get; set; }
        [JsonProperty("ClaveBoton")]
        public string sClaveBoton { get; set; }
        [JsonProperty("NombreBoton")]
        public string sNombreBoton { get; set; }
    }
}
