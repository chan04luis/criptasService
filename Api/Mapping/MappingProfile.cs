using AutoMapper;
using Data.cs.Entities.Catalogos;
using Data.cs.Entities.Seguridad;
using Models.Models;
using Models.Request;
using Models.Request.Beneficiarios;
using Models.Request.Clientes;
using Models.Request.Criptas;
using Models.Request.Fallecidos;
using Models.Request.Iglesias;
using Models.Request.Pagos;
using Models.Request.Secciones;
using Models.Request.TipoPagos;
using Models.Request.Usuarios;
using Models.Request.Zonas;
using Models.Responses.Iglesia;
using Models.Responses.Zonas;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Clientes
        CreateMap<EntClientes, Clientes>();
        CreateMap<Clientes, EntClientes>()
            .ForMember(dest => dest.sFechaNacimiento, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.sFechaNacimiento) ? "1900-01-01" : src.sFechaNacimiento))
            .ForMember(dest => dest.iEdad, opt => opt.MapFrom(src => CalcularEdad(src.sFechaNacimiento)))
            .ForMember(dest => dest.sContra, opt => opt.MapFrom(src => string.Empty));
        CreateMap<EntClienteUpdateRequest, EntClientes>();
        CreateMap<EntClienteUpdateEstatusRequest, EntClientes>();
        #endregion

        #region Iglesias
        CreateMap<EntIglesias, Iglesias>();
        CreateMap<Iglesias, EntIglesias>();
        CreateMap<Iglesias, EntIglesiaResponse>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.listZonas));
        CreateMap<EntIglesiaUpdateRequest, EntIglesias>();
        CreateMap<EntIglesiaUpdateEstatusRequest, EntIglesias>();
        #endregion

        #region Zonas
        CreateMap<EntZonas, Zonas>();
        CreateMap<Zonas, EntZonas>();
        CreateMap<Zonas, EntZonasResponse>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.listSecciones));
        CreateMap<EntZonaUpdateRequest, EntZonas>();
        CreateMap<EntZonaUpdateEstatusRequest, EntZonas>();
        #endregion

        #region Secciones
        CreateMap<EntSecciones, Secciones>();
        CreateMap<Secciones, EntSecciones>();
        CreateMap<EntSeccionesUpdateRequest, EntSecciones>();
        CreateMap<EntSeccionesUpdateEstatusRequest, EntSecciones>();
        #endregion

        #region Criptas
        CreateMap<EntCriptas, Criptas>();
        CreateMap<Criptas, EntCriptas>();
        CreateMap<EntCriptaUpdateRequest, EntCriptas>();
        CreateMap<EntCriptaUpdateEstatusRequest, EntCriptas>();
        #endregion

        #region Tipos de pagos
        CreateMap<EntTiposPago, TiposDePago>();
        CreateMap<TiposDePago, EntTiposPago>();
        CreateMap<EntTiposPagoRequest, EntTiposPago>();
        CreateMap<EntTiposPagoSearchRequest, EntTiposPago>();
        CreateMap<EntTiposPago, EntTiposPagoRequest>();
        #endregion

        #region Pagos
        CreateMap<EntPagos, Pagos>();
        CreateMap<Pagos, EntPagos>();
        CreateMap<EntPagosRequest, EntPagos>();
        CreateMap<EntPagosSearchRequest, EntPagos>();
        CreateMap<EntPagosUpdateEstatusRequest, EntPagos>();
        #endregion

        #region PagosParciales
        CreateMap<EntPagosParciales, PagosParciales>().ReverseMap();
        #endregion

        #region Criptas
        CreateMap<EntCriptas, Criptas>();
        CreateMap<Criptas, EntCriptas>();
        CreateMap<EntCriptaUpdateRequest, EntCriptas>();
        CreateMap<EntCriptaUpdateEstatusRequest, EntCriptas>();
        #endregion

        #region Visitas
        CreateMap<EntVisitas, Visitas>();
        CreateMap<Visitas, EntVisitas>();
        #endregion

        #region Fallecidos
        CreateMap<EntFallecidos, Fallecidos>();
        CreateMap<Fallecidos, EntFallecidos>().ForMember(dest => dest.iEdad, opt => opt.MapFrom(src => CalcularEdad(src.dtFechaNacimiento, src.dtFechaFallecimiento)));
        CreateMap<EntFallecidos, EntFallecidosRequest>().ReverseMap();
        CreateMap<EntFallecidosUpdateRequest, EntFallecidos>().ReverseMap();
        CreateMap<EntFallecidosUpdateEstatusRequest, EntFallecidos>().ReverseMap();
        #endregion

        #region Beneficiarios
        CreateMap<EntBeneficiarios, Beneficiarios>().ReverseMap();
        CreateMap<EntBeneficiarios, EntBeneficiariosRequest>().ReverseMap();
        CreateMap<EntBeneficiariosUpdateRequest, EntBeneficiarios>().ReverseMap();
        CreateMap<EntBeneficiariosUpdateEstatusRequest, EntBeneficiarios>().ReverseMap();
        #endregion

        #region Usuarios

        CreateMap<EntUsuarios, Usuarios>().ReverseMap();
        CreateMap<EntUsuarios, EntUsuarioRequest>().ReverseMap();
        CreateMap<EntUsuarioUpdateRequest, EntUsuarios>().ReverseMap();
        CreateMap<EntUsuarioUpdateEstatusRequest, EntUsuarios>().ReverseMap();

        #endregion

        #region Tipo de mantenimiento

        CreateMap<EntTipoDeMantenimiento, TipoDeMantenimiento>().ReverseMap();

        #endregion

        #region Servicios

        CreateMap<EntServicios, Servicios>().ReverseMap();

        #endregion

        #region Solicitud Información

        CreateMap<EntSolicitudesInfo, SolicitudesInfo>().ReverseMap();

        #endregion

    }

    private int? CalcularEdad(string fechaNacimiento)
    {
        if (DateTime.TryParse(fechaNacimiento, out DateTime fechaNac))
        {
            var edad = DateTime.Today.Year - fechaNac.Year;
            if (fechaNac.Date > DateTime.Today.AddYears(-edad)) edad--;
            return edad;
        }
        return null;
    }
    private int? CalcularEdad(string? fechaNacimiento, string? fechaFallecido)
    {
        if (DateTime.TryParse(fechaNacimiento, out DateTime fechaNac))
        {
            if (DateTime.TryParse(fechaFallecido, out DateTime fechaF))
            {
                var edad = fechaF.Year - fechaNac.Year;
                if (fechaNac.Date > DateTime.Today.AddYears(-edad)) edad--;
                return edad;
            }
        }
        return null;
    }
    private int? CalcularEdad(DateTime fechaNac)
    {
        var edad = DateTime.Today.Year - fechaNac.Year;
        if (fechaNac.Date > DateTime.Today.AddYears(-edad)) edad--;
        return edad;
    }
}