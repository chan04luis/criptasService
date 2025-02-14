using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Data.cs.Interfaces.Catalogos
{
    public interface ITipoDeMantenimientoRepositorio
    {
        Task<Response<bool>> AnyExistKey(Guid pKey);
        Task<Response<bool>> AnyExitMailAndKey(EntTipoDeMantenimiento pEntity);
        Task<Response<bool>> DAnyExistName(string nombre);
        Task<Response<EntTipoDeMantenimiento>> DSave(EntTipoDeMantenimiento entity);
        Task<Response<EntTipoDeMantenimiento>> DUpdate(EntTipoDeMantenimiento entity);
        Task<Response<EntTipoDeMantenimiento>> DUpdateEstatus(EntTipoDeMantenimiento entity);
        Task<Response<bool>> DDelete(Guid uId);
        Task<Response<EntTipoDeMantenimiento>> DGetById(Guid iKey);
        Task<Response<List<EntTipoDeMantenimiento>>> DGetList();
    }
}
