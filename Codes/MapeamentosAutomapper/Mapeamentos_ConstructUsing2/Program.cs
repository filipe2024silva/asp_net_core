using AutoMapper;
using Mapeamentos_ConstructUsing2.DTOs;
using Mapeamentos_ConstructUsing2.Entities;
using Mapeamentos_ConstructUsing2.Profiles;

Console.WriteLine("\nUsando ConstructUsing\n");

// Configuração do AutoMapper
var config = new MapperConfiguration(cfg => cfg.AddProfile<ProdutoProfile>());
var mapper = config.CreateMapper();

// Lista de produtos de exemplo
var produtos = new List<Produto>
        {
            new Produto { Id = 1, Nome = "Smartphone SamSung", Tipo = "Eletrônico", Preco = 1500.00m },
            new Produto { Id = 2, Nome = "Arroz Integral", Tipo = "Alimento", Preco = 10.50m },
            new Produto { Id = 3, Nome = "Camiseta Branca", Tipo = "Vestuário", Preco = 25.00m }
        };

// Mapeamento para ProdutoDTO
var produtosDTO = mapper.Map<List<ProdutoDTO>>(produtos);

foreach (var dto in produtosDTO)
{
    Console.WriteLine($"Código Único: {dto.CodigoUnico}");
    Console.WriteLine($"Nome: {dto.Nome}");
    Console.WriteLine($"Preço: {dto.Preco:C}");
    Console.WriteLine($"Categorias: {string.Join(", ", dto.Categorias!)}");
    Console.WriteLine(new string('-', 30));
}
Console.ReadKey();