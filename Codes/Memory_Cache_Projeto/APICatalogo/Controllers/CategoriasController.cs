using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    // Estou usando ICategoriaRepository pois ela implementa IRepository<T>
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly ILogger<CategoriasController> _logger;

    private readonly IMemoryCache _cache;
    private const string CacheCategoriasKey = "CacheCategorias";

    public CategoriasController(ICategoriaRepository categoriaRepository,
        ILogger<CategoriasController> logger,
        IMemoryCache cache)
    {
        _categoriaRepository = categoriaRepository;
        _logger = logger;
        _cache = cache;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        if (!_cache.TryGetValue(CacheCategoriasKey, out IEnumerable<Categoria>? categorias))
        {
            categorias = _categoriaRepository.GetAll();

            // Executa este bloco SE categorias for nulo OU
            // SE categorias não tiver nenhum elemento.
            if (categorias is null || !categorias.Any())
            {
                _logger.LogWarning("Não existem categorias...");
                return NotFound("Não existem categorias...");
            }

            SetCache(CacheCategoriasKey, categorias);
        }
        return Ok(categorias);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        var cacheKey = GetCategoriaCacheKey(id);

        if (!_cache.TryGetValue(cacheKey, out Categoria? categoria))
        {
            categoria = _categoriaRepository.Get(c => c.CategoriaId == id);

            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com id= {id} não encontrada...");
                return NotFound($"Categoria com id= {id} não encontrada...");
            }

            SetCache(cacheKey, categoria);

        }
        return Ok(categoria);
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        var categoriaCriada = _categoriaRepository.Create(categoria);

        InvalidateCacheAfterChange(categoriaCriada.CategoriaId, categoriaCriada);

        return new CreatedAtRouteResult("ObterCategoria", 
            new { id = categoriaCriada.CategoriaId },
            categoriaCriada);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id <= 0 || categoria is null || id != categoria?.CategoriaId)
        {
            _logger.LogWarning("Dados inconsistentes...");
            return BadRequest("Dados inconsistentes");
        }

        var categoriaAtualizada = _categoriaRepository.Update(categoria);

        InvalidateCacheAfterChange(id, categoriaAtualizada);

        return Ok(categoriaAtualizada);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _categoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id={id} não encontrada...");
            return NotFound($"Categoria com id={id} não encontrada...");
        }

        var categoriaExcluida = _categoriaRepository.Delete(categoria);

        InvalidateCacheAfterChange(id);

        return Ok(categoriaExcluida);
    }
    private string GetCategoriaCacheKey(int id) => $"CacheCategoria_{id}";

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

    private void InvalidateCacheAfterChange(int id, Categoria? categoria = null)
    {
        _cache.Remove(CacheCategoriasKey);
        _cache.Remove(GetCategoriaCacheKey(id));

        if (categoria != null)
        {
            SetCache(GetCategoriaCacheKey(id), categoria);
        }
    }
}