namespace Mapeamento_TinyMapper.DTOs;

public class UsuarioDto
{
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public int Idade { get; set; }
    public string? EnderecoCompleto { get; set; }

    public void ExibirNoConsole()
    {
        Console.WriteLine("\n=== DADOS DO USUÁRIO ===\n");
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine($"Idade: {Idade} anos");
        Console.WriteLine($"Endereço: {EnderecoCompleto}");
        Console.WriteLine(new string('=', 70));
    }
}
