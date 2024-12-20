using WebApplication.Data;

namespace WebApplication.Commands;

public abstract class Command
{
    public Response Result { get; set; } = new Response(); // обратная связь от команды
    
    public abstract Task<Response> Run(IReadOnlyList<string> args); // основной метод, определяющий ход работы команды
    // IReadOnly - доступ для чтения, но не для внесения изменений - команды не могут самостоятельно менять данные
}