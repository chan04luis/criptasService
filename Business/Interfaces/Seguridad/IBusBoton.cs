using Models.Request.Seguridad;
using Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Business.Interfaces.Seguridad
{
    public interface IBusBoton
    {
        Task<Response<BotonModelo>> BCreate(BotonRequest createModel);
        Task<Response<bool>> BDelete(Guid iKey);
        Task<Response<bool>> BUpdate(BotonRequest updateModel);
    }
}
