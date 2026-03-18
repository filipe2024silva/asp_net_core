using Mapeamento_TinyMapper.DTOs;
using Mapeamento_TinyMapper.Entities;
using Nelibur.ObjectMapper;

namespace Mapeamento_TinyMapper.Services;

public class UsuarioService
{
    public List<UsuarioDto> ObterUsuariosParaExibicao()
    {
        var usuarios = ObterUsuariosDoBanco();
        var usuariosDto = new List<UsuarioDto>();

        foreach (var usuario in usuarios)
        {
            var dto = TinyMapper.Map<UsuarioDto>(usuario);

            dto.Idade = CalcularIdade(usuario.DataNascimento);

            if (usuario.EnderecoPrincipal is not null)
            {
                dto.EnderecoCompleto = $"{usuario.EnderecoPrincipal.Logradouro}, {usuario.EnderecoPrincipal.Numero} - " +
                                       $"{usuario.EnderecoPrincipal.Cidade}/{usuario.EnderecoPrincipal.UF} - CEP: {usuario.EnderecoPrincipal.CEP}";
            }
            usuariosDto.Add(dto);
        }
        return usuariosDto;
    }


    private List<Usuario> ObterUsuariosDoBanco()
    {
        return new List<Usuario>
        {
            new Usuario
            {
                Id = Guid.NewGuid(),
                NomeCompleto = "João da Silva",
                Email = "joao.silva@email.com",
                DataNascimento = new DateTime(1985, 5, 10),
                EnderecoPrincipal = new Endereco
                {
                    Logradouro = "Rua das Flores",
                    Numero = "123",
                    Cidade = "São Paulo",
                    UF = "SP",
                    CEP = "01010-010"
                }
            },
            new Usuario
            {
                Id = Guid.NewGuid(),
                NomeCompleto = "Amanda Torres",
                Email = "amanda.torres@email.com",
                DataNascimento = new DateTime(1992, 8, 23),
                EnderecoPrincipal = new Endereco
                {
                    Logradouro = "Avenida Brasil",
                    Numero = "456",
                    Cidade = "Rio de Janeiro",
                    UF = "RJ",
                    CEP = "22030-000"
                }
            }
        };
    }
    private int CalcularIdade(DateTime nascimento)
    {
        var hoje = DateTime.Today;
        var idade = hoje.Year - nascimento.Year;
        if (nascimento.Date > hoje.AddYears(-idade)) idade--;
        return idade;
    }
}
