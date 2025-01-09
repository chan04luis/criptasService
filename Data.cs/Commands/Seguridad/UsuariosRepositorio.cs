using AutoMapper;
using Business.Data;
using Data.cs;
using Data.cs.Entities.Seguridad;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Request.Usuarios;
using Utils;
using Utils.Interfaces;

public class UsuariosRepositorio : IUsuariosRepositorio
{
    private readonly ApplicationDbContext dbContext;
    private readonly IMapper _mapper;

    public UsuariosRepositorio(ApplicationDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Response<bool>> AnyExistKey(Guid pKey)
    {
        Response<bool> response = new Response<bool>();

        try
        {
            var exitsKey = await dbContext.Usuarios.AnyAsync(i => i.uId == pKey && i.bActivo == true);
            if (exitsKey)
            {
                response.SetSuccess(exitsKey, "Usuario ya existente");
            }
            else
            {
                response.SetError("No existe el usuario");
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }
        return response;
    }
    public async Task<Response<bool>> AnyExitMailAndKey(EntUsuarios pEntity)
    {
        Response<bool> response = new Response<bool>();

        try
        {
            var existMail = await dbContext.Usuarios.AnyAsync(i => (i.uId != pEntity.uId)
                                                                    && (i.sCorreo.Equals(pEntity.sCorreo)) && i.bActivo == true);

            if (existMail)
            {
                response.SetSuccess(existMail, "Correo ya existente");
            }
            else
            {
                response.SetError("No existe el correo");
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }
        return response;
    }
    public async Task<Response<bool>> DAnyExistEmail(string pCorreo)
    {
        Response<bool> response = new Response<bool>();

        try
        {
            var existMail = await dbContext.Usuarios.AnyAsync(i => i.sCorreo.Equals(pCorreo) && i.bActivo == true);

            if (existMail)
            {
                response.SetSuccess(existMail, "Correo ya existente");
            }
            else
            {
                response.SetError("No existe el correo");
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }
        return response;
    }

    public async Task<Response<EntUsuarios>> DSave(EntUsuarios entity)
    {
        var response = new Response<EntUsuarios>();
        try
        {
            var newItem = _mapper.Map<Usuarios>(entity);
            newItem.uId=Guid.NewGuid();
            newItem.bEliminado = false;
            newItem.bActivo = true;
            newItem.dtFechaRegistro = DateTime.Now.ToLocalTime();
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
            var bEntity = await dbContext.Usuarios.FindAsync(entity.uId);
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

    public async Task<Response<EntUsuarios>> DUpdateEstatus(EntUsuarios entity)
    {
        Response<EntUsuarios> response = new Response<EntUsuarios>();

        try
        {
            var bEntity =await dbContext.Usuarios.FindAsync(entity.uId);
            bEntity.bActivo = entity.bActivo;
            bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
            dbContext.Attach(bEntity);
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

    public async Task<Response<bool>> DDelete(Guid uId)
    {
        Response<bool> response = new Response<bool>();

        try
        {
            var bEntity =await dbContext.Usuarios.FindAsync(uId);
            bEntity.bEliminado = true;
            bEntity.dtFechaEliminado = DateTime.Now.ToLocalTime();
            dbContext.Attach(bEntity);
            var exec = await dbContext.SaveChangesAsync();

            if (exec > 0)
                response.SetSuccess(true, "Eliminado correctamente");
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
            var entity = await dbContext.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.uId == iKey && x.bActivo==true);
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
            var items = await dbContext.Usuarios.AsNoTracking().Where(x => x.bEliminado == false).OrderBy(x=>x.sNombres).ToListAsync();
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
    public async Task<Response<EntUsuarios>> DGet(string correo, string sPassword)
    {
        var response = new Response<EntUsuarios>();
        try
        {
            var usuario = await dbContext.Usuarios
                        .SingleOrDefaultAsync(u => u.sCorreo == correo && u.sContra == sPassword && u.bActivo == true);

            response.SetSuccess(_mapper.Map<EntUsuarios>(usuario), "Usuario existente");
        }
        catch (Exception ex)
        {

            throw;
        }
        return response;
    }
    public async Task<Response<EntUsuarios>> DGetByIdAndPerfilAsync(Guid usuarioId, Guid perfilId)
    {
        var response = new Response<EntUsuarios>();

        try
        {
            var usuario = await dbContext.Usuarios
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.uId == usuarioId && u.uIdPerfil == perfilId);

            if (usuario == null)
            {
                response.SetError("El usuario no fue encontrado.");
                return response;
            }

            response.SetSuccess(_mapper.Map<EntUsuarios>(usuario));
        }
        catch (Exception ex)
        {
            // Registra el error
            // Logger.LogError(ex, "Error al obtener el usuario por Id y perfil.");

            response.SetError(ex);
        }

        return response;
    }

}
