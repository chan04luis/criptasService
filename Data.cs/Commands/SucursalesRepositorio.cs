
using AutoMapper;
using Business.Data;
using Data.cs.Entities.Catalogos;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Request.Catalogo.Sucursales;
using Utils;

namespace Data.cs.Commands
{
    public class SucursalesRepositorio : ISucursalesRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;

        public SucursalesRepositorio(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
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
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
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
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
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
