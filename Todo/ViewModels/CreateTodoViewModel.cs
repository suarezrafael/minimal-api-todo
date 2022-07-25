using Flunt.Notifications;
using Flunt.Validations;
namespace Todos.ViewModels
{
    public class CreateTodoViewModel : Notifiable<Notification>
    {
        public string Title { get; set; }
        public Models.Todo MapTo()
        {
            AddNotifications(new Contract<Notification>()
                 .Requires()
                 .IsNotNull(Title, "Informe o título da tarefa")
                 .IsGreaterThan(Title, 5, "O título deve conter mais de 5 caracteres."));

            return new Models.Todo(Guid.NewGuid(), Title, false);
        }

    }
}
