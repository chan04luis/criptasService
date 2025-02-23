using Newtonsoft.Json;

namespace Models.Request.Catalogo.Clientes
{
    public class EntClienteUpdateEstatusRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
    public class EntClienteDeleteRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
    }
    public class EntClienteSearchRequest
    {
        [JsonProperty("Id")]
        public Guid? uId { get; set; }
        [JsonProperty("Nombre")]
        public string? sNombre { get; set; }
        [JsonProperty("Apellido")]
        public string? sApellido { get; set; }
        [JsonProperty("Correo")]
        public string? sCorreo { get; set; }
        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
        [JsonProperty("NumPag")]
        public int iNumPag { get; set; }
        [JsonProperty("NumReg")]
        public int iNumReg { get; set; }
    }
}
