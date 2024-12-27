using Data.cs.Entities.Seguridad;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Interfaces.Seguridad
{
    public interface IPaginasRespositorio
    {
        Task<Response<Pagina>> DSave(Pagina newItem);
        Task<Response<bool>> DDelete(Guid iKey);
        Task<Response<List<Pagina>>> DGet();
        Task<Response<Pagina>> DGet(Guid iKey);
        Task<Response<bool>> DUpdate(Pagina entity);
    }
}
