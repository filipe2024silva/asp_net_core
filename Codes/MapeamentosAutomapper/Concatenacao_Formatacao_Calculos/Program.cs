using AutoMapper;
using Concatenacao_Formatacao_Calculos;
using System.Globalization;

var config = new MapperConfiguration(cfg => cfg.CreateMap<Produto, ProdutoViewModel>()
           .ForMember(dest => dest.DescricaoCompleta, opt => opt.MapFrom(src => $"{src.Nome} (Código: {src.CodigoInterno})")) // Concatenação
           .ForMember(dest => dest.ValorFormatado, opt => opt.MapFrom(src => src.Preco.ToString("C", CultureInfo.GetCultureInfo("pt-BR")))) // Formatação de moeda
           .ForMember(dest => dest.DataFormatada, opt => opt.MapFrom(src => src.DataFabricacao.ToString("dd/MM/yyyy"))) // Formatação de data
           .ForMember(dest => dest.PesoEmKg, opt => opt.MapFrom(src => src.Peso / 1000)) // Aplicação de fórmula
           .ForMember(dest => dest.CodigoProduto, opt => opt.MapFrom(src => $"Prod-{src.CodigoInterno}"))
           );

var mapper = config.CreateMapper();

var produto = new Produto
{
    Nome = "Produto Teste",
    Preco = 12.50m,
    DataFabricacao = new DateTime(2024, 10, 26),
    Peso = 5000,
    CodigoInterno = "ABC123XYZ"
};

var viewModel = mapper.Map<ProdutoViewModel>(produto);

Console.WriteLine("ProdutoViewModel:");
Console.WriteLine($"Descrição Completa: {viewModel.DescricaoCompleta}");
Console.WriteLine($"Valor Formatado: {viewModel.ValorFormatado}");
Console.WriteLine($"Data Formatada: {viewModel.DataFormatada}");
Console.WriteLine($"Peso em Kg: {viewModel.PesoEmKg}");
Console.WriteLine($"Código do Produto: {viewModel.CodigoProduto}");

Console.ReadKey();
    