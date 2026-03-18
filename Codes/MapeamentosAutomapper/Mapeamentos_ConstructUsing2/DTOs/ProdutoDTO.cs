namespace Mapeamentos_ConstructUsing2.DTOs;

public class ProdutoDTO
{
    // Código único gerado com base no ID e no nome
    public string? CodigoUnico { get; set; }
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
    // Lista de categorias baseada no tipo
    public List<string>? Categorias { get; set; }
}
