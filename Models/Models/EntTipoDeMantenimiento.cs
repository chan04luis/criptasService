using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class EntTipoDeMantenimiento
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public byte Estatus { get; set; }
        public string Img { get; set; }
    }
}
