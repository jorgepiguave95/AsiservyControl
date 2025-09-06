var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Asiservy API Gateway",
        Version = "v1",
        Description = "Interfaz centralizada de los metodos de los microservicios de Asiservy"
    });
});

var customersBaseUrl = builder.Configuration["CustomersMicroservice"] ?? "http://localhost:5201";
var productsBaseUrl = builder.Configuration["ProductsMicroservice"] ?? "http://localhost:5203";

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
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Asiservy API Gateway v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseCors("AllowAll");
app.UseAuthorization();

// Health check endpoint
app.MapGet("/health", () => new
{
    Status = "Healthy",
    Timestamp = DateTime.UtcNow,
    Environment = app.Environment.EnvironmentName,
    Services = new
    {
        CustomersService = customersBaseUrl,
        ProductsService = productsBaseUrl
    }
});

app.MapControllers();

app.Run();
