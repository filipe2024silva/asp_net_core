using AutoMapper;
using Mapeamento_AfterMap;

Console.WriteLine("AutoMapper");
Console.WriteLine("\nMapeamento usando AfterMap\n");

var config = new MapperConfiguration(cfg =>
{
    // Definindo o mapeamento entre Produto e ProdutoDTO
    cfg.CreateMap<Produto, ProdutoDTO>()
        .AfterMap((src, dest) =>
        {
            // Adiciona "(Promoção)" ao nome se estiver em promoção
            if (src.EmPromocao)
            {
                dest.Nome += " (Promoção)";
            }

            // Define o preço como zero se o produto estiver esgotado
            if (src.Esgotado)
            {
                dest.Preco = 0;
                dest.Nome += " (Esgotado)";
            }
        });
});

// Criando o mapper a partir da configuração
var mapper = config.CreateMapper();

// Lista de produtos para testar o mapeamento
var produtos = new List<Produto>
{
 new Produto { Id = 1, Nome = "Smartphone", Preco = 2000, EmPromocao = true, Esgotado = false },
 new Produto { Id = 2, Nome = "Notebook", Preco = 3500, EmPromocao = false, Esgotado = true },
 new Produto { Id = 3, Nome = "Tablet", Preco = 1500, EmPromocao = true, Esgotado = true },
 new Produto { Id = 4, Nome = "Mouse", Preco = 12, EmPromocao = true, Esgotado = false },
};

var produtosDTO = mapper.Map<List<ProdutoDTO>>(produtos);

// Exibindo os resultados
Console.WriteLine("Lista de Produtos DTO\n");
foreach (var p in produtosDTO)
{
    Console.WriteLine($"Nome: {p.Nome}, \t Preço: {p.Preco:C}");
}

Console.ReadKey();
