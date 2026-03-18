// 1. Configuração do AutoMapper
using AutoMapper;
using MapearNomesDiferentes;

Console.WriteLine("AutoMapper");
Console.WriteLine("\nMapeamento de propriedades diferentes\n");

var config = new MapperConfiguration(cfg => cfg.CreateMap<Produto, ProdutoViewModel>()
    .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Nome))
    .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Preco)));

// 2. Criação do Mapper
var mapper = config.CreateMapper();

// 3. Criação de uma instância do objeto de origem
var produto = new Produto { Nome = "Produto Teste", Preco = 10.50m };
Console.WriteLine("Produto");
Console.WriteLine($"Nome: {produto.Nome}, Preco: {produto.Preco}");

// 4. Execução do mapeamento
var viewModel = mapper.Map<ProdutoViewModel>(produto);

// 5. Resultado : Descrição: Produto Teste, Valor: 10.50
Console.WriteLine("\nProdutoViewModel");
Console.WriteLine($"Descrição: {viewModel.Descricao}, Valor: {viewModel.Valor}");

Console.ReadKey();