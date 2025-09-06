using Customers.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Customers.Infraestructure.Factories
{
    public class CustomersDbContextFactory : IDesignTimeDbContextFactory<CustomersDbContext>
    {
        public CustomersDbContext CreateDbContext(string[] args)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "1433";
            var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "sa";
            var dbPass = Environment.GetEnvironmentVariable("DB_PASS") ?? "a6Dd7GLHK2Zg4HVvSn";
            var dbName = Environment.GetEnvironmentVariable("DB_NAME_CONSUMER") ?? "CustomersDB";

            var connectionString = $"Server={dbHost},{dbPort};Database={dbName};User Id={dbUser};Password={dbPass};TrustServerCertificate=true;";

            var optionsBuilder = new DbContextOptionsBuilder<CustomersDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new CustomersDbContext(optionsBuilder.Options);
        }
    }
}
