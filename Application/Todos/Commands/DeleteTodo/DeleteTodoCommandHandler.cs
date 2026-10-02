using Application.Common.Interfaces;
using MediatR;
using Application.Common.Exceptions;
namespace Application.Todos.Commands.DeleteTodo;

public sealed class DeleteTodoCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteTodoCommand>
{
    public async Task Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await dbContext.Todos.FindAsync([request.Id], cancellationToken);

        if (todo == null)
        {
            // throw new InvalidOperationException("Todo not found.");
            throw new NotFoundException(nameof(Domain.Todos.Todo), request.Id);
        }

        dbContext.Todos.Remove(todo);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}