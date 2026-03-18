using AutoMapper;
using Mapeamento_ApiAspNetCore.Data.Context;
using Mapeamento_ApiAspNetCore.DTOs;
using Mapeamento_ApiAspNetCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mapeamento_ApiAspNetCore.Services;

public class ProdutoService : IProdutoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProdutoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<ProdutoDTO>> GetAllAsync()
    {
        var produtos = await _context.Produtos.ToListAsync();
        return _mapper.Map<List<ProdutoDTO>>(produtos);
    }

    public async Task<ProdutoDTO?> GetByIdAsync(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        return produto is null ? null : _mapper.Map<ProdutoDTO>(produto);
    }

    public async Task<ProdutoDTO> CreateProdutoAsync(CriarProdutoDTO criaProdutoDto)
    {
        //mapeia CriarProdutoDTO para Produto
        var produto = _mapper.Map<Produto>(criaProdutoDto);
        produto.DataCriacao = DateTime.Now;
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        // mapeia Produto para ProdutoDTO
        return _mapper.Map<ProdutoDTO>(produto);
    }
}
