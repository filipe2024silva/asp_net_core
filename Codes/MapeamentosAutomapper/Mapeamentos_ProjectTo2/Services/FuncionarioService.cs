using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mapeamentos_ProjectTo2.Context;
using Mapeamentos_ProjectTo2.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Mapeamentos_ProjectTo2.Services;

public class FuncionarioService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public FuncionarioService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<FuncionarioDTO>> ObterFuncionariosDTO()
    {
        return await _context.Funcionarios
            //.AsNoTracking() // Evita tracking para melhor performance
            .ProjectTo<FuncionarioDTO>(_mapper.ConfigurationProvider) // 🔥 Mapeamento direto no banco
            .ToListAsync();
    }

    public async Task<List<FuncionarioDTO>> ObterFuncionariosMap()
    {
        var funcionarios = await _context.Funcionarios.ToListAsync(); // 🔥 Carrega tudo na memória
        return _mapper.Map<List<FuncionarioDTO>>(funcionarios); // 🔄 Mapeia depois
    }
}
