using Customers.Aplication.Interfaces;
using Customers.Aplication.Services;
using Customers.Domain.Entities;
using Customers.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//
builder.Services.AddScoped<ICustomerService, CustomerService>();


// Repository
builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();


// Entity Framework


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
