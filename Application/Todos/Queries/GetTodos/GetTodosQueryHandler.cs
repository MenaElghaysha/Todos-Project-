using Domain.Todos;
using MediatR;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Application.Todos.Queries.GetTodos;

public sealed class GetTodosQueryHandler(IAppDbContext dbContext) : IRequestHandler<GetTodosQuery, List<Todo>>
{
    public Task<List<Todo>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return dbContext.Todos.ToListAsync(cancellationToken);
    }

}