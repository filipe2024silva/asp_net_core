using Mapeamento_ApiAspNetCore.DTOs;

namespace Mapeamento_ApiAspNetCore.Services;

public interface IProdutoService
{
    Task<List<ProdutoDTO>> GetAllAsync();
    Task<ProdutoDTO?> GetByIdAsync(int id);
    Task<ProdutoDTO> CreateProdutoAsync(CriarProdutoDTO criarProdutoDto);    
}
