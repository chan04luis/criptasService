using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses.Criptas
{
    public class CriptasResumen
    {
        public int Total { get; set; }
        public int Disponibles { get; set; }
        public int Ocupadas { get; set; }
    }
}
