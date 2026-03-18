namespace Mapeamento_ApiAspNetCore.DTOs;

public class CriarProdutoDTO
{
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
    public string? CodigoBarras { get; set; }
    public decimal Imposto { get; set; }
}
