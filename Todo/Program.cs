using Todos.Data;
using Todos.ViewModels;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("v1/todos", (AppDbContext context) => {
    var todo = context.Todos.ToList();
    return Results.Ok(todo);
}).Produces<Todos.Models.Todo>();

app.MapPost("v1/todos", (
    AppDbContext context,
    CreateTodoViewModel model) =>
{
    var todo = model.MapTo();
    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    context.Todos.Add(todo);
    context.SaveChanges();
    return Results.Created($"/v1/todos/{todo.Id}", todo);
}).Produces<Todos.Models.Todo>();

app.Run();
