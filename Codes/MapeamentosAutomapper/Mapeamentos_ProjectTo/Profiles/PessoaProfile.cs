using AutoMapper;
using Mapeamentos_ProjectTo.DTOs;
using Mapeamentos_ProjectTo.Entities;

namespace Mapeamentos_ProjectTo.Profiles;

public class PessoaProfile : Profile
{
    public PessoaProfile()
    {
        // Configuração do mapeamento de Pessoa para PessoaDTO
        CreateMap<Pessoa, PessoaDTO>()
            .ForMember(dest => dest.EnderecoCompleto, opt => opt.MapFrom(src =>
                $"{src.Endereco!.Rua}, {src.Endereco.Cidade}, {src.Endereco.Estado}"));
    }
}
