# ASISERVY - Microservices Architecture

### 1. Environment Configuration

Create and configure your environment variables:

```bash
# Copy the environment template
cp .env.example .env

# Edit the .env file with your specific configuration
```

### 2. Application Deployment

#### Using Docker Compose

```bash
# Navigate to the project root directory
cd ASISERVY

# Build and start all services
docker-compose up --build -d
```

#### Service Status Verification

```bash
# Check running containers
docker-compose ps

# View service logs
docker-compose logs gateway
docker-compose logs customers
docker-compose logs products
```

### 3. Application Access

Once all services are running successfully:

| Service         | URL                             | Description                   |
| --------------- | ------------------------------- | ----------------------------- |
| **API Gateway** | `http://localhost:3000`         | Main application entry point  |
| **Swagger UI**  | `http://localhost:3000/swagger` | Interactive API documentation |
| **Database**    | `localhost:1433`                | SQL Server instance           |

### Local Development Setup

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run specific service locally
dotnet run --project src/ClientGateway
```

### Database Migrations

The application uses Entity Framework Core for database management:

```bash
# Customers Service Migrations
dotnet ef migrations add InitialCreate --project src/services/Customers/Customers.Infrastructure --startup-project src/services/Customers

# Products Service Migrations
dotnet ef migrations add InitialCreate --project src/services/Products/Products.Infrastructure --startup-project src/services/Products

# Apply migrations
dotnet ef database update --project src/services/Customers/Customers.Infrastructure --startup-project src/services/Customers
dotnet ef database update --project src/services/Products/Products.Infrastructure --startup-project src/services/Products
```

### Testing

Access the Swagger UI at `http://localhost:3000/swagger` to test all available endpoints interactively.

## Docker Services

The application consists of the following containerized services:

| Service     | Port     | Description            |
| ----------- | -------- | ---------------------- |
| `gateway`   | 3000     | API Gateway service    |
| `customers` | Internal | Customers microservice |
| `products`  | Internal | Products microservice  |
| `sqlserver` | 1433     | SQL Server database    |

### Useful Commands

```bash
# Stop all services
docker-compose down

# Remove all containers and volumes
docker-compose down -v

# Rebuild specific service
docker-compose up --build gateway

# View service logs
docker-compose logs -f customers
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For questions or support, please contact the development team or create an issue in the project repository.
