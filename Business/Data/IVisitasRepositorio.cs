using Entities;
using Entities.Request.Visitas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IVisitasRepositorio
{
    Task<Response<EntVisitas>> DSave(EntVisitas entity);
    Task<Response<EntVisitas>> DUpdate(EntVisitas entity);
    Task<Response<bool>> DUpdateEliminado(Guid uId);
    Task<Response<EntVisitas>> DGetById(Guid uId);
    Task<Response<List<EntVisitas>>> DGetByFilters(EntVisitasSearchRequest filtros);
}
