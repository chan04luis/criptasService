using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request.TipoPagos
{
    public class EntTiposPago
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("Descripcion")]
        public string? sDescripcion { get; set; }

        [JsonProperty("FechaRegistro")]
        public DateTime? dtFechaRegistro { get; set; }

        [JsonProperty("FechaActualizacion")]
        public DateTime? dtFechaActualizacion { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
}
