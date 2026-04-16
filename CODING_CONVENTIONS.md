# FastFood.Store.Api - Coding Conventions & Project Setup Guide

## Table of Contents
1. [Project Overview](#project-overview)
2. [Architecture](#architecture)
3. [Naming Conventions](#naming-conventions)
4. [Code Organization](#code-organization)
5. [Coding Standards](#coding-standards)
6. [Best Practices](#best-practices)
7. [Setup Instructions for New Projects](#setup-instructions-for-new-projects)

---

## Project Overview

**Project Name:** FastFood.Store.Api (FastFood Store Management API)

**Technology Stack:**
- **.NET:** .NET 6+ (ASP.NET Core)
- **Architecture:** Clean Architecture (Layered)
- **Database:** SQL Server with Entity Framework Core
- **Logging:** Serilog
- **Authentication:** JWT (JSON Web Tokens)
- **Container:** Docker + Docker Compose

**Project Type:** RESTful API for FastFood Store Management System

---

## Architecture

This project follows **Clean Architecture** with 4-layer structure:

```
┌─────────────────────────────────────────┐
│         Api (Presentation Layer)        │
│  - Controllers, Filters, Attributes    │
│  - Order, Menu, Payment Endpoints      │
└────────────────┬────────────────────────┘
                 │
┌────────────────▼────────────────────────┐
│    Application (Business Logic Layer)   │
│  - Services, DTOs, Mappers, Configs    │
│  - Interfaces (IServices, IRepositories)│
│  - Order Processing, Inventory Mgmt    │
└────────────────┬────────────────────────┘
                 │
┌────────────────▼────────────────────────┐
│      Domain (Core Business Layer)       │
│  - Entities, Enums, Constants, Rules   │
│  - Order, MenuItem, Customer Models    │
└────────────────┬────────────────────────┘
                 │
┌────────────────▼────────────────────────┐
│    Infrastructure (Data Access Layer)   │
│  - DbContext, Repositories, Migrations  │
│  - Payment Services, Notifications      │
└─────────────────────────────────────────┘
```

### Layer Responsibilities

#### 1. **Domain Layer** (Domain.csproj)
- Core business entities and domain models
- Constants, Enums, and Business Rules
- Should have NO external dependencies
- Pure C# classes (POCOs)

#### 2. **Infrastructure Layer** (Infrastructure.csproj)
- Database connections and Entity Framework Core configuration
- Repository implementations
- Database migrations
- External service integrations
- Persistence logic

#### 3. **Application Layer** (Application.csproj)
- Business logic and services
- Data Transfer Objects (DTOs)
- AutoMapper configurations
- Application configurations
- Service interfaces and implementations

#### 4. **Api Layer** (Api.csproj)
- ASP.NET Core Controllers
- Filters and Attributes
- API endpoints and routing
- HTTP request/response handling
- Middleware configuration

---

## Naming Conventions

### Class Naming

| Type | Convention | Example |
|------|-----------|---------|
| Controller | `[Feature]Controller` | `OrderController`, `MenuItemController` |
| Base Controller | `Base[Module]Controller` | `BaseStoreController`, `BaseAuthController` |
| Service Interface | `I[Feature]Service` | `IOrderService`, `IMenuItemService` |
| Service Implementation | `[Feature]Service` | `OrderService`, `MenuItemService` |
| Repository Interface | `I[Entity]Repository` | `IOrderRepository`, `IMenuItemRepository` |
| Repository Implementation | `[Entity]Repository` | `OrderRepository`, `MenuItemRepository` |
| DTO | `[Entity]Dto` or `[Feature]Dto` | `OrderDto`, `CreateOrderDto`, `UpdateOrderDto` |
| Entity | `PascalCase` | `Order`, `MenuItem`, `Customer` |
| Constant Class | `[Area]Constant` | `MessageConstant`, `OrderStatusConstant` |
| Attribute | `[Feature]Attribute` | `ValidateJwtAttribute`, `ValidateOrderStatusAttribute` |
| Filter | `[Feature]Filter` | `EnsureStoreActiveFilter` |

### Method Naming

- **Public methods:** `PascalCase`
- **Private methods:** `PascalCase` or `camelCase`
- **Async methods:** Suffix with `Async`

```csharp
public async Task<OrderDto> GetOrderByIdAsync(int id)
public bool ValidateOrder(Order order)
private void LogError(string message)
```

### Constant Naming

```csharp
// All UPPERCASE with underscores
public const string UNAUTHORIZED = "Common_401";
public const string ACCESS_DENIED = "Common_403";
public const int MAX_RETRY_ATTEMPTS = 3;
```

### Variable Naming

- **Public properties:** `PascalCase`
- **Private fields:** `_camelCase` (with underscore prefix)
- **Local variables:** `camelCase`

```csharp
public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public decimal TotalAmount { get; set; }
    
    private string _notes;
    private int _retryCount;
    
    public void Process()
    {
        var orderId = Id;
    }
}
```

### File Naming

- Match class name exactly: `HoSoController.cs`, `HoSoService.cs`
- Use folder organization by feature area
- Keep file names consistent with project structure

### Parameter Naming

Use descriptive names. Avoid single letters except in LINQ queries:

```csharp
// Good
public Order GetById(int orderId)
public void SendNotification(string phoneNumber, string message)

// Avoid
public Order GetById(int x)
public void SendNotification(string s1, string s2)
```

---

## Code Organization

### Folder Structure

```
Api/
├── Attributes/          # Custom attributes ([BlockHtml], [ValidateRecaptcha])
├── Controllers/         # API Controllers organized by feature
│   ├── BaseStoreController.cs
│   ├── Orders/         # Order management controllers
│   ├── MenuItems/      # Menu management controllers
│   ├── Customers/      # Customer management
│   └── ...
├── Filter/             # Custom filters
├── Program.cs          # Startup configuration
├── appsettings.*.json  # Configuration files
└── Dockerfile

Application/
├── IServices/          # Service interfaces
├── Services/           # Service implementations
├── DTOs/               # Data Transfer Objects
├── Mappers/            # AutoMapper configurations
├── Helpers/            # Utility helpers
├── EmailTemplates/     # Email templates
├── Resources/          # Static resources
└── Configs/            # Configuration classes

Domain/
├── Entities/           # Domain models
├── Enums/              # Enumeration types
├── Constants/          # Constant values
├── Attributes/         # Domain attributes
└── State/              # State machines or domain state

Infrastructure/
├── Persistence/        # DbContext and EF Core configurations
├── Repositories/       # Repository implementations
├── Migrations/         # Database migrations
├── ExternalServices/   # Third-party service integrations
├── DTOs/               # DTO mappings for persistence
├── Helpers/            # Data access helpers
└── Print/              # Print-related functionality
```

### Feature-Driven Folder Organization

Each feature area (e.g., Orders, MenuItems) should follow this structure:

```
Controllers/
├── OrderController.cs

Application/IServices/
├── IOrderService.cs

Application/Services/
├── OrderService.cs

Application/DTOs/
├── OrderDto.cs
├── CreateOrderDto.cs
├── UpdateOrderDto.cs

Infrastructure/Repositories/
├── OrderRepository.cs
├── OrderQueries.cs (if complex queries)

Domain/Entities/
├── Order.cs
```

---

## Coding Standards

### C# Version & Language Features

- Use **C# 10+** features
- Nullable reference types should be enabled (`#nullable enable`)
- Use `var` for anonymous types, explicit types for clarity otherwise

### Formatting

```csharp
// Indentation: 4 spaces (or 1 tab)
// Line width: Keep under 120 characters where possible
// Always use braces even for single-line statements
public void Method()
{
    if (condition)
    {
        DoSomething();
    }
}
```

### Imports Organization

Group imports in this order:
1. System namespaces
2. Third-party packages
3. Project namespaces
4. Application namespaces

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Serilog;
using AutoMapper;

using Domain.Constants;
using Domain.Entities;

using Application.IServices;
using Application.DTOs;
```

### Access Modifiers

Always explicitly declare access modifiers:

```csharp
// Good
public class HoSo
{
    public int Id { get; set; }
    private string _name;
    protected void Method() { }
}

// Avoid
class HoSo
{
    int Id { get; set; }
}
```

### Comments

- Use XML documentation for public methods/classes
- Keep comments meaningful and up-to-date
- Use `//` for single-line and `/* */` for multi-line

```csharp
/// <summary>
/// Retrieves an order by its unique identifier
/// </summary>
/// <param name="orderId">The identifier of the order</param>
/// <returns>The order if found; otherwise null</returns>
public async Task<Order> GetOrderByIdAsync(int orderId)
{
    // Query database for the order
    return await _repository.GetByIdAsync(orderId);
}
```

### Exception Handling

```csharp
// Good
try
{
    // Operation
}
catch (ArgumentNullException ex)
{
    _logger.Error(ex, "Invalid argument provided");
    throw;
}
catch (Exception ex)
{
    _logger.Error(ex, "Unexpected error occurred");
    throw;
}
finally
{
    // Cleanup
}
```

### Async/Await

Always use async/await for I/O operations:

```csharp
// Good
public async Task<HoSoDto> GetHoSoAsync(int id)
{
    return await _service.GetByIdAsync(id);
}

// Avoid
public HoSoDto GetHoSo(int id)
{
    return _service.GetById(id).Result; // Blocks thread
}
```

---

## Best Practices

### 1. Dependency Injection

Register dependencies in `Program.cs`:

```csharp
// Register services
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();

// Register configurations
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
```

### 2. Data Transfer Objects (DTOs)

Always use DTOs for controller inputs/outputs:

```csharp
// Controller
[HttpPost]
public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto dto)
{
    var result = await _service.CreateAsync(dto);
    return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
}
```

### 3. AutoMapper Configuration

```csharp
public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<CreateOrderDto, Order>();
        CreateMap<UpdateOrderDto, Order>();
    }
}

public class MenuItemMappingProfile : Profile
{
    public MenuItemMappingProfile()
    {
        CreateMap<MenuItem, MenuItemDto>().ReverseMap();
        CreateMap<CreateMenuItemDto, MenuItem>();
        CreateMap<UpdateMenuItemDto, MenuItem>();
    }
}
```

### 4. Repository Pattern

```csharp
public interface IOrderRepository
{
    Task<Order> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId);
    Task AddAsync(Order entity);
    Task UpdateAsync(Order entity);
    Task DeleteAsync(int id);
}

public class OrderRepository : IOrderRepository
{
    private readonly DbContext _context;

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
    {
        return await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
    }
}
```

### 5. Service Layer Pattern

```csharp
public interface IOrderService
{
    Task<OrderDto> GetByIdAsync(int id);
    Task<OrderDto> CreateAsync(CreateOrderDto dto);
    Task UpdateAsync(int id, UpdateOrderDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<OrderDto>> GetByCustomerIdAsync(int customerId);
    Task<OrderDto> UpdateOrderStatusAsync(int id, string status);
}

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository repository, IMapper mapper, ILogger<OrderService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<OrderDto> GetByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving Order with ID: {OrderId}", id);
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<OrderDto>(entity);
    }

    public async Task<IEnumerable<OrderDto>> GetByCustomerIdAsync(int customerId)
    {
        _logger.LogInformation("Retrieving Orders for Customer ID: {CustomerId}", customerId);
        var entities = await _repository.GetByCustomerIdAsync(customerId);
        return _mapper.Map<IEnumerable<OrderDto>>(entities);
    }

    public async Task<OrderDto> UpdateOrderStatusAsync(int id, string status)
    {
        _logger.LogInformation("Updating Order {OrderId} status to {Status}", id, status);
        var entity = await _repository.GetByIdAsync(id);
        entity.Status = status;
        await _repository.UpdateAsync(entity);
        return _mapper.Map<OrderDto>(entity);
    }
}
```

### 6. API Controllers

```csharp
[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "Orders")]
public class OrderController : BaseStoreController
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>
    /// Get an order by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Create a new order
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Get orders by customer ID
    /// </summary>
    [HttpGet("customer/{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetByCustomerId(int customerId)
    {
        var result = await _service.GetByCustomerIdAsync(customerId);
        return Ok(result);
    }
}
```

### 7. Error Handling & Status Codes

| Code | Usage | Example |
|------|-------|---------|
| 200 | Successfully retrieved data | GET request success |
| 201 | Resource created | POST request success |
| 204 | No content | DELETE success |
| 400 | Bad request | Invalid parameters |
| 401 | Unauthorized | Missing/invalid JWT token |
| 403 | Forbidden | User lacks permission |
| 404 | Not found | Resource doesn't exist |
| 500 | Server error | Unexpected exception |

### 8. Constants Management

Organize constants in `Domain/Constants`:

```csharp
namespace Domain.Constants
{
    public static class MessageConstant
    {
        public static class CommonMessage
        {
            public const string UNAUTHORIZED = "Common_401";
            public const string ACCESS_DENIED = "Common_403";
            public const string NOT_FOUND = "Common_404";
        }

        public static class OrderMessage
        {
            public const string ORDER_NOT_FOUND = "Order_404";
            public const string ORDER_CANCELLED = "Order_001";
            public const string INVALID_ORDER_STATUS = "Order_002";
        }
    }

    public static class SystemConstant
    {
        public const string Role_Admin = "admin";
        public const string Role_Staff = "staff";
        public const string Role_Customer = "customer";
    }
}
```

### 9. Authentication & Authorization

Use base controller properties:

```csharp
[Authorize]
public class OrderController : BaseStoreController
{
    [HttpGet("my")]
    [Authorize(Roles = "customer,staff,admin")]
    public async Task<ActionResult> GetMyOrders()
    {
        var userId = UserId;       // From base controller
        var roles = Roles;         // From base controller
        var isStaff = Roles.Contains("staff");  // Helper from base controller
        
        return Ok(await _service.GetByCustomerIdAsync(userId));
    }
}
```

### 10. Logging

Use Serilog for structured logging:

```csharp
private readonly ILogger<OrderService> _logger;

public async Task<OrderDto> GetByIdAsync(int id)
{
    _logger.LogInformation("GetByIdAsync called with id: {OrderId}", id);
    
    try
    {
        var entity = await _repository.GetByIdAsync(id);
        _logger.LogInformation("Order found: {OrderId}", id);
        return _mapper.Map<OrderDto>(entity);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error retrieving Order with id: {OrderId}", id);
        throw;
    }
}
```

### 11. null Checks

Use null-coalescing and null-conditional operators:

```csharp
// Good
var value = obj?.Property ?? defaultValue;
var list = items?.ToList() ?? new List<Item>();

// Guard clauses
if (string.IsNullOrWhiteSpace(name))
    throw new ArgumentException("Name is required", nameof(name));
```

---

## Setup Instructions for New Projects

Follow these steps to create a new project following this architecture:

### Step 1: Create Solution Structure

```powershell
# Create solution folder
mkdir FastFood.Store.Api
cd FastFood.Store.Api

# Create .sln file
dotnet new sln -n FastFood.Store.Api
```

### Step 2: Create Projects

```powershell
# Create Domain project (no dependencies)
dotnet new classlib -n Domain
dotnet sln add Domain/Domain.csproj

# Create Infrastructure project
dotnet new classlib -n Infrastructure
dotnet sln add Infrastructure/Infrastructure.csproj
dotnet add Infrastructure/Infrastructure.csproj reference Domain/Domain.csproj

# Create Application project
dotnet new classlib -n Application
dotnet sln add Application/Application.csproj
dotnet add Application/Application.csproj reference Domain/Domain.csproj
dotnet add Application/Application.csproj reference Infrastructure/Infrastructure.csproj

# Create Api project
dotnet new webapi -n Api
dotnet sln add Api/Api.csproj
dotnet add Api/Api.csproj reference Application/Application.csproj
dotnet add Api/Api.csproj reference Infrastructure/Infrastructure.csproj
dotnet add Api/Api.csproj reference Domain/Domain.csproj
```

### Step 3: Install NuGet Packages

#### Domain Project
```xml
<!-- Domain.csproj -->
<ItemGroup>
    <PackageReference Include="System.ComponentModel.Annotations" Version="4.7.0" />
</ItemGroup>
```

#### Infrastructure Project
```xml
<!-- Infrastructure.csproj -->
<ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="7.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="7.0.0" />
    <PackageReference Include="Serilog" Version="3.0.0" />
    <PackageReference Include="Serilog.Sinks.File" Version="5.0.0" />
</ItemGroup>
```

#### Application Project
```xml
<!-- Application.csproj -->
<ItemGroup>
    <PackageReference Include="AutoMapper" Version="12.0.0" />
    <PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Abstractions" Version="7.0.0" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="7.0.0" />
</ItemGroup>
```

#### Api Project
```xml
<!-- Api.csproj -->
<ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="7.0.0" />
    <PackageReference Include="Serilog.AspNetCore" Version="7.0.0" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
    <PackageReference Include="Microsoft.AspNetCore.Cors" Version="2.2.0" />
</ItemGroup>
```

### Step 4: Create Folder Structure

```powershell
# Domain
mkdir Domain/Entities
mkdir Domain/Enums
mkdir Domain/Constants
mkdir Domain/Attributes

# Infrastructure
mkdir Infrastructure/Persistence
mkdir Infrastructure/Repositories
mkdir Infrastructure/Migrations
mkdir Infrastructure/ExternalServices

# Application
mkdir Application/IServices
mkdir Application/Services
mkdir Application/DTOs
mkdir Application/Mappers
mkdir Application/Helpers
mkdir Application/Configs

# Api
mkdir Api/Controllers
mkdir Api/Attributes
mkdir Api/Filter
```

### Step 5: Create Base Classes

#### Domain/Entities/BaseEntity.cs
```csharp
namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
```

#### Application/DTOs/BaseDto.cs
```csharp
namespace Application.DTOs
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
```

#### Application/DTOs/Common/ApiResponse.cs
```csharp
namespace Application.DTOs.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int? ErrorCode { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
            => new() { Success = true, Data = data, Message = message };

        public static ApiResponse<T> ErrorResponse(string message, int? errorCode = null)
            => new() { Success = false, Message = message, ErrorCode = errorCode };
    }
}
```

### Step 6: Configure DbContext

#### Infrastructure/Persistence/AppDbContext.cs
```csharp
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Add your DbSets here
        public DbSet<Order> Orders { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Order entity
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            // Configure other entities and relationships
        }
    }
}
```

#### Infrastructure/DependencyInjection.cs
```csharp
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
```

### Step 7: Configure Application Services

#### Application/DependencyInjection.cs
```csharp
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register AutoMapper
            services.AddAutoMapper(typeof(DependencyInjection));

            // Register services
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
```

### Step 8: Configure Program.cs

```csharp
using Api.Attributes;
using Application;
using Infrastructure;
using Serilog;

var builder = WebApplicationBuilder.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add CORS if needed
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}
```

### Step 9: Create Docker Support

#### Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["Api/Api.csproj", "Api/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "Api/Api.csproj"

COPY . .

RUN dotnet build "Api/Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api/Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
```

#### docker-compose.yml
```yaml
version: '3.8'

services:
  api:
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "5000:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=YourDb;User Id=sa;Password=YourPassword;
    depends_on:
      - sqlserver

  sqlserver:
    image: mcr.microsoft.com/mssql/server:2019-latest
    ports:
      - "1433:1433"
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourPassword
    volumes:
      - sqlserver_data:/var/opt/mssql

volumes:
  sqlserver_data:
```

### Step 10: Initialize Git & Version Control

```powershell
# Initialize git
git init
git add .
git commit -m "Initial commit: Project setup"

# Create .gitignore
@"
## Ignore Visual Studio temporary files
bin/
obj/
.vs/
*.user
*.suo

## Ignore NuGet
*.nupkg
*.snupkg

## Ignore logs
logs/
*.log

## Environment files
appsettings.Development.json
appsettings.Staging.json
.env
"@ | Out-File .gitignore
```

### Step 11: Database Migrations

```powershell
# Create initial migration
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Api

# Update database
dotnet ef database update --project Infrastructure --startup-project Api
```

---

## Common Development Tasks

### Adding a New Feature/Module

1. **Create Entity in Domain**
   ```csharp
   // Domain/Entities/MenuItem.cs
   public class MenuItem : BaseEntity
   {
       public string Name { get; set; }
       public string Description { get; set; }
       public decimal Price { get; set; }
       public int CategoryId { get; set; }
       public Category Category { get; set; }
   }
   ```

2. **Create DTO in Application**
   ```csharp
   // Application/DTOs/MenuItemDto.cs
   public class MenuItemDto : BaseDto
   {
       public string Name { get; set; }
       public string Description { get; set; }
       public decimal Price { get; set; }
       public int CategoryId { get; set; }
   }
   ```

3. **Create Repository Interface & Implementation**
   ```csharp
   // Application/IRepositories/IMenuItemRepository.cs
   public interface IMenuItemRepository
   {
       Task<MenuItem> GetByIdAsync(int id);
       Task<IEnumerable<MenuItem>> GetByCategoryAsync(int categoryId);
       // ... other methods
   }
   ```

4. **Create Service Interface & Implementation**
   ```csharp
   // Application/IServices/IMenuItemService.cs
   public interface IMenuItemService
   {
       Task<MenuItemDto> GetByIdAsync(int id);
       Task<IEnumerable<MenuItemDto>> GetByCategoryAsync(int categoryId);
   }
   ```

5. **Create Database Migration**
   ```powershell
   dotnet ef migrations add AddMenuItem --project Infrastructure
   dotnet ef database update --project Infrastructure
   ```

6. **Create Controller**
   ```csharp
   // Api/Controllers/MenuItemController.cs
   [ApiController]
   [Route("api/[controller]")]
   public class MenuItemController : BaseStoreController
   {
       private readonly IMenuItemService _service;
   }
   ```

### Running the Application

```powershell
# Development
dotnet run --project Api/Api.csproj

# With Docker (requires Docker Desktop)
docker-compose up --build

# Build Release
dotnet build -c Release

# Run Unit Tests
dotnet test

# Create EF Migration
dotnet ef migrations add MigrationName --project Infrastructure --startup-project Api

# Update Database
dotnet ef database update --project Infrastructure --startup-project Api
```

---

## Code Review Checklist

- [ ] Follows naming conventions
- [ ] Proper access modifiers used
- [ ] Unit tests included
- [ ] No hardcoded values
- [ ] Error handling implemented
- [ ] Logging statements added
- [ ] Performance considerations addressed
- [ ] Security best practices followed
- [ ] XML documentation for public members
- [ ] No null reference exceptions possible
- [ ] Async/await used for I/O operations
- [ ] DTOs used instead of entities in controllers
- [ ] Repository/Service pattern followed
- [ ] Dependency injection properly configured

---

## Additional Resources

- [Microsoft .NET Documentation](https://docs.microsoft.com/dotnet)
- [Clean Architecture Guide](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Entity Framework Core Guide](https://docs.microsoft.com/ef/core/)
- [ASP.NET Core Web API Guide](https://docs.microsoft.com/aspnet/core/web-api/)
- [C# Coding Conventions](https://docs.microsoft.com/dotnet/csharp/fundamentals/coding-style/coding-conventions)

---

## Contributing

When contributing to this project:

1. Create a feature branch: `git checkout -b feature/your-feature-name`
2. Follow all coding conventions outlined in this document
3. Add unit tests for new functionality
4. Update this documentation if adding new patterns
5. Submit a pull request with clear description

---

---

## FastFood Store API - Common Entities Reference

### Core Entities

#### Order
```csharp
public class Order : BaseEntity
{
    public string OrderNumber { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } // Pending, Preparing, Ready, Delivered, Cancelled
    public DateTime OrderDate { get; set; }
    public string PaymentMethod { get; set; }
    public string DeliveryAddress { get; set; }
    public ICollection<OrderItem> Items { get; set; }
}
```

#### MenuItem
```csharp
public class MenuItem : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string ImageUrl { get; set; }
    public bool IsAvailable { get; set; }
}
```

#### OrderItem
```csharp
public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
```

#### Customer
```csharp
public class Customer : BaseEntity
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public ICollection<Order> Orders { get; set; }
}
```

#### Category
```csharp
public class Category : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<MenuItem> MenuItems { get; set; }
}
```

---

**Last Updated:** April 15, 2026
**Version:** 2.0 (FastFood Store Edition)
