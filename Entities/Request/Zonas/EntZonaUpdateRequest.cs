﻿using Newtonsoft.Json;
namespace Entities.JsonRequest.Zonas
{
    public class EntZonaUpdateRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("IdIglesia")]
        public Guid uIdIglesia { get; set; }

        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
