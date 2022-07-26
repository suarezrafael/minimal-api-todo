using Flunt.Notifications;
using Flunt.Validations;

namespace CompassTodo.ViewModels
{
    public class CreateTodoViewModel : Notifiable<Notification>
    {
        public string Title { get; set; }

        public Todo MapTo()
        {
            // validação
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(Title, "Informe o título da tarefa"));

            return new Todo(Guid.NewGuid(), Title, false);
        }
    }
}
