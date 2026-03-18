using Mapeamento_TinyMapper.DTOs;
using Mapeamento_TinyMapper.Entities;
using Nelibur.ObjectMapper;

namespace Mapeamento_TinyMapper.Mappings;

public static class MapeamentoConfig
{
    public static void Configurar()
    {
        TinyMapper.Bind<Usuario, UsuarioDto>(config =>
        {
            config.Bind(src => src.NomeCompleto, dest => dest.Nome);
            config.Bind(src => src.Email, dest => dest.Email);
            // Idade e EnderecoCompleto serão calculados após o mapeamento
        });
    }
}