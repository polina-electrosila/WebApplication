using WebApplication.Data;
using WebApplication.Data.Interfaces;

namespace WebApplication.Commands;

public class TestWord : Command
{
    private readonly IDictionaryContext _dictionaryContext;
    
    public TestWord(IDictionaryContext dictionaryContext)
    {
        _dictionaryContext = dictionaryContext;
    }
    
    public async override Task<Response> Run(IReadOnlyList<string> args)
    {
        // Проверяем корректность переданных аргументов
        if (args == null || args.Count < 2)
        {
            throw new ArgumentException();
        }
        
        // Пытаемся преобразовать первый аргумент в индекс
        int id = int.Parse(args[0]);

        // Проверяем, что индекс находится в пределах списка
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        try
        {
            var word = await _dictionaryContext.ReturnWordFromId(id);
            
            // Если пользователь ответил верно,
            if (word.English.Equals(args[1]))
            {
                word.LevelUp(_dictionaryContext);
                await _dictionaryContext.SaveChangesAsync();
                Result.Message = "Совершенно верно!";
            }
            // Иначе если пользователь ошибся
            else
            {
                word.LevelDown(_dictionaryContext);
                await _dictionaryContext.SaveChangesAsync();
                Result.Message = "Неверно, правильный ответ - " + word.English;
            }
        }
        catch (FormatException)
        {
            Result.Message = "Индекс должен быть числом.";
        }
        catch (ArgumentOutOfRangeException e)
        {
            Result.Message = e.Message;
        }
        
        return await Task.FromResult(Result);
    }
}