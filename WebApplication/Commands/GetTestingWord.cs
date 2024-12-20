using WebVocabulary.Data;
using WebVocabulary.Data.Interfaces;

namespace WebVocabulary.Commands;

public class GetWord : Command
{
    private readonly IDictionaryContext _dictionaryContext;
    
    public GetWord(IDictionaryContext dictionaryContext)
    {
        _dictionaryContext = dictionaryContext;
    }

    public async override Task<Response> Run(IReadOnlyList<string> args)
    { 
        var wordList = await _dictionaryContext.OrderedByUserLevelList();
        if (wordList == null || !wordList.Any())
            throw new InvalidOperationException("База данных пуста.");
        var newWordList = wordList.Take(5);
        
        Result.Data = newWordList.Select(word => new
        {
            Id = word.Id,
            Russian = word.Russian
        }).ToList();
        
        return Result;
    }
}