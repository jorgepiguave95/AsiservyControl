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

### 3. Application Access

Once all services are running successfully:

| Service         | URL                             | Description                   |
| --------------- | ------------------------------- | ----------------------------- |
| **API Gateway** | `http://localhost:3000`         | Main application entry point  |
| **Swagger UI**  | `http://localhost:3000/swagger` | Interactive API documentation |
| **Database**    | `localhost:1433`                | SQL Server instance           |

## Database Migrations

```bash
dotnet ef migrations add AddCustomerEmailValidation
dotnet ef migrations add CreateProductCategoryTable
dotnet ef migrations add UpdateProductPriceConstraints
```

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
