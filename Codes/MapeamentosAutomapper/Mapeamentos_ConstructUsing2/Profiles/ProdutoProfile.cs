using AutoMapper;
using Mapeamentos_ConstructUsing2.DTOs;
using Mapeamentos_ConstructUsing2.Entities;

namespace Mapeamentos_ConstructUsing2.Profiles;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<Produto, ProdutoDTO>()
            .ConstructUsing(src => new ProdutoDTO
            {
                CodigoUnico = GerarCodigoUnico(src.Id, src.Nome!),
                Nome = src.Nome,
                Preco = src.Preco,
                Categorias = ObterCategoriasPorTipo(src.Tipo!)
            });
    }

    private static string GerarCodigoUnico(int id, string nome)
    {
        //combina ID e nome para gerar um código único
        return $"{id}-{nome.Replace(" ", "").ToUpper()}";
    }

    private static List<string> ObterCategoriasPorTipo(string tipo)
    {
        // define categorias com base no tipo
        return tipo switch
        {
            "Eletrônico" => new List<string> { "Tecnologia", "Eletrodomésticos" },
            "Alimento" => new List<string> { "Comida", "Perecíveis" },
            "Vestuário" => new List<string> { "Moda", "Roupas" },
            _ => new List<string> { "Outros" },
        };
    }
}
