using AutoMapper;
using Data.cs.Entities;
using Data.cs;
using System.Net;
using Entities;
using Microsoft.EntityFrameworkCore;
using Entities.Request.Visitas;

public class VisitasRepositorio : IVisitasRepositorio
{
    private readonly ApplicationDbContext dbContext;
    private readonly IMapper _mapper;

    public VisitasRepositorio(ApplicationDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Response<EntVisitas>> DSave(EntVisitas entity)
    {
        var response = new Response<EntVisitas>();

        try
        {
            var newItem = _mapper.Map<Visitas>(entity);
            newItem.bEliminado = false;
            dbContext.Visitas.Add(newItem);
            int result = await dbContext.SaveChangesAsync();

            if (result > 0)
            {
                response.SetSuccess(_mapper.Map<EntVisitas>(newItem));
            }
            else
            {
                response.SetError("Error al guardar el registro");
                response.HttpCode = HttpStatusCode.BadRequest;
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }

        return response;
    }

    public async Task<Response<EntVisitas>> DUpdate(EntVisitas entity)
    {
        var response = new Response<EntVisitas>();

        try
        {
            var existingEntity = await dbContext.Visitas.AsNoTracking().Where(b => !b.bEliminado)
                .FirstOrDefaultAsync(v => v.uId == entity.uId);

            if (existingEntity != null)
            {
                existingEntity.sNombreVisitante = entity.sNombreVisitante;
                existingEntity.uIdCriptas = entity.uIdCriptas;
                existingEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.Visitas.Update(existingEntity);
                int result = await dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    response.SetSuccess(_mapper.Map<EntVisitas>(existingEntity), "Actualizado correctamente");
                }
                else
                {
                    response.SetError("Registro no actualizado");
                    response.HttpCode = HttpStatusCode.BadRequest;
                }
            }
            else
            {
                response.SetError("Registro no encontrado");
                response.HttpCode = HttpStatusCode.NotFound;
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }

        return response;
    }

    public async Task<Response<bool>> DUpdateEliminado(Guid uId)
    {
        var response = new Response<bool>();

        try
        {
            var entity = await dbContext.Visitas.AsNoTracking().Where(b => !b.bEliminado)
                .FirstOrDefaultAsync(v => v.uId == uId);

            if (entity != null)
            {
                entity.bEliminado = true;
                entity.dtFechaEliminado = DateTime.Now.ToLocalTime();
                dbContext.Visitas.Update(entity);
                int result = await dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    response.SetSuccess(true, "Eliminado correctamente");
                }
                else
                {
                    response.SetError("Error al eliminar");
                    response.HttpCode = HttpStatusCode.BadRequest;
                }
            }
            else
            {
                response.SetError("Registro no encontrado");
                response.HttpCode = HttpStatusCode.NotFound;
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }

        return response;
    }

    public async Task<Response<EntVisitas>> DGetById(Guid uId)
    {
        var response = new Response<EntVisitas>();

        try
        {
            var entity = await dbContext.Visitas.AsNoTracking().Where(b => !b.bEliminado)
                .FirstOrDefaultAsync(v => v.uId == uId);

            if (entity != null)
            {
                response.SetSuccess(_mapper.Map<EntVisitas>(entity));
            }
            else
            {
                response.SetError("Registro no encontrado");
                response.HttpCode = HttpStatusCode.NotFound;
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }

        return response;
    }

    public async Task<Response<List<EntVisitas>>> DGetByFilters(EntVisitasSearchRequest filtros)
    {
        var response = new Response<List<EntVisitas>>();

        try
        {
            var query = dbContext.Visitas.AsNoTracking()
                .Where(v => !v.bEliminado);

            if (!string.IsNullOrWhiteSpace(filtros.sNombreVisitante))
                query = query.Where(v => v.sNombreVisitante.Contains(filtros.sNombreVisitante));

            if (filtros.uIdCriptas.HasValue)
                query = query.Where(v => v.uIdCriptas == filtros.uIdCriptas);

            var items = await query.ToListAsync();

            if (items.Any())
            {
                response.SetSuccess(_mapper.Map<List<EntVisitas>>(items));
            }
            else
            {
                response.SetError("No se encontraron registros");
                response.HttpCode = HttpStatusCode.NotFound;
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex.Message);
        }

        return response;
    }
}
