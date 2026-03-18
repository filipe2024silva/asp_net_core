namespace Mapeamentos_ProjectTo.DTOs;

public class PessoaDTO
{
    public string? Nome { get; set; }
    public int Idade { get; set; }
    // Propriedade customizada no DTO
    public string? EnderecoCompleto { get; set; }
}
