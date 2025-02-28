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
        public string Ingresos { get; set; }

        public int Clientes { get; set; }   

        public int Ventas { get; set; }

        public int VendidasMesPasado { get; set; }
        public int VendidasSemanaPasada { get; set; }   
    }
}
