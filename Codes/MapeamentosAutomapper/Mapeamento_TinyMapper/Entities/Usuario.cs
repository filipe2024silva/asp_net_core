namespace Mapeamento_TinyMapper.Entities;

public class Usuario
{
    public Guid Id { get; set; }
    public string? NomeCompleto { get; set; }
    public string? Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public Endereco? EnderecoPrincipal { get; set; }
}
