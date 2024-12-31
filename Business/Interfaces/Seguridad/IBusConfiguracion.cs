using Models.Seguridad;
using Utils;

namespace Business.Interfaces.Seguridad
{
    public interface IBusConfiguracion
    {
        Task<Response<List<ModuloModelo>>> ObtenerElementosSistema();
        Task<Response<bool>> bCrearConfiguracion(ConfiguracionModelo createModel);
        Task<Response<ConfiguracionModelo>> BObtenerConfiguracion();
    }
}
