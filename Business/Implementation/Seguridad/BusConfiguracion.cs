using AutoMapper;
using Business.Interfaces.Seguridad;
using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Entities;
using Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Response<List<ElementoSrcModelo>> obtenerElementos = await configuracionRepositorio.ObtenerElementosSistema();
                if (obtenerElementos.HasError)
                {
                    return response.GetResponse(obtenerElementos);
                }

                List<ModuloModelo> lstElementos = obtenerElementos.Result
                    .GroupBy(x => x.IdModulo)
                    .Select(x => new ModuloModelo
                    {
                        uIdModulo = x.Key,
                        sClaveModulo = x.FirstOrDefault().ClaveModulo,
                        sNombreModulo = x.FirstOrDefault().NombreModulo,
                        sPathModulo = x.FirstOrDefault().PathModulo,
                        bMostrarEnMenu = x.FirstOrDefault().MostrarModuloEnMenu,
                        Paginas = x
                            .GroupBy(y => y.IdPagina)
                            .Select(y => new PaginaModelo
                            {
                                uIdPagina = y.Key,
                                sClavePagina = y.FirstOrDefault().ClavePagina,
                                sNombrePagina = y.FirstOrDefault().NombrePagina,
                                sPathPagina = y.FirstOrDefault().PathPagina,
                                bMostrarEnMenu = y.FirstOrDefault().MostrarPaginaEnMenu,
                                Botones = y
                                    .GroupBy(z => z.IdBoton)
                                    .Select(z => new BotonModelo
                                    {
                                        uIdBoton = z.Key,
                                        sClaveBoton = z.FirstOrDefault().ClaveBoton,
                                        sNombreBoton = z.FirstOrDefault().NombreBoton,
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
