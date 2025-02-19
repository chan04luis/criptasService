using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Data.cs.Interfaces.Catalogos
{
    public interface ISolicitudesInfoRepositorio
    {

        Task<Response<bool>> AnyExistKey(Guid pKey);
        Task<Response<bool>> AnyExistSolicitud(Guid idCliente, Guid idServicio, string mensaje);
        Task<Response<EntSolicitudesInfo>> DSave(EntSolicitudesInfo entity);
        Task<Response<EntSolicitudesInfo>> DUpdate(EntSolicitudesInfo entity);
        Task<Response<bool>> DDelete(Guid uId);
        Task<Response<EntSolicitudesInfo>> DGetById(Guid iKey);
        Task<Response<List<EntSolicitudesInfo>>> DGetList();
        Task<Response<List<EntSolicitudesInfo>>> DGetListActive();
    }
}
