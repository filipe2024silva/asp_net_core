using AutoMapper;
using Mapeamento_ApiAspNetCore.DTOs;
using Mapeamento_ApiAspNetCore.Entities;
using Mapeamento_ApiAspNetCore.Mappings.Converters;

namespace Mapeamento_ApiAspNetCore.Mappings.Profiles;


// Profile AutoMapper
public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<Produto, ProdutoDTO>() // mapeia Produto para ProdutoDTO
         .ForMember(dest => dest.PrecoFormatado, 
                     opt => opt.MapFrom(src => src.Preco.ToString("C2")))
         .ForMember(dest => dest.DataCriacaoFormatada, 
                     opt => opt.MapFrom(src => src.DataCriacao.ToString("dd/MM/yyyy")))
         .ForMember(dest => dest.Status, opt => 
                     opt.MapFrom(src => src.DataCriacao < DateTime.Now.AddDays(-30) ? "Antigo" : "Novo"))
         .ForMember(dest => dest.PrecoComImposto, 
                     opt => opt.MapFrom(src => src.Preco + (src.Preco * src.Imposto / 100)))
         .AfterMap((src, dest) =>
         {
            if (src.Preco > 500)
                dest.Observacao = "Produto de alto valor.";
            else
                dest.Observacao = "Produto com preço acessível.";
         })
        .ForMember(dest => dest.CodigoBarrasFormatado, opt => 
                   opt.ConvertUsing(new CodigoBarrasConverter(), src => src.CodigoBarras));

        CreateMap<CriarProdutoDTO, Produto>();// mapeia CriarProdutoDTO para Produto
    }
}