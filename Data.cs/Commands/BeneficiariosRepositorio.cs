using AutoMapper;
using Data.cs.Entities;
using Data.cs;
using System.Net;
using Entities;
using Microsoft.EntityFrameworkCore;

public class BeneficiariosRepositorio : IBeneficiariosRepositorio
{
    private readonly ApplicationDbContext dbContext;
    private readonly IMapper _mapper;

    public BeneficiariosRepositorio(ApplicationDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Response<EntBeneficiarios>> DSave(EntBeneficiarios entity)
    {
        var response = new Response<EntBeneficiarios>();

        try
        {
            var newItem = _mapper.Map<Beneficiarios>(entity);
            newItem.bEliminado = false;
            newItem.dtFechaRegistro = DateTime.Now.ToLocalTime();
            dbContext.Beneficiarios.Add(newItem);
            int result = await dbContext.SaveChangesAsync();

            if (result > 0)
            {
                response.SetSuccess(_mapper.Map<EntBeneficiarios>(newItem));
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

    public async Task<Response<EntBeneficiarios>> DUpdate(EntBeneficiarios entity)
    {
        var response = new Response<EntBeneficiarios>();

        try
        {
            var existingEntity = await dbContext.Beneficiarios.AsNoTracking()
                .FirstOrDefaultAsync(b => b.uId == entity.uId);

            if (existingEntity != null)
            {
                existingEntity.sNombre = entity.sNombre;
                existingEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.Beneficiarios.Update(existingEntity);
                int result = await dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    response.SetSuccess(_mapper.Map<EntBeneficiarios>(existingEntity), "Actualizado correctamente");
                }
                else
                {
                    response.SetError("Registro no actualizado");
                    response.HttpCode = HttpStatusCode.BadRequest;
                }
            }
            else
            {
                response.SetError("Beneficiario no encontrado");
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
            var entity = await dbContext.Beneficiarios.AsNoTracking()
                .FirstOrDefaultAsync(b => b.uId == uId);

            if (entity != null)
            {
                entity.bEliminado = true;
                entity.dtFechaEliminado = DateTime.Now.ToLocalTime();
                dbContext.Beneficiarios.Update(entity);
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
                response.SetError("Beneficiario no encontrado");
                response.HttpCode = HttpStatusCode.NotFound;
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }

        return response;
    }

    public async Task<Response<EntBeneficiarios>> DGetById(Guid uId)
    {
        var response = new Response<EntBeneficiarios>();

        try
        {
            var entity = await dbContext.Beneficiarios.AsNoTracking()
                .FirstOrDefaultAsync(b => b.uId == uId && !b.bEliminado);

            if (entity != null)
            {
                response.SetSuccess(_mapper.Map<EntBeneficiarios>(entity));
            }
            else
            {
                response.SetError("Beneficiario no encontrado");
                response.HttpCode = HttpStatusCode.NotFound;
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }

        return response;
    }

    public async Task<Response<List<EntBeneficiarios>>> DGetByFilters(EntBeneficiariosSearchRequest filtros)
    {
        var response = new Response<List<EntBeneficiarios>>();

        try
        {
            var query = dbContext.Beneficiarios.AsNoTracking()
                .Where(b => !b.bEliminado);

            if (!string.IsNullOrWhiteSpace(filtros.sNombre))
                query = query.Where(b => b.sNombre.Contains(filtros.sNombre));

            if (filtros.bEstatus.HasValue)
                query = query.Where(b => b.bEstatus == filtros.bEstatus);

            var items = await query.ToListAsync();

            if (items.Any())
            {
                response.SetSuccess(_mapper.Map<List<EntBeneficiarios>>(items));
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

    public async Task<Response<EntBeneficiarios>> DUpdateBoolean(EntBeneficiariosUpdateEstatusRequest entity)
    {
        var response = new Response<EntBeneficiarios>();

        try
        {
            var bEntity = await dbContext.Beneficiarios.AsNoTracking()
                .FirstOrDefaultAsync(b => b.uId == entity.uId);

            if (entity != null)
            {
                bEntity.bEstatus = entity.bEstatus;
                bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();

                dbContext.Beneficiarios.Update(bEntity);
                int result = await dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    response.SetSuccess(_mapper.Map<EntBeneficiarios>(bEntity), "Estatus actualizado correctamente");
                }
                else
                {
                    response.SetError("No se pudo actualizar el estatus");
                    response.HttpCode = HttpStatusCode.BadRequest;
                }
            }
            else
            {
                response.SetError("Beneficiario no encontrado");
                response.HttpCode = HttpStatusCode.NotFound;
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex.Message);
        }

        return response;
    }

    public async Task<Response<List<EntBeneficiarios>>> DGetList(Guid uIdCripta)
    {
        var response = new Response<List<EntBeneficiarios>>();

        try
        {
            var items = await dbContext.Beneficiarios.AsNoTracking()
                .Where(b => !b.bEliminado && b.uIdCripta==uIdCripta).ToListAsync();

            if (items.Any())
            {
                response.SetSuccess(_mapper.Map<List<EntBeneficiarios>>(items));
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
