using Moq;
using WebApplication.Commands;
using WebApplication.Data;
using WebApplication.Data.Interfaces;
using Xunit;

namespace UnitTests;

public class AddWordTests
{
    [Fact]
    public async Task AddWord_ReturnsSuccessMessage_WordIsAdded()
    {
        var mockContext = new Mock<IDictionaryContext>();
        var addWord = new AddWord(mockContext.Object);
        
        var response = await addWord.Run(new List<string> { "слово", "word", "2" });

        // Проверка результата
        Assert.Equal("Слово успешно добавлено", response.Message);
        mockContext.Verify(w => w.AddWord(It.Is<Word>(word => word.Russian == "слово" && word.English == "word" && word.UserLevel == 2)), Times.Once);
    }
    
    [Fact]
    public async Task AddWord_ThrowsArgumentException_WordIsInvalid()
    {
        var mockContext = new Mock<IDictionaryContext>();        
        var addWord = new AddWord(mockContext.Object);

        // Проверка результата
        Assert.Equal("Необходимо передать три аргумента: слово на русском, его перевод и начальный уровень запоминания (от 0 до 3)", (await addWord.Run(new List<string> { "слово", " ", "2" })).Message);
        Assert.Equal("Необходимо передать три аргумента: слово на русском, его перевод и начальный уровень запоминания (от 0 до 3)", (await addWord.Run(new List<string> { "слово", "word", "" })).Message);
        Assert.Null((await addWord.Run(new List<string> { "слово", " ", "2" })).Data);
    }
    
    [Fact]
    public async Task AddWord_ThrowsArgumentException_LevelIsInvalid()
    {
        var mockContext = new Mock<IDictionaryContext>();        
        var addWord = new AddWord(mockContext.Object);

        // Проверка результата
        Assert.Equal("Уровень должен быть числом от 0 до 3", (await addWord.Run(new List<string> { "слово", "word", "6" })).Message);
        Assert.Equal("Уровень должен быть числом от 0 до 3", (await addWord.Run(new List<string> { "слово", "word", "-1" })).Message);
    }
    
    
}