using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class ConfiguracionModelo
    {
        [JsonProperty("IdConfiguracion")]
        public Guid uIdConfiguracion { get; set; }
        [JsonProperty("TituloNavegador")]
        public string? sTituloNavegador { get; set; }
        [JsonProperty("Titulo")]
        public string? sTitulo { get; set; }
        [JsonProperty("MetaDescripcion")]
        public string? sMetaDescripcion { get; set; }
        [JsonProperty("ColorPrimario")]
        public string? sColorPrimario { get; set; }
        [JsonProperty("ColorSecundario")]
        public string? sColorSecundario { get; set; }
        [JsonProperty("ContrastePrimario")]
        public string? sContrastePrimario { get; set; }
        [JsonProperty("ContrasteSecundario")]
        public string? sContrasteSecundario { get; set; }
        [JsonProperty("UrlFuente")]
        public string? sUrlFuente { get; set; }
        [JsonProperty("NombreFuente")]
        public string? sNombreFuente { get; set; }
        [JsonProperty("RutaImagenFondo")]
        public string? sRutaImagenFondo { get; set; }
        [JsonProperty("RutaImagenLogo")]
        public string? sRutaImagenLogo { get; set; }
        [JsonProperty("RutaImagenPortal")]
        public string? sRutaImagenPortal { get; set; }
    }
}
