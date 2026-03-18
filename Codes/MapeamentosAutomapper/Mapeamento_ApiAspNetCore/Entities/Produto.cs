namespace Mapeamento_ApiAspNetCore.Entities;

public class Produto
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
    public string? CodigoBarras { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public decimal Imposto { get; set; }
}
