# FastFood Store Management System (WinForms)

A Desktop Application for FastFood Store Management built with Clean Architecture and .NET 8 WinForms.

## Project Structure

```
FastFood.Store.sln
├── Domain/              # Core business entities and domain logic
├── Infrastructure/      # Data access, EF Core, repositories
├── Application/         # Business logic, services, DTOs
├── WinFormsApp.csproj   # WinForms presentation layer
├── Form1.cs             # Main Form
├── Program.cs           # Entry point
├── Dockerfile           # (Optional) For building the application
└── docker-compose.yml   # (Optional) For database/infrastructure
```

## Prerequisites

- .NET 8.0 SDK or later
- SQL Server or PostgreSQL (depending on configuration)
- Visual Studio 2022 (recommended for WinForms Designer)

## Getting Started

### 1. Configure Connection String

Edit `appsettings.json` (or `appsettings.Development.json`) and add your database connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=FastFoodStoreDb;Username=postgres;Password=your_password;"
}
```

### 2. Create Database

Run migrations to create the database:

```bash
dotnet ef database update --project Infrastructure --startup-project .
```

### 3. Run the Application

```bash
dotnet run
```

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

### WinForms Presentation Layer
- Windows Forms UI
- Dependency Injection configuration in `Program.cs`
- Form logic and event handling

## Key Features

- ✅ Clean Architecture
- ✅ WinForms (.NET 8)
- ✅ Entity Framework Core
- ✅ Dependency Injection
- ✅ Serilog logging
- ✅ Responsive UI (using WinForms layouts)

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
3. Create repository interface/implementation in `Infrastructure`
4. Create service interface/implementation in `Application`
5. Create/Update WinForms in `WinFormsApp`
6. Create database migration if needed

## Building for Production

```bash
dotnet build -c Release
dotnet publish -c Release -o ./publish
```

## Contributing

Please follow the coding conventions and architecture rules outlined in the documentation.
