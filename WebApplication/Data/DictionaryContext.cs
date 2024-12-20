using Microsoft.EntityFrameworkCore;
using WebVocabulary.Data.Interfaces;

namespace WebVocabulary.Data;

public class DictionaryContext : DbContext, IDictionaryContext
{
    private DbSet<Word> Words { get; set; }
    
    public DictionaryContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=\"C:\\Users\\evrom\\OneDrive\\Desktop\\ИТМО\\программирование\\dataBase.db\"");
        }
    }
    
    public async Task<int> Count() => await Words.CountAsync();

    public async Task<List<Word>> OrderedByUserLevelList()
    {
        return await Words.OrderBy(word => word.UserLevel).ToListAsync();
    }
    
    public async Task AddWord(Word word)
    {
        await Words.AddAsync(word);
        await SaveChangesAsync();
    }

    public async Task<bool> RemoveWord(string word)
    {
        var wordsToRemove = Words.Where(w => w.Russian == word);
        if (await wordsToRemove.AnyAsync())
        {
            Words.RemoveRange(wordsToRemove);
            await SaveChangesAsync();
            return true;
        }
        return false;
    }

    //public bool WordInDataBase(string word) => Words.Any(w => w.Russian == word);
    
    public async Task<Word?> ReturnWordFromId(int id)
    {
        return await Words.FirstOrDefaultAsync(word => word.Id == id);
    }

    public async Task<List<Word>> ReturnAllWords()
    {
        return await Words.ToListAsync();
    }
}