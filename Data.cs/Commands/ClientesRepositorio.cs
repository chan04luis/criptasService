
using AutoMapper;
using Data.cs;
using Data.cs.Entities;
using Entities;
using Entities.JsonRequest.Clientes;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

public class ClientesRepositorio : IClientesRepositorio
{
    private readonly ApplicationDbContext dbContext;
    private readonly IMapper _mapper;

    public ClientesRepositorio(ApplicationDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Response<EntClientes>> DSave(EntClientes entity)
    {
        var response = new Response<EntClientes>();
        try
        {
            var newItem = _mapper.Map<Clientes>(entity);
            newItem.bEliminado = false;
            dbContext.Clientes.Add(newItem);
            int i = await dbContext.SaveChangesAsync();
            if (i == 0)
            {
                response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                response.SetError("Error al guardar el registro");
            }
            else
            {
                response.SetSuccess(_mapper.Map<EntClientes>(newItem));
            }

        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }
        return response;
    }

    public async Task<Response<EntClientes>> DUpdate(EntClientes entity)
    {
        Response<EntClientes> response = new Response<EntClientes>();

        try
        {
            var bEntity = dbContext.Clientes.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
            bEntity.sNombre=entity.sNombre;
            bEntity.sApellidos=entity.sApellidos;
            bEntity.sTelefono = entity.sTelefono;
            bEntity.sDireccion = entity.sDireccion;
            bEntity.sEmail = entity.sEmail;
            bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
            dbContext.Update(bEntity);
            var exec = await dbContext.SaveChangesAsync();

            if (exec > 0)
                response.SetSuccess(_mapper.Map<EntClientes>(bEntity), "Actualizado correctamente");
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

    public async Task<Response<EntClientes>> DUpdateBoolean(EntClientes entity)
    {
        Response<EntClientes> response = new Response<EntClientes>();

        try
        {
            var bEntity = dbContext.Clientes.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
            bEntity.bEstatus = entity.bEstatus;
            bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
            dbContext.Update(bEntity);
            var exec = await dbContext.SaveChangesAsync();

            if (exec > 0)
                response.SetSuccess(_mapper.Map<EntClientes>(bEntity), "Actualizado correctamente");
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
            var bEntity = dbContext.Clientes.AsNoTracking().FirstOrDefault(x => x.uId == uId);
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

    public async Task<Response<EntClientes>> DGetById(Guid iKey)
    {
        var response = new Response<EntClientes>();
        try
        {
            var entity = await dbContext.Clientes.AsNoTracking().Where(x => x.bEliminado == false).AsNoTracking()
            .SingleOrDefaultAsync(x => x.uId == iKey);
            if (entity != null)
                response.SetSuccess(_mapper.Map<EntClientes>(entity));
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

    public async Task<Response<List<EntClientes>>> DGetByFilters(EntClienteSearchRequest filtros)
    {
        var response = new Response<List<EntClientes>>();
        try
        {
            var items = await dbContext.Clientes.AsNoTracking().Where(c => c.bEliminado == false).ToListAsync();
            if(items.Count >0 && filtros.sNombre != null) 
            {
                items = items.Where(x => x.sNombre.ToLower().Contains(filtros.sNombre.ToLower())).ToList();
            }
            if (items.Count > 0 && filtros.sApellido != null)
            {
                items = items.Where(x => x.sApellidos.ToLower().Contains(filtros.sApellido.ToLower())).ToList();
            }
            if (items.Count >0 && filtros.bEstatus != null) 
            {
                items = items.Where(x => x.bEstatus==filtros.bEstatus).ToList();
            }
            if (items.Count > 0)
                response.SetSuccess(_mapper.Map<List<EntClientes>>(items));
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

    public async Task<Response<List<EntClientes>>> DGetByEmail(string sEmail)
    {
        var response = new Response<List<EntClientes>>();
        try
        {
            var items = await dbContext.Clientes.AsNoTracking().Where(c => c.sEmail==sEmail && c.bEliminado==false).ToListAsync();
            if (items.Count > 0)
                response.SetSuccess(_mapper.Map<List<EntClientes>>(items));
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

    public async Task<Response<List<EntClientes>>> DGetList()
    {
        var response = new Response<List<EntClientes>>();
        try
        {
            var items = await dbContext.Clientes.AsNoTracking().Where(x=>x.bEliminado==false).ToListAsync();
            if (items.Count > 0)
                response.SetSuccess(_mapper.Map<List<EntClientes>>(items));
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