using AutoMapper;
using Business.Data;
using Data.cs.Entities;
using Entities;
using Entities.Models;
using Entities.Request.Secciones;
using Microsoft.EntityFrameworkCore;

namespace Data.cs.Commands
{
    public class SeccionesRepositorio : ISeccionesRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;

        public SeccionesRepositorio(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<EntSecciones>> DSave(EntSecciones entity)
        {
            var response = new Response<EntSecciones>();
            try
            {
                var newItem = _mapper.Map<Secciones>(entity);
                newItem.bEliminado = false;
                dbContext.Secciones.Add(newItem);
                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    response.SetError("Error al guardar el registro");
                }
                else
                {
                    response.SetSuccess(_mapper.Map<EntSecciones>(newItem));
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntSecciones>> DUpdate(EntSecciones entity)
        {
            var response = new Response<EntSecciones>();
            try
            {
                var bEntity = dbContext.Secciones.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                if (bEntity != null)
                {
                    bEntity.sNombre = entity.sNombre;
                    bEntity.uIdZona = entity.uIdZona;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntSecciones>(bEntity), "Actualizado correctamente");
                    else
                    {
                        response.SetError("Registro no actualizado");
                        response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Sección no encontrada");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntSecciones>> DUpdateBoolean(EntSecciones entity)
        {
            var response = new Response<EntSecciones>();
            try
            {
                var bEntity = dbContext.Secciones.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                if (bEntity != null)
                {
                    bEntity.bEstatus = entity.bEstatus;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntSecciones>(bEntity), "Actualizado correctamente");
                    else
                    {
                        response.SetError("Registro no actualizado");
                        response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Sección no encontrada");
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
            var response = new Response<bool>();
            try
            {
                var bEntity = dbContext.Secciones.AsNoTracking().FirstOrDefault(x => x.uId == uId);
                if (bEntity != null)
                {
                    bEntity.bEliminado = true;
                    bEntity.dtFechaEliminado = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(true, "Eliminado correctamente");
                    else
                    {
                        response.SetError("Registro no eliminado");
                        response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Sección no encontrada");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntSecciones>> DGetById(Guid iKey)
        {
            var response = new Response<EntSecciones>();
            try
            {
                var entity = await dbContext.Secciones.AsNoTracking().SingleOrDefaultAsync(x => x.uId == iKey && !x.bEliminado);
                if (entity != null)
                    response.SetSuccess(_mapper.Map<EntSecciones>(entity));
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

        public async Task<Response<List<EntSecciones>>> DGetByName(string sNombre, Guid uIdZona)
        {
            var response = new Response<List<EntSecciones>>();
            try
            {
                var items = await dbContext.Secciones.AsNoTracking().Where(x => !x.bEliminado && x.sNombre.ToLower() == sNombre.ToLower() && x.uIdZona == uIdZona).ToListAsync();
                if (items.Any())
                    response.SetSuccess(_mapper.Map<List<EntSecciones>>(items));
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

        public async Task<Response<List<EntSecciones>>> DGetByFilters(EntSeccionSearchRequest filtros)
        {
            var response = new Response<List<EntSecciones>>();
            try
            {
                var query = dbContext.Secciones.AsNoTracking().Where(x => !x.bEliminado);

                if (!string.IsNullOrWhiteSpace(filtros.sNombre))
                    query = query.Where(x => x.sNombre.Contains(filtros.sNombre, StringComparison.OrdinalIgnoreCase));

                if (filtros.bEstatus.HasValue)
                    query = query.Where(x => x.bEstatus == filtros.bEstatus);

                var items = await query.ToListAsync();

                if (items.Any())
                    response.SetSuccess(_mapper.Map<List<EntSecciones>>(items));
                else
                {
                    response.SetError("No se encontraron registros");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<List<EntSecciones>>> DGetList(Guid uIdZona)
        {
            var response = new Response<List<EntSecciones>>();
            try
            {
                var items = await dbContext.Secciones.AsNoTracking().Where(x => !x.bEliminado && x.uIdZona == uIdZona).ToListAsync();
                if (items.Any())
                    response.SetSuccess(_mapper.Map<List<EntSecciones>>(items));
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

}
