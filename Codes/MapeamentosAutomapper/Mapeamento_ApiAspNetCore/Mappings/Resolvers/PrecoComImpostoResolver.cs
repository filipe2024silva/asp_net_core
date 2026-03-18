using AutoMapper;
using Mapeamento_ApiAspNetCore.DTOs;
using Mapeamento_ApiAspNetCore.Entities;

namespace Mapeamento_ApiAspNetCore.Mappings.Resolvers;

public class PrecoComImpostoResolver : IValueResolver<Produto, ProdutoDTO, decimal>
{
    public decimal Resolve(Produto source, ProdutoDTO destination, decimal destMember, ResolutionContext context)
    {
        return source.Preco + (source.Preco * source.Imposto / 100);
    }
}
