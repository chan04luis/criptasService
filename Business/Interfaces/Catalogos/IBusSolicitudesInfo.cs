using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Business.Interfaces.Catalogos
{
    public interface IBusSolicitudesInfo
    {
        Task<Response<EntSolicitudesInfo>> SaveSolicitud(EntSolicitudesInfo solicitud);

        Task<Response<EntSolicitudesInfo>> UpdateSolicitud(EntSolicitudesInfo entSolicitud);

        Task<Response<bool>> DeleteSolicitudById(Guid id);

        Task<Response<EntSolicitudesInfo>> GetSolicitudById(Guid id);

        Task<Response<List<EntSolicitudesInfo>>> GetSolicitudList();
        Task<Response<List<EntSolicitudesInfo>>> GetListActive();
    }
}
