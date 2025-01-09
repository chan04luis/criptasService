using Models.Seguridad;
using Utils;

namespace Business.Interfaces.Seguridad
{
    public interface IBusAutenticacion
    {
        Task<Response<LoginResponseModelo>> Login(LoginModelo loginModel);
        Task<Response<LoginResponseModelo>> Refresh();

    }
}
