using Data.cs.Entities.Seguridad;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Interfaces.Seguridad
{
    public interface IPerfilesRepositorio
    {
        Task<Response<bool>> AnyExistKey(Guid pKey);
        Task<Response<bool>> AnyExitNameAndKey(Perfil pEntity);
        Task<Response<bool>> AnyExitName(string pName);
        Task<Response<bool>> Delete(Guid iKey);
        Task<Response<List<Perfil>>> GetAll();
        Task<Response<Perfil>> Get(Guid iKey);
        Task<Response<Perfil>> Save(Perfil newItem);
        Task<Response<bool>> Update(Perfil entity);
    }
}
