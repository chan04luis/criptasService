using Data.cs.Entities.Seguridad;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Interfaces.Seguridad
{
    public interface IBotonesRepositorio
    {
        Task<Response<Boton>> DSave(Boton newItem);
        Task<Response<bool>> DDelete(Guid iKey);
        Task<Response<List<Boton>>> DGet();
        Task<Response<Boton>> DGet(Guid iKey);
        Task<Response<bool>> DUpdate(Boton entity);
    }
}
