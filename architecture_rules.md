# Clean Architecture Rules & Guidelines

This document outlines the architectural rules and responsibilities for the `CMCDTQG.ADMIN.API` solution.

## 1. The Dependency Rule
The overriding rule in Clean Architecture is that **source code dependencies must point inwards**. 
- The **Domain** layer knows nothing about any other layer. It has no external dependencies.
- The **Application** layer depends only on the **Domain** layer.
- The **Infrastructure** and **Api** (Presentation) layers depend on the **Application** and **Domain** layers.
- The **Api** layer should **never** directly reference the **Infrastructure** layer (except for configuring Dependency Injection in `Program.cs`, which should be minimized using DI extension methods located in Infrastructure).

---

## 2. Layer Responsibilities

### 2.1 Domain Layer (`Domain`)
This is the core of the system. It contains the business and behavioral rules.
- **Entities**: Business objects with a unique identity (e.g., `User`, `Document`).
- **Value Objects**: Immutable objects that are defined by their attributes rather than an identity.
- **Enums**: Strongly-typed enumerations defining business states or types.
- **Constants**: Application-wide static business values.
- **Rule**: NO external dependencies. Do not reference Entity Framework, ASP.NET Core, or any third-party frameworks here.

### 2.2 Application Layer (`Application`)
This layer coordinates the application's use cases and handles the business flow.
- **Interfaces**: Defines contracts for repositories (`IRepository`), external services (`IEmailService`), etc.
- **Services/Use Cases**: Contains the core logic that manipulates Domain entities.
- **DTOs (Data Transfer Objects)**: Objects used to transfer data between layers, keeping Domain entities isolated from the presentation.
- **Mappers**: Logic to map between Entities and DTOs.
- **Rule**: Depends ONLY on the Domain layer. It should not contain any SQL, HTTP calls, or file system interactions. It defines *what* is needed (via Interfaces), not *how* it's implemented.

### 2.3 Infrastructure Layer (`Infrastructure`)
This layer implements the interfaces defined in the Application layer.
- **Persistence**: Contains the `AppDbContext`, EF Core Configurations, and Migrations.
- **Repositories**: Implements the `IRepository` interfaces defined in the Application layer, performing actual database queries.
- **External Services**: Integrations with external APIs (e.g., Minio, Email providers).
- **Rule**: This is where technology-specific code lives. If you change the database provider, only this layer should be modified.

### 2.4 API / Presentation Layer (`Api`)
The entry point of the application.
- **Controllers**: Responsible for receiving HTTP requests, validating input formats, routing to the Application layer, and returning correct HTTP responses (200, 400, 404, 500).
- **Filters/Middleware**: Global exception handling, authentication/authorization filters.
- **Program.cs**: Application startup, middleware pipeline configuration, and Dependency Injection bootstrapping.
- **Rule**: Controllers should be "thin". They should not contain business rules or direct database access. They simply translate HTTP requests into Application layer calls.

---

## 3. General Architectural Best Practices
- **Fail Fast**: Validate incoming data as early as possible in the Application or API layer.
- **Separation of Concerns**: Keep files small and focused on a single responsibility (Single Responsibility Principle).
- **Dependency Injection**: Rely heavily on interfaces and constructor injection. Avoid service locator patterns and static classes for business logic.
