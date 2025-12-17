using DTOs;
using Entities;
using Mapster;
using System.Globalization;

namespace Mappings
{
    public class MapsterConfig
    {
        public static void ConfigureMapping()
        {
            TypeAdapterConfig<Company, CompanyDTO>
                .NewConfig()
                .Map(dest => dest.Employees, src => src.Employees.Adapt<List<EmployeeDTO>>());

            TypeAdapterConfig<Employee, EmployeeDTO>
                .NewConfig()
                .Map(dest => dest.SalarySummary, src => $"{src.Salary:N2} Euros")
                .Map(dest => dest.AddressSummary, src => $"{src.Address!.Street}, {src.Address.City} - {src.Address.State}");
        }
    }
}
