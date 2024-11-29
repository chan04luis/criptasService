using Newtonsoft.Json;
public class EntUsuarios
{
    [JsonProperty("Id")]
    public Guid uId { get; set; }

    [JsonProperty("Nombres")]
    public string sNombres { get; set; }

    [JsonProperty("Apellidos")]
    public string sApellidos { get; set; }

    [JsonProperty("Correo")]
    public string sCorreo { get; set; }

    [JsonProperty("Contra")]
    public string sContra { get; set; }

    [JsonProperty("Telefono")]
    public string sTelefono { get; set; }

    [JsonProperty("Activo")]
    public bool bActivo { get; set; }

    [JsonProperty("FechaRegistro")]
    public DateTime dtFechaRegistro { get; set; }

    [JsonProperty("FechaActualizacion")]
    public DateTime dtFechaActualizacion { get; set; }
}
