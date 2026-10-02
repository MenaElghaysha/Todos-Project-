# Todo API — Clean Architecture & CQRS

A Todo REST API built with **ASP.NET Core 10**, following **Clean Architecture** and **CQRS** principles.

The project was built to practice designing a maintainable .NET backend by separating business logic, application use cases, infrastructure concerns, and API responsibilities.

## 🚀 Features

- Create, read, update, and delete Todo items
- Clean Architecture
- CQRS using MediatR
- Command and Query separation
- FluentValidation
- MediatR pipeline validation behavior
- Entity Framework Core
- SQLite database
- EF Core migrations
- Global exception handling
- Standardized error responses with ProblemDetails
- Dependency Injection
- Async/await throughout the application

---

## 🏗️ Architecture

The solution is divided into four main layers:

```text
┌──────────────────────────────┐
│             API              │
│ Controllers / Requests       │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│         Application          │
│ Commands / Queries /         │
│ Validation / Interfaces     │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│            Domain            │
│         Todo Entity          │
└──────────────────────────────┘
               ▲
               │
┌──────────────┴───────────────┐
│       Infrastructure         │
│ EF Core / SQLite / Data      │
└──────────────────────────────┘
```

### API

Responsible for handling HTTP requests and responses.

```text
API/
├── Controllers/
│   └── TodosController.cs
├── Requests/
│   ├── CreateTodoRequest.cs
│   ├── UpdateTodoRequest.cs
│   └── DeleteTodoRequest.cs
└── Exceptions/
    └── GlobalExceptionHandler.cs
```

### Application

Contains the application's use cases and business workflow.

```text
Application/
├── Behaviors/
│   ├── IAssemblyMarker.cs
│   └── ValidationBehaviors.cs
├── Common/
│   ├── Exceptions/
│   └── Interfaces/
└── Todos/
    ├── Commands/
    │   ├── CreateTodo/
    │   ├── UpdateTodo/
    │   └── DeleteTodo/
    └── Queries/
        ├── GetTodos/
        └── GetTodoById/
```

### Domain

Contains the core domain entities.

```text
Domain/
└── Todos/
    └── Todos.cs
```

The `Todo` entity contains:

```csharp
public Guid Id { get; set; }
public string Title { get; set; }
public bool IsCompleted { get; set; }
```

### Infrastructure

Responsible for persistence and database-related concerns.

```text
Infrastructure/
├── Data/
│   ├── AppDbContext.cs
│   └── Configurations/
│       └── TodoConfiguration.cs
└── Migrations/
```

The project uses **Entity Framework Core with SQLite** for data persistence.

---

## 🔄 CQRS

The project separates operations into **Commands** and **Queries**.

### Commands

Commands modify application state.

```text
CreateTodoCommand
UpdateTodoCommand
DeleteTodoCommand
```

Each command has its own handler.

For example:

```text
CreateTodo/
├── CreateTodoCommand.cs
├── CreateTodoCommandHandler.cs
└── CreateTodoCommandValidator.cs
```

### Queries

Queries retrieve data without modifying application state.

```text
GetTodosQuery
GetTodoByIdQuery
```

Each query has its own handler.

This separation keeps individual use cases focused and makes the application easier to extend and maintain.

---

## 🧩 MediatR

The API controller does not directly call the database or application handlers.

Instead, it sends commands and queries through **MediatR**:

```csharp
var query = new GetTodosQuery();

var todos = await mediator.Send(query);
```

The flow is:

```text
HTTP Request
     ↓
Controller
     ↓
MediatR
     ↓
Command / Query
     ↓
Handler
     ↓
Application / Infrastructure
     ↓
Database
```

---

## ✅ Validation

The project uses **FluentValidation** together with a MediatR pipeline behavior.

Validation is performed before the command reaches its handler.

```text
Request
   ↓
Controller
   ↓
MediatR Pipeline
   ↓
ValidationBehavior
   ↓
Validator
   ↓
Command Handler
```

This keeps validation separate from the controllers and handlers.

---

## 🛡️ Exception Handling

The API uses a global exception handler together with ASP.NET Core's `ProblemDetails` support.

```csharp
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
```

This provides a centralized approach for handling exceptions and returning consistent error responses.

---

## 🌐 API Endpoints

Base route:

```text
/api/Todos
```

| Method | Endpoint              | Description       |
| ------ | --------------------- | ----------------- |
| GET    | `/api/Todos`          | Get all todos     |
| GET    | `/api/Todos/{todoId}` | Get a todo by ID  |
| POST   | `/api/Todos`          | Create a new todo |
| PUT    | `/api/Todos/{todoId}` | Update a todo     |
| DELETE | `/api/Todos/{todoId}` | Delete a todo     |

### Create Todo

```http
POST /api/Todos
Content-Type: application/json
```

```json
{
  "title": "Learn Clean Architecture"
}
```

### Update Todo

```http
PUT /api/Todos/{todoId}
Content-Type: application/json
```

```json
{
  "title": "Learn Clean Architecture",
  "isCompleted": true
}
```

### Get All Todos

```http
GET /api/Todos
```

### Get Todo by ID

```http
GET /api/Todos/{todoId}
```

### Delete Todo

```http
DELETE /api/Todos/{todoId}
```

---

## 🛠️ Technologies

- **.NET 10**
- **ASP.NET Core Web API**
- **C#**
- **Entity Framework Core 10**
- **SQLite**
- **MediatR**
- **FluentValidation**
- **ProblemDetails**
- **Dependency Injection**
- **EF Core Migrations**

---

## 📁 Project Structure

```text
Clean Architecture & CQRS in action/
│
├── API/
│   ├── Controllers/
│   ├── Exceptions/
│   ├── Requests/
│   └── Program.cs
│
├── Application/
│   ├── Behaviors/
│   ├── Common/
│   └── Todos/
│       ├── Commands/
│       │   ├── CreateTodo/
│       │   ├── UpdateTodo/
│       │   └── DeleteTodo/
│       └── Queries/
│           ├── GetTodos/
│           └── GetTodoById/
│
├── Domain/
│   └── Todos/
│       └── Todos.cs
│
├── Infrastructure/
│   ├── Data/
│   └── Migrations/
│
├── .gitignore
└── README.md
```

---

## ▶️ Getting Started

### Prerequisites

Make sure you have:

- .NET 10 SDK
- Git

### Clone the repository

```bash
git clone https://github.com/MenaElghaysha/Todos-Project-.git
cd Todos-Project-
```

### Restore dependencies

```bash
dotnet restore
```

### Apply database migrations

```bash
dotnet ef database update --project Infrastructure --startup-project API
```

### Run the API

```bash
dotnet run --project API
```

The API will start using the configured ASP.NET Core development environment.

---

## 🎯 Project Goals

This project was created as a practical exercise to understand how to build a .NET Web API using:

- Clean Architecture
- CQRS
- MediatR
- Dependency Injection
- Entity Framework Core
- Validation pipelines
- Centralized exception handling

The main goal was not only to build a CRUD API, but to understand how these architectural patterns work together in a real application.

---

## 📌 Future Improvements

Possible future enhancements include:

- Authentication and authorization
- Pagination and filtering
- Unit and integration tests
- API documentation with Swagger/OpenAPI
- Repository abstraction where appropriate
- Logging and monitoring
- Docker support
- CI/CD pipeline
