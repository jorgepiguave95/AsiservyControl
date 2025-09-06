# ASISERVY - Arquitectura de Microservicios

# 1. Configuración del Entorno

1. Crea y configura tus variables de entorno, copia la plantilla de variables de entorno y edita el archivo .env con tu configuración específica

2. Abrir Docker Desktop

3. Abrir una terminal y ejecutar el siguiente comando en la raiz del proyecto

```bash
docker-compose up --build
```

4. Ingresar al gestor de base de datos y crear las bases de datos correspondientes.

5. Abrir la terminal y ubicarse en cada microservicio para realizar las migraciones de Entity Framework Core.

# Migraciones con dotnet

```bash Customner
# Navegar al proyecto de infraestructura
cd src/services/Customers/Customers.Infraestructure

# Crear migración
dotnet ef migrations add NombreMigracion

# Aplicar migración
dotnet ef database update

```

```bash Products
cd src/services/Products/Products.Infraestructure

# Crear migración
dotnet ef migrations add NombreMigracion

# Aplicar migración
dotnet ef database update

```

# Acceso a la Aplicación

Una vez que todos los servicios estén ejecutándose correctamente:

| Servicio          | URL                             | Descripción                         |
| ----------------- | ------------------------------- | ----------------------------------- |
| **API Gateway**   | `http://localhost:3000`         | Punto de entrada principal          |
| **Swagger UI**    | `http://localhost:3000/swagger` | Documentación interactiva de la API |
| **Base de Datos** | `localhost:1433`                | Instancia de SQL Server             |

# Servicios Docker

La aplicación está compuesta por los siguientes servicios en contenedores:

| Servicio    | Puerto  | Descripción                |
| ----------- | ------- | -------------------------- |
| `gateway`   | 3000    | Servicio API Gateway       |
| `customers` | Interno | Microservicio de Clientes  |
| `products`  | Interno | Microservicio de Productos |
| `sqlserver` | 1433    | Base de datos SQL Server   |

### Comandos Útiles

```bash
# Detener todos los servicios
docker-compose down

# Eliminar todos los contenedores y volúmenes
docker-compose down -v

# Reconstruir un servicio específico
docker-compose up --build gateway

# Ver logs de un servicio
docker-compose logs -f customers
```
