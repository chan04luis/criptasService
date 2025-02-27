using Newtonsoft.Json;

namespace Models.Responses.Criptas
{
    public class MisCriptas
    {
        [JsonProperty("Id")]
        public string uId {  get; set; }
        [JsonProperty("Cripta")]
        public string sNombre { get; set; }
        [JsonProperty("Seccion")]
        public string sNombreSeccion { get; set; }
        [JsonProperty("Zona")]
        public string sNombreZona { get; set; }
        [JsonProperty("Pagado")]
        public bool bPagado { get; set; }
        [JsonProperty("Precio")]
        public decimal dPrecio { get; set; }
        [JsonProperty("Iglesia")]
        public string sIglesia { get; set; }
        [JsonProperty("Lat")]
        public string sLatitud { get; set; }
        [JsonProperty("Long")]
        public string sLongitud { get; set; }
        [JsonProperty("Fallecidos")]
        public int iFallecidos { get; set; }
        [JsonProperty("Beneficiarios")]
        public int iBeneficiarios { get; set; }
        [JsonProperty("Visitas")]
        public int iVisitas { get; set; }
        [JsonProperty("FechaCompra")]
        public DateTime dtFechaCompra {  get; set; }
    }
}
