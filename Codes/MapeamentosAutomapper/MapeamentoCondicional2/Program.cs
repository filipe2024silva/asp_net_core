using AutoMapper;
using MapeamentoCondicional2;
using System;

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Funcionario, FuncionarioDTO>()
        .ForMember(dest => dest.NomeDepartamento, opt =>
            opt.Condition(src => src.Departamento != null))
        .ForMember(dest => dest.NomeDepartamento, opt =>
            opt.MapFrom(src => src.Cargo == "Gerente" ? 
                               src.Departamento!.Nome : src.Departamento!.Sigla));
});

var funcionarios = new List<Funcionario>
{
    new Funcionario
    {
        Id = 1, Nome = "Ana", Cargo = "Gerente",
        Departamento = new Departamento { Nome = "Recursos Humanos", Sigla = "RH" }
    },
    new Funcionario
    {
        Id = 2, Nome = "Carlos", Cargo = "Analista",
        Departamento = new Departamento { Nome = "Tecnologia da Informação", Sigla = "TI" }
    },
    new Funcionario
    {
        Id = 3, Nome = "Beatriz", Cargo = "Coordenadora",
        Departamento = new Departamento { Nome = "Marketing", Sigla = "MK" }
    },
    new Funcionario
    {
        Id = 4, Nome = "Paulo", Cargo = "Coordenador",
        Departamento = null
    },
    new Funcionario
    {
        Id = 5, Nome = "Helena", Cargo = "Gerente",
        Departamento = new Departamento { Nome= "Contabilidade", Sigla= "TI"}
    }
};

var mapper = config.CreateMapper();

// Mapeia os funcionários
var funcionariosDTO = mapper.Map<List<FuncionarioDTO>>(funcionarios);

// Filtra funcionários com departamento não nulo antes de mapear
//var funcionariosDTO = mapper.Map<List<FuncionarioDTO>>(
//    funcionarios.Where(f => f.Departamento != null).ToList()
//);

foreach (var funcionarioDTO in funcionariosDTO)
{
    Console.WriteLine($"Id: {funcionarioDTO.Id}, Nome: {funcionarioDTO.Nome}," +
        $" Cargo: {funcionarioDTO.Cargo}, Departamento: {funcionarioDTO.NomeDepartamento}");
}

Console.ReadLine();

