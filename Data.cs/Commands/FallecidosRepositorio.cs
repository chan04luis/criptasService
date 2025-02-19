using AutoMapper;
using Data.cs;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Data.cs.Entities.Catalogos;
using Utils;
using Models.Models;
using Models.Request.Fallecidos;
using System;
using Models.Responses.Criptas;
using System.Linq;
using Utils.Implementation;
using System.Collections.Generic;
using Utils.Interfaces;

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
            newItem.dtFechaActualizacion = DateTime.Now.ToLocalTime();
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
                existingEntity.dtFechaNacimiento = entity.dtFechaNacimiento;
                existingEntity.dtFechaFallecimiento = entity.dtFechaFallecimiento;
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


    public async Task<Response<EntFallecidos>> DUpdateDocs(EntFallecidos entity)
    {
        var response = new Response<EntFallecidos>();

        try
        {
            var existingEntity = await dbContext.Fallecidos.AsNoTracking().Where(b => !b.bEliminado)
                .FirstOrDefaultAsync(f => f.uId == entity.uId);

            if (existingEntity != null)
            {
                existingEntity.sActaDefuncion = entity.sActaDefuncion;
                existingEntity.sAutorizacionTraslado = entity.sAutorizacionTraslado;
                existingEntity.sAutorizacionIncineracion = entity.sAutorizacionIncineracion;
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

            var items = await query.Select(x=> new EntFallecidos()
            {
                sNombre=x.sNombre,
                sApellidos=x.sApellidos,
                uId=x.uId,
                iEdad = (x.dtFechaFallecimiento!=null && x.dtFechaNacimiento != null) ? DateTime.Parse(x.dtFechaFallecimiento).Year - DateTime.Parse(x.dtFechaNacimiento).Year : 0,
                dtFechaActualizacion=x.dtFechaActualizacion,
                dtFechaFallecimiento=x.dtFechaFallecimiento,
                dtFechaNacimiento=x.dtFechaNacimiento,
                dtFechaRegistro=x.dtFechaRegistro,
                bEstatus=x.bEstatus.Value,
                uIdCripta=x.uIdCripta
            }).ToListAsync();

            if (items.Any())
            {
                response.SetSuccess(items);
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
                .Where(f => !f.bEliminado && f.uIdCripta == uIdCripta)
                .Select(x => new EntFallecidos()
                {
                    sNombre = x.sNombre,
                    sApellidos = x.sApellidos,
                    uId = x.uId,
                    iEdad = (x.dtFechaFallecimiento != null && x.dtFechaNacimiento != null) ? DateTime.Parse(x.dtFechaFallecimiento).Year - DateTime.Parse(x.dtFechaNacimiento).Year : 0,
                    dtFechaActualizacion = x.dtFechaActualizacion,
                    dtFechaFallecimiento = x.dtFechaFallecimiento,
                    dtFechaNacimiento = x.dtFechaNacimiento,
                    dtFechaRegistro = x.dtFechaRegistro,
                    bEstatus = x.bEstatus.Value,
                    uIdCripta = x.uIdCripta
                }).ToListAsync();

            if (items.Any())
            {
                response.SetSuccess(items);
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

    public async Task<Response<PagedResult<FallecidosBusqueda>>> DGetFallecidos(EntFallecidosSearchRequest fallecido)
    {
        var response = new Response<PagedResult<FallecidosBusqueda>>();
        try
        {
            var items = (
                from f in dbContext.Fallecidos
                join c in dbContext.Criptas on f.uIdCripta equals c.uId
                join s in dbContext.Secciones on c.uIdSeccion equals s.uId
                join z in dbContext.Zonas on s.uIdZona equals z.uId
                join i in dbContext.Iglesias on z.uIdIglesia equals i.uId
                where !f.bEliminado && !c.bEliminado && !s.bEliminado && !z.bEliminado && !i.bEliminado
                      && f.sNombre.ToLower().Contains(fallecido.sNombre.ToLower()) && f.sApellidos.ToLower().Contains(fallecido.sApellidos.ToLower())
                select new FallecidosBusqueda
                {
                    uId = f.uId,
                    uIdIglesia = i.uId,
                    sNombres = f.sNombre,
                    sApellidos = f.sApellidos,
                    iEdad = f.dtFechaNacimiento != null && f.dtFechaFallecimiento != null
                            ? (DateTime.Parse(f.dtFechaFallecimiento).Year - DateTime.Parse(f.dtFechaNacimiento).Year)
                            : 0,
                    sFechaNacido = f.dtFechaNacimiento ?? "No registrado",
                    sFechaFallecido = f.dtFechaFallecimiento ?? "No registrado",
                    sNombre = c.sNumero,
                    sNombreSeccion = s.sNombre,
                    sNombreZona = z.sNombre,
                    sIglesia = i.sNombre
                }
            );
            if (fallecido.uIdIglesia != null)
            {
                items = items.Where(x => x.uIdIglesia == fallecido.uIdIglesia);
            }
            int totalRecords = await items.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalRecords / fallecido.iNumReg);
            if (items.Any())
            {
                var result = await items
                    .OrderBy(x => x.sNombre)
                    .ThenBy(x => x.sApellidos)
                    .ThenBy(x => x.uId)
                    .Skip((fallecido.iNumPag - 1) * fallecido.iNumReg)
                    .Take(fallecido.iNumReg)
                    .ToListAsync();
                var resultado = new PagedResult<FallecidosBusqueda>(result, totalRecords, fallecido.iNumPag, fallecido.iNumReg);
                response.SetSuccess(resultado);
            }
            else
            {
                response.SetError("Sin registros");
                response.HttpCode = System.Net.HttpStatusCode.NotFound;
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex.Message);
        }
        return response;
    }

}
