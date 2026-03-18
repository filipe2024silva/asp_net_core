using Mapeamento_TinyMapper.Mappings;
using Mapeamento_TinyMapper.Services;

Console.WriteLine("=== DEMONSTRAÇÃO TINYMAPPER ===");

MapeamentoConfig.Configurar();

var service = new UsuarioService();

var usuarios = service.ObterUsuariosParaExibicao();

foreach (var usuario in usuarios)
{
    usuario.ExibirNoConsole();
    Console.WriteLine();
}

Console.ReadKey();
