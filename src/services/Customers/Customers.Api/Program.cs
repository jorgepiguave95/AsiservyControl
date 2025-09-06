using Customers.Aplication.Interfaces;
using Customers.Aplication.Services;
using Customers.Domain.Entities;
using Customers.Infraestructure.Repositories;
using Customers.Infraestructure.Persistence;
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
builder.Services.AddScoped<ICustomerService, CustomerService>();

// Repository
builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();

// Entity Framework
var dbHost = builder.Configuration["DB_HOST"];
var dbPort = builder.Configuration["DB_PORT"];
var dbUser = builder.Configuration["DB_USER"];
var dbPassword = builder.Configuration["DB_PASS"];
var dbName = builder.Configuration["DB_NAME"];

var connectionString = $"Server={dbHost},{dbPort};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=true;";


builder.Services.AddDbContext<CustomersDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseCors("AllowGateway");
app.UseAuthorization();

app.MapControllers();

app.Run();
