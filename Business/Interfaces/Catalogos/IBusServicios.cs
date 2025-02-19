using Models.Models;
using Models.Responses.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Business.Interfaces.Catalogos
{
    public interface IBusServicios
    {
        Task<Response<EntServicios>> SaveServicio(EntServicios servicio);
        Task<Response<EntServicios>> UpdateServicio(EntServicios entServicio);
        Task<Response<EntServicios>> UpdateServicioStatus(EntServicios entServicio);
        Task<Response<bool>> DeleteServicioById(Guid id);
        Task<Response<EntServicios>> BGetById(Guid id);
        Task<Response<List<EntServicios>>> GetServicioList();
        Task<Response<List<EntServicios>>> GetListActive();
        Task<Response<List<EntServiceItem>>> GetListActive(Guid uIdIglesia);
    }
}
