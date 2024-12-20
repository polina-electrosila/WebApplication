using WebApplication.Commands;
using WebApplication.Data;
using WebApplication.Data.Interfaces;
using WebApplication.Manager.Interfaces;

namespace WebApplication.Manager;

public class CommandManager : ICommandManager
{
    private readonly Dictionary<CommandName, Command> _commands; // словарь команд
    
    public CommandManager(IDictionaryContext dictionarycontext)
    {
        IDictionaryContext dictionaryContext = dictionarycontext;
        _commands = new Dictionary<CommandName, Command>();
        // Заносим в словарь все возможные команды по схеме "название команды" - "экземпляр класса команды"
        _commands.Add(CommandName.Exit, new Exit());
        _commands.Add(CommandName.RemoveWord, new RemoveWord(dictionaryContext));
        _commands.Add(CommandName.TestWord, new TestWord(dictionaryContext));
        _commands.Add(CommandName.AddWord, new AddWord(dictionaryContext));
        _commands.Add(CommandName.GetWord, new GetWord(dictionarycontext));
    }
    
    public async Task<Response> Execute(CommandName command) // принимает имя команды из списка CommandName и вызывает метод Run
        // соответствующего объекта команды, передавая пустой список аргументов
    {
        return await _commands[command].Run(new List<string>());
    }
    
    public async Task<Response> Execute(CommandName command, List<string> args) // также принимает имя команды,
        // но передает список аргументов для работы команды
    {
        return await _commands[command].Run(args);
    }
}