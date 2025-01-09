using AutoMapper;
using Business.Interfaces.Seguridad;
using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Business.Implementation.Seguridad
{
    public class BusConfiguracion:IBusConfiguracion
    {
        private readonly IConfiguracionesRepositorio configuracionRepositorio;
        private readonly IMapper mapeador;
        public BusConfiguracion(IMapper mapeador, IConfiguracionesRepositorio configuracionRepositorio)
        {
            this.mapeador = mapeador;
            this.configuracionRepositorio = configuracionRepositorio;
        }
        public async Task<Response<List<ModuloModelo>>> ObtenerElementosSistema()
        {
            Response<List<ModuloModelo>> response = new();
            try
            {
                Response<List<Modulo>> obtenerElementos = await configuracionRepositorio.ObtenerElementosSistema();
                if (obtenerElementos.HasError)
                {
                    return response.GetResponse(obtenerElementos);
                }

                List<ModuloModelo> lstElementos = obtenerElementos.Result
                    .GroupBy(x => x.uIdModulo)
                    .Select(x => new ModuloModelo
                    {
                        uIdModulo = x.Key,
                        sClaveModulo = x.FirstOrDefault().sClaveModulo,
                        sNombreModulo = x.FirstOrDefault().sNombreModulo,
                        sPathModulo = x.FirstOrDefault().sPathModulo,
                        bMostrarEnMenu = x.FirstOrDefault().bMostrarEnMenu,
                        Paginas = x.FirstOrDefault()?.lstPaginas
                            .GroupBy(y => y.uIdPagina)
                            .Select(y => new PaginaModelo
                            {
                                uIdPagina = y.Key,
                                sClavePagina = y.FirstOrDefault().sClavePagina,
                                sNombrePagina = y.FirstOrDefault().sNombrePagina,
                                sPathPagina = y.FirstOrDefault().sPathPagina,
                                bMostrarEnMenu = y.FirstOrDefault().bMostrarEnMenu,
                                Botones = y.FirstOrDefault()?.lstBotones
                                    .GroupBy(z => z.uIdBoton)
                                    .Select(z => new BotonModelo
                                    {
                                        uIdBoton = z.Key,
                                        sClaveBoton = z.FirstOrDefault().sClaveBoton,
                                        sNombreBoton = z.FirstOrDefault().sNombreBoton,
                                    })
                                    .Where(x => x.uIdBoton != Guid.Empty)
                                    .ToList()
                            })
                            .Where(x => x.uIdPagina != Guid.Empty)
                            .ToList()
                    })
                    .Where(x => x.uIdModulo != Guid.Empty)
                    .ToList();

                response.SetSuccess(lstElementos, "Se ha consultado la configuración");
            }
            catch (Exception ex)
            {
                response.SetError("Ocurrió un error en la base de datos al consultar la configuración");
            }
            return response;
        }
        public async Task<Response<bool>> bCrearConfiguracion(ConfiguracionModelo createModel)
        {
            Response<bool> response = new();
            try
            {
                Configuracion entConfiguracion = mapeador.Map<Configuracion>(createModel);

                Response<Configuracion> EntConfiguracionGuardada = await configuracionRepositorio.DObtenerConfiguracion();
                if (EntConfiguracionGuardada.Result == null)
                {
                    await configuracionRepositorio.DSave(entConfiguracion);
                }
                else
                {
                    entConfiguracion.uIdConfiguracion = EntConfiguracionGuardada.Result.uIdConfiguracion;
                    await configuracionRepositorio.DUpdate(entConfiguracion);
                }
                response.SetSuccess(true);
            }
            catch (Exception ex)
            {
                response.SetError("No se guardó el perfil");
            }
            return response;
        }
        public async Task<Response<ConfiguracionModelo>> BObtenerConfiguracion()
        {
            Response<ConfiguracionModelo> response = new Response<ConfiguracionModelo>();
            try
            {
                Response<Configuracion> entConfiguracion = await configuracionRepositorio.DObtenerConfiguracion();
                ConfiguracionModelo entConfiguracionMapeado = mapeador.Map<ConfiguracionModelo>(entConfiguracion.Result);
                response.SetSuccess(entConfiguracionMapeado);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
    }
}
