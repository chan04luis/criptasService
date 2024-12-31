using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class LoginModelo
    {
        public required string sCorreo { get; set; }
        public required string sPassword { get; set; }
    }
}
