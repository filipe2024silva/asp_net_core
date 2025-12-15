using APICatalogue.Extensions;
using APICatalogue.Filters;
using Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<AppDbContext>(options => 
                    options.UseMySql(mySqlConnectionStr, 
                    ServerVersion.AutoDetect(mySqlConnectionStr)));

builder.Services.AddScoped<ApiLoggingFilter>();

builder.Services.AddScoped<Repositories.ICategoryRepository, Repositories.CategoryRepository>();
builder.Services.AddScoped<Repositories.IProductRepository, Repositories.ProductRepository>();
builder.Services.AddScoped(typeof(Repositories.IRepository<>), typeof(Repositories.Repository<>));
builder.Services.AddScoped<Repositories.IUnitOfWork, Repositories.UnitOfWork>();

builder.Logging.AddProvider(new Logging.CustomerLoggerProvider(new Logging.CustomerLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
