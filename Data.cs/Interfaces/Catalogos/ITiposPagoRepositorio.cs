using Entities.Request.TipoPagos;
using Entities;

namespace Business.Data
{
    public interface ITiposPagoRepositorio
    {
        Task<Response<List<EntTiposPago>>> GetByName(string sNombre);
        Task<Response<EntTiposPago>> Save(EntTiposPago entity);
        Task<Response<EntTiposPago>> Update(EntTiposPago entity);
        Task<Response<EntTiposPago>> UpdateEstatus(Guid uId, bool? bEstatus);
        Task<Response<bool>> UpdateEliminado(Guid uId);
        Task<Response<EntTiposPago>> GetById(Guid uId);
        Task<Response<List<EntTiposPago>>> GetByFilters(EntTiposPagoSearchRequest filtros);
        Task<Response<List<EntTiposPago>>> GetList();
    }
}
