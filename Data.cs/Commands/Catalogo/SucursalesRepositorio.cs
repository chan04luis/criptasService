
using AutoMapper;
using Business.Data;
using Data.cs.Entities.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Models;
using Models.Request.Catalogo.Sucursales;
using Models.Responses.Servicio;
using Utils;

namespace Data.cs.Commands.Catalogo
{
    public class SucursalesRepositorio : ISucursalesRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<SucursalesRepositorio> _logger;

        public SucursalesRepositorio(ApplicationDbContext dbContext, IMapper mapper, ILogger<SucursalesRepositorio> logger)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<EntSucursal>> DSave(EntSucursal entity)
        {
            var response = new Response<EntSucursal>();
            try
            {
                var newItem = _mapper.Map<Sucursales>(entity);
                newItem.bEliminado = false;
                dbContext.Sucursal.Add(newItem);
                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    response.SetError("Error al guardar el registro");
                }
                else
                {
                    response.SetSuccess(_mapper.Map<EntSucursal>(newItem));
                }

            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntSucursal>> DUpdate(EntSucursal entity)
        {
            Response<EntSucursal> response = new Response<EntSucursal>();

            try
            {
                var bEntity = dbContext.Sucursal.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                bEntity.sNombre = entity.sNombre;
                bEntity.sTelefono = entity.sTelefono;
                bEntity.sDireccion = entity.sDireccion;
                bEntity.sCiudad = entity.sCiudad;
                bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.Update(bEntity);
                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                    response.SetSuccess(_mapper.Map<EntSucursal>(bEntity), "Actualizado correctamente");
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

        public async Task<Response<EntSucursal>> DUpdateMaps(EntSucursalMaps entity)
        {
            Response<EntSucursal> response = new Response<EntSucursal>();

            try
            {
                var bEntity = dbContext.Sucursal.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                bEntity.sLatitud = entity.sLatitud;
                bEntity.sLongitud = entity.sLongitud;
                bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.Update(bEntity);
                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                    response.SetSuccess(_mapper.Map<EntSucursal>(bEntity), "Actualizado correctamente");
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

        public async Task<Response<EntSucursal>> DUpdateBoolean(EntSucursal entity)
        {
            Response<EntSucursal> response = new Response<EntSucursal>();

            try
            {
                var bEntity = dbContext.Sucursal.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                bEntity.bEstatus = entity.bEstatus;
                bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.Update(bEntity);
                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                    response.SetSuccess(_mapper.Map<EntSucursal>(bEntity), "Actualizado correctamente");
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
                var bEntity = dbContext.Sucursal.AsNoTracking().FirstOrDefault(x => x.uId == uId);
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

        public async Task<Response<EntSucursal>> DGetById(Guid iKey)
        {
            var response = new Response<EntSucursal>();
            try
            {
                var entity = await dbContext.Sucursal
                    .AsNoTracking()
                    .Where(x => x.bEliminado == false)
                    .SingleOrDefaultAsync(x => x.uId == iKey);
                if (entity != null)
                    response.SetSuccess(_mapper.Map<EntSucursal>(entity));
                else
                {
                    response.SetError("Registro no encontrado");
                    response.HttpCode = System.Net.HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<List<EntSucursal>>> DGetByFilters(EntSucursalSearchRequest filtros)
        {
            var response = new Response<List<EntSucursal>>();
            try
            {
                var items = await dbContext.Sucursal.AsNoTracking().Where(c => c.bEliminado == false).ToListAsync();
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
                    response.SetSuccess(_mapper.Map<List<EntSucursal>>(items));
                else
                {
                    response.SetError("Registro no encontrado");
                    response.HttpCode = System.Net.HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<List<EntSucursal>>> DGetList()
        {
            var response = new Response<List<EntSucursal>>();
            try
            {
                var items = await dbContext.Sucursal.AsNoTracking().Where(x => x.bEliminado == false).ToListAsync();
                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntSucursal>>(items));
                else
                {
                    response.SetError("Sin registros");
                    response.HttpCode = System.Net.HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<List<EntServiceItem>>> DGetListPreAssigmentUser(Guid uId)
        {
            var response = new Response<List<EntServiceItem>>();

            try
            {
                var sucursal = await dbContext.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.uId == uId && !x.bEliminado);
                if (sucursal == null)
                {
                    response.SetError("Usuario no existe");
                    return response;
                }
                var itemsQuery = from s in dbContext.Sucursal.AsNoTracking()
                                 join su in dbContext.SucursalesUsuario.AsNoTracking().Where(x => x.IdUsuario == uId)
                                 on s.uId equals su.IdSucursal into servicioUserGroup
                                 from ss in servicioUserGroup.DefaultIfEmpty()
                                 where !s.bEliminado && s.bEstatus.Value
                                 orderby s.sNombre
                                 select new EntServiceItem
                                 {
                                     Id = s.uId,
                                     Nombre = s.sNombre,
                                     ImgPreview = "",
                                     IdAsignado = ss == null ? null : ss.Id,
                                     Asignado = ss == null ? false : ss.Asignado
                                 };

                var items = await itemsQuery.ToListAsync();

                if (items.Any())
                {
                    response.SetSuccess(items);
                    response.HttpCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    response.SetError("Sin registros");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DGetListPreAssigmentUser));
                response.SetError(ex.Message);
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<Response<bool>> DSaveToUser(List<EntServiceItem> entities, Guid uId)
        {
            var response = new Response<bool>();
            try
            {
                foreach (var item in entities)
                {
                    if (item.IdAsignado != null)
                    {
                        var dbItem = await dbContext.SucursalesUsuario.FindAsync(item.IdAsignado);
                        if (dbItem != null)
                        {
                            dbItem.Asignado = item.Asignado;
                            dbItem.FechaActualizacion = DateTime.Now.ToLocalTime();
                            dbContext.SucursalesUsuario.Update(dbItem);
                        }
                    }
                    else
                    {
                        var itemAdd = new SucursalesUsuario()
                        {
                            Id = Guid.NewGuid(),
                            IdSucursal = item.Id.Value,
                            IdUsuario = uId,
                            FechaRegistro = DateTime.Now.ToLocalTime(),
                            FechaActualizacion = DateTime.Now.ToLocalTime(),
                            Asignado = item.Asignado
                        };
                        dbContext.SucursalesUsuario.Add(itemAdd);
                    }
                }
                int i = dbContext.SaveChanges();
                if (i == 0)
                {
                    response.Result = false;
                    response.Message = "Error al guardar el registro";
                    response.HasError = false;
                    response.HttpCode = System.Net.HttpStatusCode.NotModified;
                }
                else
                {
                    response.SetSuccess(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DSaveToUser));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<List<EntSucursal>>> DGetByIdUser(Guid uId)
        {
            var response = new Response<List<EntSucursal>>();
            try
            {
                var items = await dbContext.Sucursal.AsNoTracking().Where(x => x.bEliminado == false 
                    && dbContext.SucursalesUsuario.Where(s=>s.IdUsuario==uId && s.Asignado).Select(s=>s.IdSucursal).Contains(x.uId)
                ).ToListAsync();
                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntSucursal>>(items));
                else
                {
                    response.SetError("Sin registros");
                    response.HttpCode = System.Net.HttpStatusCode.NoContent;
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
