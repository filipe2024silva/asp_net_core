using AutoMapper;
using MapeamentoCondicional1;

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Produto, ProdutoDTO>()
       .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
           src.Preco > 100 && src.Estoque > 0));
});

var produtos = new List<Produto>
{
    new Produto { Id = 1, Nome = "Cadeira", Preco = 150, Estoque =  5 },
    new Produto { Id = 2, Nome = "Caneta", Preco = 10, Estoque = 10 },
    new Produto { Id = 3, Nome = "Mesa", Preco = 200, Estoque = 1 },
    new Produto { Id = 4, Nome = "Sofá", Preco = 950, Estoque = 0 },
    new Produto { Id = 5, Nome = "Mochila", Preco = 99, Estoque = 15 }
};

var mapper = config.CreateMapper();

// Mapeia os produtos que atendem à condição
//var listaProdutosDTO = mapper.Map<List<ProdutoDTO>>(produtos);

// Filtra os produtos antes do mapeamento
var listaProdutosDTO = mapper.Map<List<ProdutoDTO>>(produtos
                             .Where(produto => produto.Preco > 100 && produto.Estoque > 0)
                             .ToList());

foreach (var produtoDTO in listaProdutosDTO)
{
    Console.WriteLine($"Id: {produtoDTO.Id}, Nome: {produtoDTO.Nome}, " +
                      $"Preço: {produtoDTO.Preco} Estoque: {produtoDTO.Estoque}");
}

Console.ReadLine();
