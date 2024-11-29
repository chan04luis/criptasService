using AutoMapper;
using Business.Data;
using Data.cs;
using Data.cs.Entities;
using Entities;
using Entities.Request.Usuarios;
using Microsoft.EntityFrameworkCore;

public class UsuariosRepositorio : IUsuariosRepositorio
{
    private readonly ApplicationDbContext dbContext;
    private readonly IMapper _mapper;

    public UsuariosRepositorio(ApplicationDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Response<EntUsuarios>> DSave(EntUsuarios entity)
    {
        var response = new Response<EntUsuarios>();
        try
        {
            var newItem = _mapper.Map<Usuarios>(entity);
            newItem.bEliminado = false;
            dbContext.Usuarios.Add(newItem);
            int i = await dbContext.SaveChangesAsync();
            if (i == 0)
            {
                response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                response.SetError("Error al guardar el registro");
            }
            else
            {
                response.SetSuccess(_mapper.Map<EntUsuarios>(newItem));
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }
        return response;
    }

    public async Task<Response<EntUsuarios>> DUpdate(EntUsuarios entity)
    {
        Response<EntUsuarios> response = new Response<EntUsuarios>();

        try
        {
            var bEntity = dbContext.Usuarios.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
            bEntity.sNombres = entity.sNombres;
            bEntity.sApellidos = entity.sApellidos;
            bEntity.sTelefono = entity.sTelefono;
            bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
            dbContext.Update(bEntity);
            var exec = await dbContext.SaveChangesAsync();

            if (exec > 0)
                response.SetSuccess(_mapper.Map<EntUsuarios>(bEntity), "Actualizado correctamente");
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

    public async Task<Response<EntUsuarios>> DUpdateBoolean(EntUsuarios entity)
    {
        Response<EntUsuarios> response = new Response<EntUsuarios>();

        try
        {
            var bEntity = dbContext.Usuarios.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
            bEntity.bActivo = entity.bActivo;
            bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
            dbContext.Update(bEntity);
            var exec = await dbContext.SaveChangesAsync();

            if (exec > 0)
                response.SetSuccess(_mapper.Map<EntUsuarios>(bEntity), "Actualizado correctamente");
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
            var bEntity = dbContext.Usuarios.AsNoTracking().FirstOrDefault(x => x.uId == uId);
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

    public async Task<Response<EntUsuarios>> DGetById(Guid iKey)
    {
        var response = new Response<EntUsuarios>();
        try
        {
            var entity = await dbContext.Usuarios.AsNoTracking().Where(x => x.bEliminado == false).AsNoTracking()
            .SingleOrDefaultAsync(x => x.uId == iKey);
            if (entity != null)
                response.SetSuccess(_mapper.Map<EntUsuarios>(entity));
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

    public async Task<Response<EntUsuarios>> DGetByEmail(string sCorreo)
    {
        var response = new Response<EntUsuarios>();
        try
        {
            var entity = await dbContext.Usuarios.AsNoTracking().Where(x => x.bEliminado == false).AsNoTracking()
            .SingleOrDefaultAsync(x => x.sContra == sCorreo);
            if (entity != null)
                response.SetSuccess(_mapper.Map<EntUsuarios>(entity));
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

    public async Task<Response<List<EntUsuarios>>> DGetList()
    {
        var response = new Response<List<EntUsuarios>>();
        try
        {
            var items = await dbContext.Usuarios.AsNoTracking().Where(x => x.bEliminado == false).ToListAsync();
            if (items.Count > 0)
                response.SetSuccess(_mapper.Map<List<EntUsuarios>>(items));
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

    public async Task<Response<List<EntUsuarios>>> DGetByFilters(EntUsuarioSearchRequest search)
    {
        var response = new Response<List<EntUsuarios>>();
        try
        {
            var items = await dbContext.Usuarios.AsNoTracking().Where(x => x.bEliminado == false).ToListAsync();
            if (items.Count > 0)
                response.SetSuccess(_mapper.Map<List<EntUsuarios>>(items));
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

    public async Task<Response<EntUsuarios>> DLogin(EntUsuarioLoginRequest loginRequest)
    {
        var response = new Response<EntUsuarios>();
        try
        {
            var user = await dbContext.Usuarios.AsNoTracking()
                .FirstOrDefaultAsync(u => u.sCorreo == loginRequest.sCorreo && u.sContra == loginRequest.sContra && u.bActivo == true && u.bEliminado == false);

            if (user == null)
            {
                response.SetError("No existe usuario");
                response.HttpCode = System.Net.HttpStatusCode.Unauthorized;
            }else if (user.sContra != loginRequest.sContra)
            {
                response.SetError("Contraseña incorrecta");
                response.HttpCode = System.Net.HttpStatusCode.Unauthorized;
            }
            else if (!user.bActivo)
            {
                response.SetError("Usuario inactivo");
                response.HttpCode = System.Net.HttpStatusCode.Unauthorized;
            }
            else
            {
                response.SetSuccess(_mapper.Map<EntUsuarios>(user), "Login exitoso");
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }
        return response;
    }
}
