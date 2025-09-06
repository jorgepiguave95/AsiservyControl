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
builder.Services.AddDbContext<CustomersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseCors("AllowGateway");
app.UseAuthorization();

app.MapControllers();

app.Run();
