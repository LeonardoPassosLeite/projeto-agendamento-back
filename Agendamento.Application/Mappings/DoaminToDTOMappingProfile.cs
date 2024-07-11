using Agendamento.Application.DTOs;
using Agendamento.Domain.Entities;
using AutoMapper;

namespace Agendamento.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Produto, ProdutoFotoDTO>()
             .ForMember(dest => dest.FotoPrincipal, opt => opt.MapFrom(src => src.FotoPrincipal != null ? new FotoDTO
             {
                 Id = src.FotoPrincipal.Id,
                 Url = src.FotoPrincipal.Url,
                 FilePath = src.FotoPrincipal.FilePath,
                 ProdutoId = src.Id
             } : null));
            CreateMap<Produto, ProdutoUpdateDTO>().ReverseMap();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<ClienteEmpresa, ClienteEmpresaDTO>().ReverseMap();
            CreateMap<EmpresaCliente, EmpresaClienteDTO>().ReverseMap();
            CreateMap<Foto, FotoDTO>().ReverseMap();
        }
    }
}
