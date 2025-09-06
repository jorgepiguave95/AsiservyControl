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
var dbHost = builder.Configuration["DB_HOST"];
var dbPort = builder.Configuration["DB_PORT"];
var dbUser = builder.Configuration["DB_USER"];
var dbPass = builder.Configuration["DB_PASS"];
var dbName = builder.Configuration["DB_NAME"];

var connectionString = $"Server={dbHost},{dbPort};Database={dbName};User Id={dbUser};Password={dbPass};TrustServerCertificate=true;";

builder.Services.AddDbContext<ProductsDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseCors("AllowGateway");
app.UseAuthorization();

app.MapControllers();

app.Run();
