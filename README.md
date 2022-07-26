# minimal-api-todo
### Apresentacao do chapter 26/07/2022 👋
-Chapter: .Net Core
Pessoa: Rafael Vieira Suarez
Pauta: .NET 6 ASP NET Minimal API's
Descrição:
- Novo template do ASP NET web
- Visual Studio 2022
- Entity Framework Core
- Configuração de contexto
- SQLite
- Trabalhando com Migration
- ViewModels
- Validações
- Documentação com Swagger

Obs.: 30 min de prática, 10 min de comentários.
<hr>
1. Abrir terminal (Novo template)
   dotnet --version
   Criar projeto via comando
   dotnet new web -o CompassTodo

2. Criar pasta Models (Models)

3. Criar record Todo na pasta models
   public record Todo(Guid Id, string Title, bool Done);

4. Implementar chamada no arquivo Program.cs
```c#
    app.MapGet("/", () => {
      var todo = new Todo(Guid.NewGuid(), "ir a academia", false);
      return Results.Ok(todo);
    });
 ```
5. Adicionar pacote do EF Core (EntityFrameworkCore)
   dotnet add package Microsoft.EntityFrameworkCore.SqLite
   dotnet add package Microsoft.EntityFrameworkCore.Design
   
6. Criar pasta Data
7. Criar classe AppDbContext
```c#
    public class AppDbContext : DbContext
    {   public DbSet<Todo> Todos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=app.db;Cache=Shared");
    }
```
8. No Program.cs adicione  (Configurando o DbContext)
```c#
   builder.Services.AddDbContext<AppDbContext>();
```
9. Modificar o MapGet para passar a instancia do context
```c#
   app.MapGet("v1/todos", (AppDbContext context) => {
      var todo = context.Todos.ToList();
      return Results.Ok(todo);
   });
```
10. dotnet tool install --global dotnet-ef 
10. dotnet ef migrations add InitialMigration (Trabalhando com as migrações)
11. dotnet ef database update
12. dotnet add package Flunt(ViewModels)
13. Criar a pasta ViewModels Criar a classe CreateTodoViewModel
```c#
    public class CreateTodoViewModel : Notifiable<Notification>
    {  public string Title { get; set; }
       public Todo MapTo()
	   {     AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(Title, "Informe o título da tarefa")
                .IsGreaterThan(Title, 5, "O título deve conter mais de 5 caracteres."));

            return new Todo(Guid.NewGuid(), Title, false);
        }
    }
```
14. No Program.cs criar metodo mapPost
```c#
app.MapPost("v1/todos",(
    AppDbContext context,
    CreateTodoViewModel model) =>
{
    var todo = model.MapTo();
    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    context.Todos.Add(todo);
    context.SaveChanges();
    return Results.Created($"/v1/todos/{todo.Id}", todo);
});
```
15. Rodar o app , criar no postman.
16. dotnet add package Swashbuckle.AspNetCore (Documentação com Swagger)
17 Adicione no Program.cs
```c#
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
```
<hr>
