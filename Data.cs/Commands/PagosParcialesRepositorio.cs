using AutoMapper;
using Business.Data;
using Data.cs.Entities;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.cs.Commands
{
    public class PagosParcialesRepositorio : IPagosParcialesRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;

        public PagosParcialesRepositorio(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<List<EntPagosParciales>>> DGetByPagoId(Guid uIdPago)
        {
            var response = new Response<List<EntPagosParciales>>();
            try
            {
                var pagosParciales = await dbContext.PagosParciales.AsNoTracking()
                    .Where(p => p.uIdPago == uIdPago && !p.bEliminado)
                    .ToListAsync();

                response.Result = _mapper.Map<List<EntPagosParciales>>(pagosParciales);
            }
            catch (Exception ex)
            {
                response.SetError($"Error al obtener pagos parciales: {ex.Message}");
            }
            return response;
        }

        public async Task<Response<EntPagosParciales>> DSave(EntPagosParciales entity)
        {
            var response = new Response<EntPagosParciales>();
            try
            {
                var newEntity = _mapper.Map<PagosParciales>(entity);
                newEntity.dtFechaRegistro = DateTime.Now.ToLocalTime();
                newEntity.bEliminado = false;

                dbContext.PagosParciales.Add(newEntity);
                var exec = await dbContext.SaveChangesAsync();

                response.SetSuccess(exec > 0 ? _mapper.Map<EntPagosParciales>(newEntity) : null, "Registro guardado con éxito");
            }
            catch (Exception ex)
            {
                response.SetError($"Error al guardar el registro: {ex.Message}");
            }
            return response;
        }

        public async Task<Response<EntPagosParciales>> DUpdate(EntPagosParciales entity)
        {
            var response = new Response<EntPagosParciales>();
            try
            {
                var existingEntity = await dbContext.PagosParciales.AsNoTracking().FirstOrDefaultAsync(p => p.uId == entity.uId && !p.bEliminado);
                if (existingEntity != null)
                {
                    _mapper.Map(entity, existingEntity);
                    existingEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();

                    dbContext.Update(existingEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    response.SetSuccess(exec > 0 ? _mapper.Map<EntPagosParciales>(existingEntity) : null, "Registro actualizado con éxito");
                }
                else
                {
                    response.SetError("Registro no encontrado");
                }
            }
            catch (Exception ex)
            {
                response.SetError($"Error al actualizar el registro: {ex.Message}");
            }
            return response;
        }

        public async Task<Response<bool>> DUpdateEliminado(Guid uId)
        {
            var response = new Response<bool>();
            try
            {
                var entity = await dbContext.PagosParciales.AsNoTracking().FirstOrDefaultAsync(p => p.uId == uId);
                if (entity != null)
                {
                    entity.bEliminado = true;
                    entity.dtFechaEliminado = DateTime.Now.ToLocalTime();

                    dbContext.Update(entity);
                    var exec = await dbContext.SaveChangesAsync();

                    response.SetSuccess(exec > 0, "Registro eliminado con éxito");
                }
                else
                {
                    response.SetError("Registro no encontrado");
                }
            }
            catch (Exception ex)
            {
                response.SetError($"Error al eliminar el registro: {ex.Message}");
            }
            return response;
        }

        public async Task<Response<bool>> DUpdateEliminadoByIdPago(Guid uId)
        {
            var response = new Response<bool>();
            try
            {
                var entities = await dbContext.PagosParciales.AsNoTracking().Where(p => p.uIdPago == uId && !p.bEliminado).ToListAsync();
                if (entities != null)
                {
                    foreach(var entity in entities)
                    {
                        entity.bEliminado = true;
                        entity.dtFechaEliminado = DateTime.Now.ToLocalTime();
                        dbContext.Update(entity);
                    }
                    var exec = await dbContext.SaveChangesAsync();

                    response.SetSuccess(exec > 0, "Registro eliminado con éxito");
                }
                else
                {
                    response.SetError("Registro no encontrado");
                }
            }
            catch (Exception ex)
            {
                response.SetError($"Error al eliminar el registro: {ex.Message}");
            }
            return response;
        }

        public async Task<Response<EntPagosParciales>> DGetById(Guid uId)
        {
            var response = new Response<EntPagosParciales>();
            try
            {
                var entity = await dbContext.PagosParciales.AsNoTracking()
                    .SingleOrDefaultAsync(p => p.uId == uId && !p.bEliminado);

                response.SetSuccess(entity != null ? _mapper.Map<EntPagosParciales>(entity) : null, "Registro obtenido con éxito");
            }
            catch (Exception ex)
            {
                response.SetError($"Error al obtener el registro: {ex.Message}");
            }
            return response;
        }
    }

}
