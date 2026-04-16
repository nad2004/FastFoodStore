# FastFood Store API

A RESTful API for FastFood Store Management System built with Clean Architecture and .NET 7.

## Project Structure

```
FastFood.Store.Api/
├── Domain/              # Core business entities and domain logic
├── Infrastructure/      # Data access, EF Core, repositories
├── Application/         # Business logic, services, DTOs
├── Api/                 # ASP.NET Core controllers, middleware
├── Dockerfile
├── docker-compose.yml
└── .gitignore
```

## Prerequisites

- .NET 7.0 SDK or later
- SQL Server 2019 or later (or SQL Server Express)
- Visual Studio 2022 or VS Code (optional)

## Getting Started

### 1. Configure Connection String

Edit `Api/appsettings.Development.json` and add your SQL Server connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=FastFoodStoreDb;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=true;"
}
```

### 2. Create Database

Run migrations to create the database:

```bash
cd Api
dotnet ef database update --project ../Infrastructure --startup-project .
```

Or if you prefer to use Package Manager Console in Visual Studio:

```
Update-Database -Project Infrastructure -StartupProject Api
```

### 3. Run the Application

```bash
cd Api
dotnet run
```

The API will be available at `https://localhost:5001` with Swagger documentation at `https://localhost:5001/swagger`

## Using Docker

### Build and Run with Docker Compose

```bash
docker-compose up --build
```

- API will be available at `http://localhost:5000`
- SQL Server will be available at `localhost:1433`

**Note:** Update the connection string password in `docker-compose.yml` before running.

## API Documentation

Once running, visit `/swagger` to see the API documentation and test endpoints.

## Project Layers

### Domain Layer
- Contains entities, enums, and constants
- No dependencies on external frameworks
- Represents core business logic

### Infrastructure Layer
- Entity Framework Core configurations
- Repository implementations
- Database migrations
- External service integrations

### Application Layer
- Business logic and services
- DTOs for data transfer
- AutoMapper configurations
- Service interfaces

### API Layer
- ASP.NET Core controllers
- HTTP request/response handling
- Authentication and authorization
- Middleware configuration

## Key Features

- ✅ Clean Architecture
- ✅ Entity Framework Core
- ✅ AutoMapper
- ✅ Serilog logging
- ✅ Swagger/OpenAPI
- ✅ CORS support
- ✅ Dependency Injection
- ✅ Docker support

## Database Schema

### Core Entities

- **Customer** - Customer information
- **Order** - Customer orders
- **OrderItem** - Individual items in an order
- **MenuItem** - Menu items available
- **Category** - Menu item categories

## Development

### Adding a New Feature

1. Create entity in `Domain/Entities/`
2. Create DTO in `Application/DTOs/`
3. Create repository interface/implementation
4. Create service interface/implementation
5. Add controller with endpoints
6. Create database migration

### Database Migrations

```bash
# Create migration
dotnet ef migrations add MigrationName --project Infrastructure --startup-project Api

# Update database
dotnet ef database update --project Infrastructure --startup-project Api

# Remove last migration
dotnet ef migrations remove --project Infrastructure --startup-project Api
```

## Building for Production

```bash
dotnet build -c Release
dotnet publish -c Release -o ./publish
```

## Contributing

Please follow the coding conventions and architecture rules outlined in the documentation.

## License

This project is part of the BTL assignment for .NET development.
