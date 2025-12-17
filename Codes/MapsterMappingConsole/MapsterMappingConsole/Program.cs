using DTOs;
using Mappings;
using Mapster;

Console.WriteLine("\nUsing Mapster\n");

MapsterConfig.ConfigureMapping();

var employees = new List<Entities.Employee>
{
    new Entities.Employee
    {
        Id = 1,
        Name = "Alice Johnson",
        Position = "Developer",
        Salary = 60000,
        Address = new Entities.Address
        {
            Street = "123 Maple St",
            City = "Springfield",
            State = "IL"
        }
    },
    new Entities.Employee
    {
        Id = 2,
        Name = "Bob Smith",
        Position = "Manager",
        Salary = 75000,
        Address = new Entities.Address
        {
            Street = "456 Oak St",
            City = "Greenville",
            State = "TX"
        }
    },
    new Entities.Employee
    {
        Id = 3,
        Name = "Carol White",
        Position = "Designer",
        Salary = 50000,
        Address = new Entities.Address
        {
            Street = "789 Pine St",
            City = "Fairview",
            State = "CA"
        }
    }
};

var company = new Entities.Company
{
    Name = "Tech Solutions",
    Employees = employees
};

CompanyDTO companyDto = company.Adapt<CompanyDTO>();

Console.WriteLine($"Company: {companyDto.Name}");
Console.WriteLine("\nEmployees:");

foreach (var emp in companyDto.Employees!)
{
    Console.WriteLine($"Name: { emp.Name}");
    Console.WriteLine($"Position: {emp.Position}");
    Console.WriteLine($"Salary: {emp.SalarySummary}");
    Console.WriteLine($"Address: {emp.AddressSummary}");

    Console.WriteLine("--------------------------------------");
}

Console.ReadKey();