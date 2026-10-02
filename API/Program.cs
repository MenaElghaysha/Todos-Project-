using FluentValidation;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using API.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add ProblemDetails middleware to the application, which provides a standardized way to handle and return error responses in a consistent format.
// This middleware is useful for returning detailed error information to clients in a structured manner, making it easier to diagnose issues and understand the nature of errors that occur during API requests.
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Application.IAssemblyMarker).Assembly));

builder.Services.AddValidatorsFromAssembly(
    typeof(Application.IAssemblyMarker).Assembly);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Register the ValidationBehaviors as a pipeline behavior for MediatR
// make new instance of ValidationBehaviors and add it to the pipeline 
// make new object of ValidationBehaviors and add it to the pipeline
builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(Application.Behaviors.ValidationBehaviors<,>));


// make new instance of AppDbContext and add it to the DI container , (like the sigleton pattern) , so that it can be injected into other classes that need it.
builder.Services.AddScoped<IAppDbContext, AppDbContext>();

var app = builder.Build();

app.UseExceptionHandler(); // Use the custom exception handling middleware to handle exceptions and return appropriate error responses.

// app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();