using WebApplication.Data;

namespace WebApplication.Manager.Interfaces;

public interface ICommandManager
{
    public Task<Response> Execute(CommandName command);
    public Task<Response> Execute(CommandName command, List<string> args);
}