using AutoMapper;
using Data.cs.Entities;
using Data.cs;
using System.Net;
using Entities;
using Microsoft.EntityFrameworkCore;
using Entities.Request.Fallecidos;

public class FallecidosRepositorio : IFallecidosRepositorio
{
    private readonly ApplicationDbContext dbContext;
    private readonly IMapper _mapper;

    public FallecidosRepositorio(ApplicationDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Response<EntFallecidos>> DSave(EntFallecidos entity)
    {
        var response = new Response<EntFallecidos>();

        try
        {
            var newItem = _mapper.Map<Fallecidos>(entity);
            newItem.bEliminado = false;
            newItem.dtFechaRegistro = DateTime.Now.ToLocalTime();
            dbContext.Fallecidos.Add(newItem);
            int result = await dbContext.SaveChangesAsync();

            if (result > 0)
            {
                response.SetSuccess(_mapper.Map<EntFallecidos>(newItem));
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

    public async Task<Response<EntFallecidos>> DUpdate(EntFallecidos entity)
    {
        var response = new Response<EntFallecidos>();

        try
        {
            var existingEntity = await dbContext.Fallecidos.AsNoTracking().Where(b => !b.bEliminado)
                .FirstOrDefaultAsync(f => f.uId == entity.uId);

            if (existingEntity != null)
            {
                existingEntity.sNombre = entity.sNombre;
                existingEntity.uIdCripta = entity.uIdCripta;
                existingEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.Fallecidos.Update(existingEntity);
                int result = await dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    response.SetSuccess(_mapper.Map<EntFallecidos>(existingEntity), "Actualizado correctamente");
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
            var entity = await dbContext.Fallecidos.AsNoTracking().Where(b => !b.bEliminado)
                .FirstOrDefaultAsync(f => f.uId == uId);

            if (entity != null)
            {
                entity.bEliminado = true;
                entity.dtFechaEliminado = DateTime.Now.ToLocalTime();
                dbContext.Fallecidos.Update(entity);
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

    public async Task<Response<EntFallecidos>> DGetById(Guid uId)
    {
        var response = new Response<EntFallecidos>();

        try
        {
            var entity = await dbContext.Fallecidos.AsNoTracking().Where(b => !b.bEliminado)
                .FirstOrDefaultAsync(f => f.uId == uId && !f.bEliminado);

            if (entity != null)
            {
                response.SetSuccess(_mapper.Map<EntFallecidos>(entity));
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

    public async Task<Response<List<EntFallecidos>>> DGetByFilters(EntFallecidosSearchRequest filtros)
    {
        var response = new Response<List<EntFallecidos>>();

        try
        {
            var query = dbContext.Fallecidos.AsNoTracking()
                .Where(f => !f.bEliminado);

            if (!string.IsNullOrWhiteSpace(filtros.sNombre))
                query = query.Where(f => f.sNombre.Contains(filtros.sNombre));

            if (filtros.bEstatus.HasValue)
                query = query.Where(f => f.bEstatus == filtros.bEstatus);

            var items = await query.ToListAsync();

            if (items.Any())
            {
                response.SetSuccess(_mapper.Map<List<EntFallecidos>>(items));
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

    public async Task<Response<EntFallecidos>> DUpdateBoolean(EntFallecidosUpdateEstatusRequest entity)
    {
        var response = new Response<EntFallecidos>();

        try
        {
            var fEntity = await dbContext.Fallecidos.AsNoTracking().Where(b => !b.bEliminado)
                .FirstOrDefaultAsync(f => f.uId == entity.uId);

            if (fEntity != null)
            {
                fEntity.bEstatus = entity.bEstatus;
                fEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();

                dbContext.Fallecidos.Update(fEntity);
                int result = await dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    response.SetSuccess(_mapper.Map<EntFallecidos>(fEntity), "Estatus actualizado correctamente");
                }
                else
                {
                    response.SetError("No se pudo actualizar el estatus");
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
            response.SetError(ex.Message);
        }

        return response;
    }

    public async Task<Response<List<EntFallecidos>>> DGetList(Guid uIdCripta)
    {
        var response = new Response<List<EntFallecidos>>();

        try
        {
            var items = await dbContext.Fallecidos.AsNoTracking()
                .Where(f => !f.bEliminado && f.uIdCripta == uIdCripta).ToListAsync();

            if (items.Any())
            {
                response.SetSuccess(_mapper.Map<List<EntFallecidos>>(items));
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
