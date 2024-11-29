
using AutoMapper;
using Business.Data;
using Data.cs.Entities;
using Entities;
using Entities.JsonRequest.Iglesias;
using Entities.Models;
using Entities.Responses.Iglesia;
using Microsoft.EntityFrameworkCore;

namespace Data.cs.Commands
{
    public class IglesiasRepositorio : IIglesiasRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;

        public IglesiasRepositorio(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<EntIglesias>> DSave(EntIglesias entity)
        {
            var response = new Response<EntIglesias>();
            try
            {
                var newItem = _mapper.Map<Iglesias>(entity);
                newItem.bEliminado = false;
                dbContext.Iglesias.Add(newItem);
                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    response.SetError("Error al guardar el registro");
                }
                else
                {
                    response.SetSuccess(_mapper.Map<EntIglesias>(newItem));
                }

            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntIglesias>> DUpdate(EntIglesias entity)
        {
            Response<EntIglesias> response = new Response<EntIglesias>();

            try
            {
                var bEntity = dbContext.Iglesias.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                bEntity.sNombre = entity.sNombre;
                bEntity.sDireccion = entity.sDireccion;
                bEntity.sCiudad = entity.sCiudad;
                bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.Update(bEntity);
                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                    response.SetSuccess(_mapper.Map<EntIglesias>(bEntity), "Actualizado correctamente");
                else
                {
                    response.SetError("Registro no actualizado");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntIglesias>> DUpdateBoolean(EntIglesias entity)
        {
            Response<EntIglesias> response = new Response<EntIglesias>();

            try
            {
                var bEntity = dbContext.Iglesias.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                bEntity.bEstatus = entity.bEstatus;
                bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.Update(bEntity);
                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                    response.SetSuccess(_mapper.Map<EntIglesias>(bEntity), "Actualizado correctamente");
                else
                {
                    response.SetError("Registro no actualizado");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
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
                var bEntity = dbContext.Iglesias.AsNoTracking().FirstOrDefault(x => x.uId == uId);
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
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntIglesiaResponse>> DGetById(Guid iKey)
        {
            var response = new Response<EntIglesiaResponse>();
            try
            {
                var entity = await dbContext.Iglesias
                    .AsNoTracking()
                    .Where(x => x.bEliminado == false)
                    .Include(x => x.listZonas)
                    .SingleOrDefaultAsync(x => x.uId == iKey);
                if (entity != null)
                    response.SetSuccess(_mapper.Map<EntIglesiaResponse>(entity));
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

        public async Task<Response<List<EntIglesias>>> DGetByFilters(EntIglesiaSearchRequest filtros)
        {
            var response = new Response<List<EntIglesias>>();
            try
            {
                var items = await dbContext.Iglesias.AsNoTracking().Where(c => c.bEliminado == false).ToListAsync();
                if (items.Count > 0 && filtros.sNombre != null)
                {
                    items = items.Where(x => x.sNombre.ToLower().Contains(filtros.sNombre.ToLower())).ToList();
                }
                if (items.Count > 0 && filtros.sCiudad != null)
                {
                    items = items.Where(x => x.sCiudad.ToLower().Contains(filtros.sCiudad.ToLower())).ToList();
                }
                if (items.Count > 0 && filtros.bEstatus != null)
                {
                    items = items.Where(x => x.bEstatus == filtros.bEstatus).ToList();
                }
                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntIglesias>>(items));
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

        public async Task<Response<List<EntIglesias>>> DGetList()
        {
            var response = new Response<List<EntIglesias>>();
            try
            {
                var items = await dbContext.Iglesias.AsNoTracking().Where(x => x.bEliminado == false).ToListAsync();
                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntIglesias>>(items));
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
