using Domain.Todos;
using MediatR;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Todos.Queries.GetTodoById;

public sealed class GetTodoByIdQueryHandler(IAppDbContext dbContext) : IRequestHandler<GetTodoByIdQuery, Todo?>
{
    public Task<Todo?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        return dbContext.Todos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
    }

}