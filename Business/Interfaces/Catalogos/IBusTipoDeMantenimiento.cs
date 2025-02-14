using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Business.Interfaces.Catalogos
{
    public interface IBusTipoDeMantenimiento
    {
        Task<Response<EntTipoDeMantenimiento>> SaveTipoDeMantenimiento(EntTipoDeMantenimiento tipoDeMantenimiento);
        Task<Response<EntTipoDeMantenimiento>> UpdateTipoDeMantenimiento(EntTipoDeMantenimiento entTipoDeMantenimiento);
        Task<Response<EntTipoDeMantenimiento>> UpdateTipoDeMantenimientoStatus(EntTipoDeMantenimiento entTipoDeMantenimiento);
        Task<Response<bool>> DeleteTipoDeMantenimientoById(Guid id);
        Task<Response<List<EntTipoDeMantenimiento>>> GetTipoDeMantenimientoList();
    }
}
