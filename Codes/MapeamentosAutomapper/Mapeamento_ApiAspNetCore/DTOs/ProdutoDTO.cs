namespace Mapeamento_ApiAspNetCore.DTOs;

public class ProdutoDTO
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? PrecoFormatado { get; set; } // Exemplo de transformação
    public string? Status { get; set; } // exemplo condicional
    public string? DataCriacaoFormatada { get; set; } // formatação
    public decimal PrecoComImposto { get; set; } //cálculos
    public string? Observacao { get; set; } // AfterMap
    public string? CodigoBarrasFormatado { get; set; } // ConvertUsing
}
