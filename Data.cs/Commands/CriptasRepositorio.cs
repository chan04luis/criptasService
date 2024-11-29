
using AutoMapper;
using Business.Data;
using Data.cs.Entities;
using Entities;
using Entities.Models;
using Entities.Request.Criptas;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Data.cs.Commands
{
    public class CriptasRepositorio : ICriptasRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;

        public CriptasRepositorio(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Response<List<EntCriptas>>> DGetByName(string nombre, Guid uIdSeccion)
        {
            var response = new Response<List<EntCriptas>>();

            try
            {
                var criptas = await dbContext.Criptas.AsNoTracking()
                    .Where(c => c.sNumero == nombre && c.uIdSeccion == uIdSeccion && !c.bEliminado)
                    .ToListAsync();

                response.Result = _mapper.Map<List<EntCriptas>>(criptas);
                return response;
            }
            catch (Exception ex)
            {
                response.SetError($"Error al consultar criptas por nombre: {ex.Message}");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
        public async Task<Response<EntCriptas>> DSave(EntCriptas entity)
        {
            var response = new Response<EntCriptas>();
            try
            {
                var newItem = _mapper.Map<Criptas>(entity);
                newItem.bEliminado = false;
                dbContext.Criptas.Add(newItem);
                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    response.SetError("Error al guardar el registro");
                }
                else
                {
                    response.SetSuccess(_mapper.Map<EntCriptas>(newItem));
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntCriptas>> DUpdate(EntCriptas entity)
        {
            var response = new Response<EntCriptas>();
            try
            {
                var bEntity = dbContext.Criptas.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                if (bEntity != null)
                {
                    bEntity.uIdSeccion = entity.uIdSeccion;
                    bEntity.uIdCliente = entity.uIdCliente;
                    bEntity.sNumero = entity.sNumero;
                    bEntity.sUbicacionEspecifica = entity.sUbicacionEspecifica;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntCriptas>(bEntity), "Actualizado correctamente");
                    else
                    {
                        response.SetError("Registro no actualizado");
                        response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Cripta no encontrada");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntCriptas>> DUpdateBoolean(EntCriptas entity)
        {
            var response = new Response<EntCriptas>();
            try
            {
                var bEntity = dbContext.Criptas.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                if (bEntity != null)
                {
                    bEntity.bEstatus = entity.bEstatus;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntCriptas>(bEntity), "Actualizado correctamente");
                    else
                    {
                        response.SetError("Registro no actualizado");
                        response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Cripta no encontrada");
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
                var bEntity = dbContext.Criptas.AsNoTracking().FirstOrDefault(x => x.uId == uId);
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
                    response.SetError("Cripta no encontrada");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntCriptas>> DGetById(Guid iKey)
        {
            var response = new Response<EntCriptas>();
            try
            {
                var entity = await dbContext.Criptas.AsNoTracking().SingleOrDefaultAsync(x => x.uId == iKey && !x.bEliminado);
                if (entity != null)
                    response.SetSuccess(_mapper.Map<EntCriptas>(entity));
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

        public async Task<Response<List<EntCriptas>>> DGetByFilters(EntCriptaSearchRequest filtros)
        {
            var response = new Response<List<EntCriptas>>();
            try
            {
                var query = dbContext.Criptas.AsNoTracking().Where(x => !x.bEliminado);

                if (filtros.uIdCliente.HasValue)
                    query = query.Where(x => x.uIdCliente == filtros.uIdCliente);

                if (filtros.uIdSeccion.HasValue)
                    query = query.Where(x => x.uIdSeccion == filtros.uIdSeccion);

                if (!string.IsNullOrWhiteSpace(filtros.sNumero))
                    query = query.Where(x => x.sNumero.Contains(filtros.sNumero));

                if (filtros.bEstatus.HasValue)
                    query = query.Where(x => x.bEstatus == filtros.bEstatus);

                var items = await query.ToListAsync();

                if (items.Any())
                    response.SetSuccess(_mapper.Map<List<EntCriptas>>(items));
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

        public async Task<Response<List<EntCriptas>>> DGetList(Guid uIdSeccion)
        {
            var response = new Response<List<EntCriptas>>();
            try
            {
                var items = await dbContext.Criptas.AsNoTracking().Where(x => !x.bEliminado && x.uIdSeccion == uIdSeccion).ToListAsync();
                if (items.Any())
                    response.SetSuccess(_mapper.Map<List<EntCriptas>>(items));
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
