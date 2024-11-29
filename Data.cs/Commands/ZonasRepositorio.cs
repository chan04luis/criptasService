using AutoMapper;
using Business.Data;
using Data.cs.Entities;
using Entities;
using Entities.JsonRequest.Zonas;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.cs.Commands
{
    public class ZonasRepositorio : IZonasRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;

        public ZonasRepositorio(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<EntZonas>> DSave(EntZonas entity)
        {
            var response = new Response<EntZonas>();
            try
            {
                var newItem = _mapper.Map<Zonas>(entity);
                newItem.bEliminado = false;
                dbContext.Zonas.Add(newItem);
                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    response.SetError("Error al guardar el registro");
                }
                else
                {
                    response.SetSuccess(_mapper.Map<EntZonas>(newItem));
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntZonas>> DUpdate(EntZonas entity)
        {
            Response<EntZonas> response = new Response<EntZonas>();

            try
            {
                var bEntity = dbContext.Zonas.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                if (bEntity != null)
                {
                    bEntity.sNombre = entity.sNombre;
                    bEntity.uIdIglesia = entity.uIdIglesia;
                    bEntity.bEstatus = entity.bEstatus;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntZonas>(bEntity), "Actualizado correctamente");
                    else
                    {
                        response.SetError("Registro no actualizado");
                        response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Zona no encontrada");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntZonas>> DUpdateBoolean(EntZonas entity)
        {
            Response<EntZonas> response = new Response<EntZonas>();

            try
            {
                var bEntity = dbContext.Zonas.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                if (bEntity != null)
                {
                    bEntity.bEstatus = entity.bEstatus;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntZonas>(bEntity), "Actualizado correctamente");
                    else
                    {
                        response.SetError("Registro no actualizado");
                        response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Zona no encontrada");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
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
            Response<bool> response = new Response<bool>();

            try
            {
                var bEntity = dbContext.Zonas.AsNoTracking().FirstOrDefault(x => x.uId == uId);
                if (bEntity != null)
                {
                    bEntity.bEliminado = true;
                    bEntity.dtFechaEliminado = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(true, "bEliminado correctamente");
                    else
                    {
                        response.SetError("Registro no eliminado");
                        response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Zona no encontrada");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntZonas>> DGetById(Guid iKey)
        {
            var response = new Response<EntZonas>();
            try
            {
                var entity = await dbContext.Zonas.AsNoTracking().Where(x => x.bEliminado == false).AsNoTracking()
                .SingleOrDefaultAsync(x => x.uId == iKey);
                if (entity != null)
                    response.SetSuccess(_mapper.Map<EntZonas>(entity));
                else
                {
                    response.SetError("Registro no encontrado");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<List<EntZonas>>> DGetByName(string sNombre, Guid uIdIglesia)
        {
            var response = new Response<List<EntZonas>>();
            try
            {
                var items = await dbContext.Zonas.AsNoTracking().Where(x => x.bEliminado == false && x.sNombre.ToLower() == sNombre.ToLower() && x.uIdIglesia == uIdIglesia).ToListAsync();
                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntZonas>>(items));
                else
                {
                    response.SetError("Registro no encontrado");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<List<EntZonas>>> DGetByFilters(EntZonaSearchRequest filtros)
        {
            var response = new Response<List<EntZonas>>();
            try
            {
                var items = await dbContext.Zonas.AsNoTracking().Where(z => z.bEliminado == false).ToListAsync();

                if (items.Count > 0 && filtros.sNombre != null)
                {
                    items = items.Where(x => x.sNombre.ToLower().Contains(filtros.sNombre.ToLower())).ToList();
                }

                if (items.Count > 0 && filtros.bEstatus != null)
                {
                    items = items.Where(x => x.bEstatus == filtros.bEstatus).ToList();
                }

                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntZonas>>(items));
                else
                {
                    response.SetError("Registro no encontrado");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<List<EntZonas>>> DGetList(Guid uIdIglesia)
        {
            var response = new Response<List<EntZonas>>();
            try
            {
                var items = await dbContext.Zonas.AsNoTracking().Where(x => x.bEliminado == false && x.uIdIglesia == uIdIglesia).ToListAsync();
                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntZonas>>(items));
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

        public async Task<Response<List<EntZonas>>> DGetByIglesiaId(Guid iglesiaId)
        {
            var response = new Response<List<EntZonas>>();
            try
            {
                var items = await dbContext.Zonas.AsNoTracking()
                    .Where(z => z.uIdIglesia == iglesiaId && !z.bEliminado)
                    .ToListAsync();

                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntZonas>>(items));
                else
                {
                    response.SetError("No se encontraron zonas para esta iglesia");
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

}
