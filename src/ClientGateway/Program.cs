var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var customersBaseUrl = builder.Configuration["Microservices:CustomersService:BaseUrl"] ?? "http://localhost:5201";
var productsBaseUrl = builder.Configuration["Microservices:ProductsService:BaseUrl"] ?? "http://localhost:5203";

builder.Services.AddHttpClient("CustomersService", client =>
{
    client.BaseAddress = new Uri(customersBaseUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHttpClient("ProductsService", client =>
{
    client.BaseAddress = new Uri(productsBaseUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30);
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
