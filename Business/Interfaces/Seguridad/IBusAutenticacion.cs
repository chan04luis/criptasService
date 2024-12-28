using Entities;
using Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Seguridad
{
    public interface IBusAutenticacion
    {
        Task<Response<LoginResponseModelo>> Login(LoginModelo loginModel);

    }
}
