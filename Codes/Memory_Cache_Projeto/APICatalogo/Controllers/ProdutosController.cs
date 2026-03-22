using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    //Aqui eu estou usando apenas o repositório específico
    //Pois como ele implementa IRepository ele contém todos
    //os métodos do repositório genérico e também o método específico
    //sendo suficiente para realizar todas as operações 
    private readonly IProdutoRepository _produtoRepository;
    private readonly ILogger<ProdutosController> _logger;

    private readonly IMemoryCache _cache;

    private const string CacheProdutosKey = "CacheProdutos";
    private const string CacheProdutosCategoriaPrefix = "CacheProdutosCategoria_";

    public ProdutosController(IProdutoRepository produtoRepository,
                              ILogger<ProdutosController> logger,
                              IMemoryCache cache)
    {
        _produtoRepository = produtoRepository;
        _logger = logger;
        _cache = cache;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        if (!_cache.TryGetValue(CacheProdutosKey, out IEnumerable<Produto>? produtos))
        {
            produtos = _produtoRepository.GetAll();

            if (produtos is null || !produtos.Any())
            {
                _logger.LogWarning("Não existem produtos...");
                return NotFound("Não existem produtos...");
            }

            SetCache(CacheProdutosKey, produtos);
        }
        return Ok(produtos);
    }

    [HttpGet("categoria/{id}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
    {
        var cacheKey = GetProdutosCategoriaCacheKey(id);

        if (!_cache.TryGetValue(cacheKey, out IEnumerable<Produto>? produtos))
        {
            produtos = _produtoRepository.GetProdutosPorCategoria(id);

            if (produtos is null || !produtos.Any())
            {
                _logger.LogWarning($"Nenhum produto encontrado para categoria com id = {id}");
                return NotFound($"Nenhum produto encontrado para categoria com id = {id}");
            }

            SetCache(cacheKey, produtos);
        }

        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var cacheKey = GetProdutoCacheKey(id);

        if (!_cache.TryGetValue(cacheKey, out Produto? produto))
        {
            produto = _produtoRepository.Get(p => p.ProdutoId == id);

            if (produto is null)
            {
                _logger.LogWarning($"Produto com id= {id} não encontrado...");
                return NotFound($"Produto com id= {id} não encontrado...");
            }

            SetCache(cacheKey, produto);
        }
        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
        {
            _logger.LogWarning("Dados inválidos");
            return BadRequest("Dados inválidos");
        }

        var novoProduto = _produtoRepository.Create(produto);

        InvalidateCacheAfterChange(novoProduto.ProdutoId, novoProduto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = novoProduto.ProdutoId }, novoProduto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id <= 0 || produto is null || id != produto?.ProdutoId)
        {
            _logger.LogWarning("Dados inconsistentes");
            return BadRequest("Dados inconsistentes");
        }

        var produtoAtualizado = _produtoRepository.Update(produto);

        InvalidateCacheAfterChange(id, produtoAtualizado);

        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _produtoRepository.Get(p => p.ProdutoId == id);

        if (produto is null)
        {
            _logger.LogWarning($"Produto não encontrado para o id = {id}");
            return NotFound($"Produto não encontrado para o id = {id}");
        }

        var produtoDeletado = _produtoRepository.Delete(produto);

        InvalidateCacheAfterChange(id);

        return Ok(produtoDeletado);
    }
   
#region Métodos Auxiliares de Cache
    private string GetProdutoCacheKey(int id) => $"CacheProduto_{id}";
    private string GetProdutosCategoriaCacheKey(int categoriaId) => $"{CacheProdutosCategoriaPrefix}{categoriaId}";

    //centraliza a configuração de expiração/prioridade do cache.
    private void SetCache<T>(string key, T data)
    {
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
            SlidingExpiration = TimeSpan.FromSeconds(15),
            Priority = CacheItemPriority.High
        };
        _cache.Set(key, data, cacheOptions);
    }

    //remove as chaves relevantes após alterações (inserir, atualizar, deletar),
    //evitando dados desatualizados.
    private void InvalidateCacheAfterChange(int id, Produto? produto = null)
    {
        // remove cache da lista de produtos e do produto específico
        _cache.Remove(CacheProdutosKey);
        _cache.Remove(GetProdutoCacheKey(id));

        // se temos referência ao produto (em POST ou PUT),
        // também limpamos o cache da categoria correspondente
        if (produto != null)
        {
            _cache.Remove(GetProdutosCategoriaCacheKey(produto.CategoriaId));
            SetCache(GetProdutoCacheKey(id), produto); // repopula cache do produto individual
        }
    }
#endregion
}