using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

// <summary>
// Represents the application's database context for Entity Framework Core.
// session on DB
public class AppDbContext(DbContextOptions<AppDbContext> options)
: DbContext(options),IAppDbContext
{

    // <summary>
    // Represents the collection of Todo entities in the database.
    // </summary>
    public DbSet<Domain.Todos.Todo> Todos => Set<Domain.Todos.Todo>();

    // <summary>
    // Configures the model for the database context.
    //يا EF Core، دور في الـ assembly دي على configurations زي TodoConfiguration وطبقها.
    // يعني بدل ما تحطي كل configuration جوه AppDbContext، عملوا لها ملفات منفصلة.
    // وده جزء من التنظيم.
    // </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations for the Todo entity
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Configurations.TodoConfiguration).Assembly);
    }
}