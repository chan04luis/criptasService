using AutoMapper;
using Data.cs.Entities.Catalogos;
using Data.cs.Entities.Seguridad;
using Models.Models;
using Models.Request.Catalogo.Clientes;
using Models.Request.Catalogo.Sucursales;
using Models.Request.Usuarios;


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

        #region Sucursales
        CreateMap<EntSucursal, Sucursales>();
        CreateMap<Sucursales, EntSucursal>();
        CreateMap<EntSucursalUpdateRequest, EntSucursal>();
        CreateMap<EntSucursalUpdateEstatusRequest, EntSucursal>();
        #endregion

        #region Usuarios

        CreateMap<EntUsuarios, Usuarios>().ReverseMap();
        CreateMap<EntUsuarios, EntUsuarioRequest>().ReverseMap();
        CreateMap<EntUsuarioUpdateRequest, EntUsuarios>().ReverseMap();
        CreateMap<EntUsuarioUpdateEstatusRequest, EntUsuarios>().ReverseMap();

        #endregion

        #region Servicios

        CreateMap<EntServicios, Servicios>().ReverseMap();

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
}