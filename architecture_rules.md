# WinForms Application Architecture Rules & Guidelines

This document outlines the architectural rules and responsibilities for the `FastFood.Store` WinForms solution.

## 1. The Dependency Rule
The overriding rule is that **source code dependencies must point inwards**. 
- The **Domain** layer is the core and knows nothing about any other layer.
- The **Application** layer contains business logic and depends only on Domain.
- The **WinForms (Root)** layer and **Infrastructure** layer are the outermost layers.
- In this WinForms setup, the UI layer interacts directly with **Application Services** or **Infrastructure Repositories** via Dependency Injection.

---

## 2. Layer Responsibilities

### 2.1 Domain Layer (`Domain`)
The core business logic and entities.
- **Entities**: Objects like `Order`, `MenuItem`, `Customer`.
- **Enums/Constants**: Business-related types and static values.
- **Rule**: No dependencies on UI frameworks or Database technologies.

### 2.2 Application Layer (`Application`)
The "Brain" of the application that coordinates tasks.
- **Services**: Classes that handle business operations (e.g., `OrderService.PlaceOrder()`).
- **DTOs**: Data structures used to pass data to and from the UI.
- **Rule**: Contains the "What to do" logic. It should not know about Windows Forms or specific database SQL.

### 2.3 Infrastructure Layer (`Infrastructure`)
The technical implementation details.
- **Persistence**: `AppDbContext`, EF Core migrations.
- **Repositories**: Direct data access logic (SQL/EF Core).
- **Rule**: Handles *how* data is stored and retrieved.

### 2.4 WinForms / Presentation Layer (Root)
The user interface and entry point.
- **Forms/Controls**: Handle user input, button clicks, and data display.
- **Program.cs**: Bootstraps the application, configures Dependency Injection (DI), and starts the main form.
- **Logic**: UI logic should only handle validation of input formats and calling the appropriate service/function. It should NOT contain complex business rules or raw SQL queries.

---

## 3. WinForms Specific Best Practices

- **Dependency Injection**: Always use constructor injection for Forms. All services and repositories should be registered in `Program.cs`.
- **Event Handling**: Keep event handlers small. Delegate complex work to the Application layer.
- **Async/Await**: Use asynchronous calls for database or long-running operations to keep the UI responsive (avoid freezing the window).
- **Thread Safety**: Ensure UI updates from background tasks are performed on the UI thread (using `Invoke` if necessary).
- **Separation of Concerns**: Don't put business logic inside `button_Click` methods. Move it to a Service class.

---

## 4. Database Interaction
Since there is no Web API, the WinForms application communicates directly with the database via the Infrastructure layer.
- **Direct Calls**: The Form calls an injected Service or Repository.
- **DataContext Lifecycle**: The `AppDbContext` is typically scoped per operation or short-lived to avoid memory issues in a long-running desktop app.
