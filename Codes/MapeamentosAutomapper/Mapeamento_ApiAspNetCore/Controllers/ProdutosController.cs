using Mapeamento_ApiAspNetCore.DTOs;
using Mapeamento_ApiAspNetCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mapeamento_ApiAspNetCore.Controllers;

// Controller
[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _service;

    public ProdutosController(IProdutoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get() => 
                      Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var produtoDto = await _service.GetByIdAsync(id);
        return produtoDto is not null ? Ok(produtoDto) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CriarProdutoDTO criarProdutoDto)
    {
        var produtoDto = await _service.CreateProdutoAsync(criarProdutoDto);
        return CreatedAtAction(nameof(GetById), new { id = produtoDto.Id }, produtoDto);
    }
}