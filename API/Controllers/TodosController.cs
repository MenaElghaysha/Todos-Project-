using Microsoft.AspNetCore.Mvc;
using MediatR;
using API.Requests;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new Application.Todos.Queries.GetTodos.GetTodosQuery();
        var todos = await mediator.Send(query);
        return Ok(todos);
    }

    [HttpGet("{todoId:guid}", Name = "GetTodoById")]
    public async Task<IActionResult> Get(Guid todoId)
    {
        var query = new Application.Todos.Queries.GetTodoById.GetTodoByIdQuery(todoId);
        var todo = await mediator.Send(query);
        return todo is null ? NotFound() : Ok(todo);
    }


    [HttpPost]
    public async Task<IActionResult> Post(CreateTodoRequest request)
    {
        var command = new Application.Todos.Commands.CreateTodo.CreateTodoCommand(request.Title);
        var todoId = await mediator.Send(command);
        return CreatedAtRoute("GetTodoById", new { todoId }, null);
    }


    [HttpPut("{todoId:guid}")]
    public async Task<IActionResult> Put(Guid todoId, UpdateTodoRequest request)
    {
        var command = new Application.Todos.Commands.UpdateTodo.UpdateTodoCommand(todoId, request.Title, request.IsCompleted);
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{todoId:guid}")]
    public async Task<IActionResult> Delete(Guid todoId)
    {
        var command = new Application.Todos.Commands.DeleteTodo.DeleteTodoCommand(todoId);
        await mediator.Send(command);
        return NoContent();
    }
}