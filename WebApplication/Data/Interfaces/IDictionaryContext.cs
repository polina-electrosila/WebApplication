namespace WebVocabulary.Data.Interfaces;

public interface IDictionaryContext {
    Task<int> Count();
    Task<List<Word>> OrderedByUserLevelList();
    Task AddWord(Word word);
    Task<bool> RemoveWord(string word);
    Task<Word?> ReturnWordFromId(int id);
    Task<List<Word>> ReturnAllWords();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}