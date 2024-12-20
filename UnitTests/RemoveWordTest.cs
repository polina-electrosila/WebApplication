using Moq;
using WebApplication.Commands;
using WebApplication.Data.Interfaces;
using Xunit;

namespace UnitTests;

public class RemoveWordTest
{
    [Fact]
    public async Task RemoveWord_ReturnsSuccessMessage_WordIsRemoved()
    {
        var mockContext = new Mock<IDictionaryContext>();
        
        var removeWord = new RemoveWord(mockContext.Object);
        
        mockContext.Setup(w => w.RemoveWord(It.IsAny<string>())).ReturnsAsync(true);
        

        
        var response = await removeWord.Run(new List<string> {"слово"});

        // Проверка результата
        Assert.Equal("Слово успешно удалено", response.Message);
        mockContext.Verify(w => w.RemoveWord("слово"), Times.Once);
        Assert.Null((await removeWord.Run(new List<string> { "слово"})).Data);
    }
    
    [Fact]
    public async Task RemoveWord_ReturnsFailureMessage_WordIsNotInWordList()
    {
        var mockContext = new Mock<IDictionaryContext>();
        
        var removeWord = new RemoveWord(mockContext.Object);
        
        mockContext.Setup(w => w.RemoveWord(It.IsAny<string>())).ReturnsAsync(false);
        
        var response = await removeWord.Run(new List<string> {"слово"});

        // Проверка результата
        Assert.Equal("Такого слова нет в словаре", response.Message);
        mockContext.Verify(w => w.RemoveWord("слово"), Times.Once);
    }
}