namespace Domain.Todos;

public class Todo
{
    // Guid = Globally Unique Identifier, used to uniquely identify each Todo item
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public bool IsCompleted { get; set; }
}