using DTOs;
using Entities;
using Mapster;

namespace MapsterMapping.Mappings
{
    public class MpasterConfig
    {
        public static void ConfigureMapping()
        {
            TypeAdapterConfig<Company, CompanyDTO>
                .NewConfig()
                .Map(dest => dest.Employees, src => src.Employees.Adapt<List<EmployeeDTO>>());

            TypeAdapterConfig<Employee, EmployeeDTO>
                .NewConfig()
                .Map(dest => dest.SalarySummary, src => $"€ {src.Salary:N2}")
                .Map(dest => dest.AddressSummary, src => $"{src.Address!.Street}, {src.Address.City} - {src.Address.State}");
        }
    }
}
