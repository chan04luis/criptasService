using Entities.Models;
using Entities.Request.Secciones;
using Entities;

namespace Business.Interfaces
{
    public interface IBusSecciones
    {
        Task<Response<EntSecciones>> ValidateAndSaveSection(EntSeccionRequest seccion);
        Task<Response<EntSecciones>> SaveSection(EntSecciones seccion);
        Task<Response<EntSecciones>> ValidateAndUpdateSection(EntSeccionesUpdateRequest seccion);
        Task<Response<EntSecciones>> UpdateSection(EntSecciones seccion);
        Task<Response<EntSecciones>> UpdateSectionStatus(EntSeccionesUpdateEstatusRequest seccion);
        Task<Response<bool>> DeleteSectionById(Guid id);
        Task<Response<EntSecciones>> GetSectionById(Guid id);
        Task<Response<List<EntSecciones>>> GetSectionsByFilters(EntSeccionSearchRequest filtros);
        Task<Response<List<EntSecciones>>> GetSectionList(Guid uIdIglesia);
    }

}
