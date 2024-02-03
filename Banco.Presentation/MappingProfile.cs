using AutoMapper;
using Banco.Application.DTO;
using Banco.Infraestructure.Modelos;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<CrearUsuarioDTO, Usuario>();
        CreateMap<Usuario, CrearUsuarioDTO>();

        CreateMap<Cliente, CrearClienteDTO>();
        CreateMap<CrearClienteDTO, Cliente>();

        CreateMap<CrearPrestamoDTO, Prestamo>();
        CreateMap<CrearTransaccionDTO, Transaccione>();

        CreateMap<CrearCuentaBancariaDTO, CuentasBancaria>();

    }
}