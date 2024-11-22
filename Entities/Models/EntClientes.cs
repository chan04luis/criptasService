using Newtonsoft.Json;

namespace Entities.Models
{
    public class EntClientes
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("Nombres")]
        public string sNombre { get; set; }
        [JsonProperty("Apellidos")]
        public string sApellidos { get; set; }
        [JsonProperty("Direccion")]
        public string sDireccion { get; set; }
        [JsonProperty("FechaNac")]
        public string sFechaNacimiento { get; set; }
        [JsonProperty("Sexo")]
        public string sSexo { get; set; }
        [JsonProperty("Edad")]
        public int? iEdad { get; set; }
        [JsonProperty("Telefono")]
        public string sTelefono { get; set; }
        [JsonProperty("Email")]
        public string sEmail { get; set; }
        [JsonProperty("FechaRegistro")]
        public DateTime? dtFechaRegistro { get; set; }
        [JsonProperty("FechaActualizacion")]
        public DateTime? dtFechaActualizacion { get; set; }
        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
