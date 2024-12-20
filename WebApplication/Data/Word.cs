using System.ComponentModel.DataAnnotations;
using WebVocabulary.Data.Interfaces;

namespace WebVocabulary.Data;

public class Word
{
    [Key]
    public int Id { get; set; }
    public String Russian { get; private set; } // само слово (на русском)
    public String English { get; private set; } // его перевод (на английском)
    public int UserLevel {get; private set;} // уровень запоминания слова у пользователя
    

    public Word(String russian, String english, int userLevel)
    {
        Russian = russian;
        English = english;
        UserLevel = userLevel;
    }

    public void LevelUp(IDictionaryContext dictionaryContext)
    {
        if (UserLevel < 3) UserLevel++;
    }

    public void LevelDown(IDictionaryContext dictionaryContext)
    {
        if (UserLevel > 0) UserLevel--;
    }
}