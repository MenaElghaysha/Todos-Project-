using Application.Common.Interfaces;
using MediatR;
namespace Application.Todos.Commands.CreateTodo;

public sealed class CreateTodoCommandHandler(IAppDbContext dbContext) : IRequestHandler<CreateTodoCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Domain.Todos.Todo
        {
            Id = Guid.NewGuid(),
            Title = request.Title
        };

        dbContext.Todos.Add(todo);
        await dbContext.SaveChangesAsync(cancellationToken);

        return todo.Id;
    }
}