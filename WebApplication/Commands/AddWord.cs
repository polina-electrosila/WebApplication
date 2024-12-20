using WebVocabulary.Data;
using WebVocabulary.Data.Interfaces;

namespace WebVocabulary.Commands;

public class AddWord : Command
{
    private readonly IDictionaryContext _dictionaryContext;
    
    public AddWord(IDictionaryContext dictionaryContext)
    {
        _dictionaryContext = dictionaryContext;
    }
    
    public async override Task<Response> Run(IReadOnlyList<string> args)
    {
        // Проверяем корректность переданных аргументов
        if (args.Any(arg => string.IsNullOrWhiteSpace(arg)))
        {
            Result.Message = "Необходимо передать три аргумента: слово на русском, его перевод и начальный уровень запоминания (от 0 до 3)";
            return Result;
        }
        
        try
        {
            // Вытаскиваем из переданных данных нужные части:
            string wordReal = args[0];
            string wordTranslation = args[1];
            
            if (!int.TryParse(args[2], out int wordLevel) || wordLevel < 0 || wordLevel > 3)
            {
                Result.Message = "Уровень должен быть числом от 0 до 3";
                return Result;
            }
            
            // Добавляем новое слово в словарь
            await _dictionaryContext.AddWord((new Word(wordReal, wordTranslation, wordLevel)));
            Result.Message = "Слово успешно добавлено"; // Записываем обратную связь
        }

        // Обработка исключений
        catch (ArgumentException e) 
        {
            Result.Message = e.Message;
        }

        Result.Message = "Слово успешно добавлено";
        return Result;
    }
}