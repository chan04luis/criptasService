using AutoMapper;
using Data.cs.Entities;
using Entities.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Ejemplo de mapeo de entidad a DTO
        CreateMap<EntClientes, Clientes>();
        CreateMap<Clientes, EntClientes>();
    }
}