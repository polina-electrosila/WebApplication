using WebApplication.Data;
using WebApplication.Data.Interfaces;

namespace WebApplication.Commands;

public class RemoveWord : Command
{
    private readonly IDictionaryContext _dictionaryContext;
    
    public RemoveWord(IDictionaryContext dictionaryContext)
    {
        _dictionaryContext = dictionaryContext;
    }
    
    public async override Task<Response> Run(IReadOnlyList<string> args)
    {
        // Проверяем корректность переданных аргументов
        if (args.Any(arg => string.IsNullOrWhiteSpace(arg)))
        {
            Result.Message = "Не указано слово для удаления.";
        }
        try
        {
            if (await _dictionaryContext.RemoveWord(args[0]))
            {
                Result.Message = "Слово успешно удалено";
            }
            else
            {
                Result.Message = "Такого слова нет в словаре";
            }
        }

        catch (ArgumentException e)
        {
            Result.Message = $"Ошибка аргументов: {e.Message}";
        }
        
        return Result;
    }
}