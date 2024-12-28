using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Seguridad
{
    public class LoginModelo
    {
        public required string sUserName { get; set; }
        public required string sPassword { get; set; }
    }
}
