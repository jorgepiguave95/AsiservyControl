using Products.Aplication.Interfaces;
using Products.Aplication.Services;
using Products.Domain.Entities;
using Products.Infraestructure.Repositories;
using Products.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGateway", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Services
builder.Services.AddScoped<IProductService, ProductService>();

// Repository
builder.Services.AddScoped<IRepository<ProductControl>, ProductControlRepository>();
builder.Services.AddScoped<IProductControlRepository, ProductControlRepository>();

// Entity Framework
builder.Services.AddDbContext<ProductsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseCors("AllowGateway");
app.UseAuthorization();

app.MapControllers();

app.Run();
