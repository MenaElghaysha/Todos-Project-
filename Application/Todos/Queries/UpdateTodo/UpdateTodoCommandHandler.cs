using Application.Common.Interfaces;
using Application.Todos.Commands.UpdateTodo;
using MediatR;
using Application.Common.Exceptions;
namespace Application.Todos.Commands.UpdateTodo;

public sealed class UpdateTodoCommandHandler(IAppDbContext dbContext) : IRequestHandler<UpdateTodoCommand>
{
    public async Task Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await dbContext.Todos.FindAsync(new object[] { request.Id }, cancellationToken);

        if (todo == null)
        {
            // throw new InvalidOperationException("Todo not found.");
            throw new NotFoundException(nameof(Domain.Todos.Todo), request.Id);
        }

        todo.Title = request.Title;
        todo.IsCompleted = request.IsCompleted;

        await dbContext.SaveChangesAsync(cancellationToken);

    }
}