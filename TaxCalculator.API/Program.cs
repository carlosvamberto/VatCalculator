using Microsoft.OpenApi.Models;
using TaxCalculator.Application.Interfaces;
using TaxCalculator.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "VAT Calculation API",
        Description = "Calcule VAT, NET and GROSS",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Carlos Vamberto Filho",
            Url = new Uri("https://www.linkedin.com/in/carlosvamberto/")
        }
    });
});

// Adding dependency injection
builder.Services.AddScoped<IVatCalculationService, VatCalculationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
