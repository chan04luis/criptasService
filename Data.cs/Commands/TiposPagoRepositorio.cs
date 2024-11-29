using AutoMapper;
using Business.Data;
using Data.cs.Entities;
using Entities;
using Entities.Request.TipoPagos;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Data.cs.Commands
{
    public class TiposDePagoRepositorio : ITiposPagoRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;

        public TiposDePagoRepositorio(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<List<EntTiposPago>>> GetByName(string sNombre)
        {
            var response = new Response<List<EntTiposPago>>();

            try
            {
                var tiposPago = await dbContext.TiposDePagos
                    .Where(tp => tp.sNombre == sNombre && !tp.bEliminado).AsNoTracking()
                    .ToListAsync();

                response.Result = _mapper.Map<List<EntTiposPago>>(tiposPago);
                return response;
            }
            catch (Exception ex)
            {
                response.SetError($"Error al consultar tipos de pago por nombre: {ex.Message}");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntTiposPago>> Save(EntTiposPago entity)
        {
            var response = new Response<EntTiposPago>();
            try
            {
                var newItem = _mapper.Map<TiposDePago>(entity);
                newItem.uId = Guid.NewGuid();
                newItem.bEliminado = false;
                newItem.dtFechaRegistro = DateTime.Now.ToLocalTime();
                dbContext.TiposDePagos.Add(newItem);

                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    response.HttpCode = HttpStatusCode.BadRequest;
                    response.SetError("Error al guardar el registro");
                }
                else
                {
                    response.SetSuccess(_mapper.Map<EntTiposPago>(newItem));
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntTiposPago>> Update(EntTiposPago entity)
        {
            var response = new Response<EntTiposPago>();
            try
            {
                var bEntity = dbContext.TiposDePagos.AsNoTracking().FirstOrDefault(tp => tp.uId == entity.uId);
                if (bEntity != null)
                {
                    bEntity.sNombre = entity.sNombre;
                    bEntity.sDescripcion = entity.sDescripcion;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                    bEntity.bEstatus = entity.bEstatus;

                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntTiposPago>(bEntity), "Actualizado correctamente");
                    else
                    {
                        response.SetError("Registro no actualizado");
                        response.HttpCode = HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Tipo de pago no encontrado");
                    response.HttpCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntTiposPago>> UpdateEstatus(Guid uId, bool? bEstatus)
        {
            var response = new Response<EntTiposPago>();
            try
            {
                var bEntity = dbContext.TiposDePagos.AsNoTracking().FirstOrDefault(tp => tp.uId == uId);
                if (bEntity != null)
                {
                    bEntity.bEstatus = bEstatus;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();

                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntTiposPago>(bEntity), "Estado actualizado correctamente");
                    else
                    {
                        response.SetError("Registro no actualizado");
                        response.HttpCode = HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Tipo de pago no encontrado");
                    response.HttpCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<bool>> UpdateEliminado(Guid uId)
        {
            var response = new Response<bool>();
            try
            {
                var bEntity = dbContext.TiposDePagos.AsNoTracking().FirstOrDefault(tp => tp.uId == uId);
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
                        response.HttpCode = HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Tipo de pago no encontrado");
                    response.HttpCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntTiposPago>> GetById(Guid uId)
        {
            var response = new Response<EntTiposPago>();
            try
            {
                var entity = await dbContext.TiposDePagos.AsNoTracking()
                    .SingleOrDefaultAsync(tp => tp.uId == uId && !tp.bEliminado);
                if (entity != null)
                    response.SetSuccess(_mapper.Map<EntTiposPago>(entity));
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

        public async Task<Response<List<EntTiposPago>>> GetByFilters(EntTiposPagoSearchRequest filtros)
        {
            var response = new Response<List<EntTiposPago>>();
            try
            {
                var query = dbContext.TiposDePagos.AsNoTracking().Where(tp => !tp.bEliminado);

                if (!string.IsNullOrWhiteSpace(filtros.sNombre))
                    query = query.Where(tp => tp.sNombre.Contains(filtros.sNombre));

                if (filtros.bEstatus.HasValue)
                    query = query.Where(tp => tp.bEstatus == filtros.bEstatus);

                var items = await query.ToListAsync();

                if (items.Any())
                    response.SetSuccess(_mapper.Map<List<EntTiposPago>>(items));
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

        public async Task<Response<List<EntTiposPago>>> GetList()
        {
            var response = new Response<List<EntTiposPago>>();
            try
            {
                var items = await dbContext.TiposDePagos.AsNoTracking().Where(tp => !tp.bEliminado).ToListAsync();

                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntTiposPago>>(items));
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
}
