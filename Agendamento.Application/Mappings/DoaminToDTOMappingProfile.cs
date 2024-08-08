using Agendamento.Application.DTOs;
using Agendamento.Domain.Enitiies;
using Agendamento.Domain.Entities;
using AutoMapper;

namespace Agendamento.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            // Mapeamentos de Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();

            // Mapeamentos de Produto
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Produto, ProdutoFotoDTO>()
                .ForMember(dest => dest.FotoPrincipal, opt => opt.MapFrom(src => src.FotoPrincipal));

            // Mapeamentos de Cliente
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Cliente, ClienteFotoDTO>()
                .ForMember(dest => dest.FotoPrincipal, opt => opt.MapFrom(src => src.FotoPrincipal));

            // Mapeamentos de Funcionario
            CreateMap<Funcionario, FuncionarioDTO>().ReverseMap();

            // Mapeamentos de FotoProduto e FotoCliente
            CreateMap<FotoProduto, FotoProdutoDTO>().ReverseMap();
            CreateMap<FotoCliente, FotoClienteDTO>().ReverseMap();
        }
    }
}
