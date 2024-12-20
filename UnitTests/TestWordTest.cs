using Moq;
using WebVocabulary.Commands;
using WebVocabulary.Data;
using WebVocabulary.Data.Interfaces;
using Xunit;

namespace UnitTests;

public class TestWordTest
{
    [Fact]
    public async Task TestWord_ReturnsSuccessMessage_AnswerIsCorrect()
    {
        var mockContext = new Mock<IDictionaryContext>();
        
        var word1 = new Word("слово1", "word1",0);
        var word2 = new Word("слово2", "word2", 3);
        var word3 = new Word("слово3", "word3", 2);

        mockContext.Setup(w => w.ReturnAllWords()).ReturnsAsync(new List<Word> {word1, word2, word3});
        mockContext.Setup(w => w.Count()).ReturnsAsync(3);
        mockContext.Setup(w => w.ReturnWordFromId(1)).ReturnsAsync(word2);
        mockContext.Setup(w => w.ReturnWordFromId(2)).ReturnsAsync(word3);
        
        var testWord = new TestWord(mockContext.Object);

        // Проверка результата
        Assert.Equal("Совершенно верно!", (await testWord.Run(new List<string> {"1", "word2"})).Message);
        Assert.Equal(3, word2.UserLevel);
        
        Assert.Equal("Совершенно верно!", (await testWord.Run(new List<string> {"2", "word3"})).Message);
        Assert.Equal(3, word3.UserLevel);
        
        Assert.Null((await testWord.Run(new List<string> {"2", "word3"})).Data);
    }
    
    [Fact]
    public async Task TestWord_ReturnsSuccessMessage_AnswerIsIncorrect()
    {
        var mockContext = new Mock<IDictionaryContext>();
        
        var word1 = new Word("слово1", "word1",0);
        var word2 = new Word("слово2", "word2", 3);
        var word3 = new Word("слово3", "word3", 3);

        mockContext.Setup(w => w.ReturnAllWords()).ReturnsAsync(new List<Word> {word1, word2, word3});
        mockContext.Setup(w => w.Count()).ReturnsAsync(3);
        mockContext.Setup(w => w.ReturnWordFromId(1)).ReturnsAsync(word2);
        mockContext.Setup(w => w.ReturnWordFromId(0)).ReturnsAsync(word1);
        
        var testWord = new TestWord(mockContext.Object);
        
        // Проверка результата
        Assert.Equal("Неверно, правильный ответ - word2", (await testWord.Run(new List<string> { "1", "word" })).Message);
        Assert.Equal(2, word2.UserLevel);
        
        Assert.Equal("Неверно, правильный ответ - word1", (await testWord.Run(new List<string> { "0", "word" })).Message);
        Assert.Equal(0, word1.UserLevel);
        
        Assert.Null((await testWord.Run(new List<string> { "0", "word" })).Data);
    }
    
    [Fact]
    public async Task TestWord_ThrowsException_ArgumentsAreInvalid()
    {
        var mockContext = new Mock<IDictionaryContext>();
        
        var word1 = new Word("слово1", "word1",0);
        var word2 = new Word("слово2", "word2", 3);
        var word3 = new Word("слово3", "word3", 3);

        mockContext.Setup(w => w.ReturnAllWords()).ReturnsAsync(new List<Word> {word1, word2, word3});
        mockContext.Setup(w => w.Count()).ReturnsAsync(3);
        
        var testWord = new TestWord(mockContext.Object);

        // Проверка результата
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => testWord.Run(new List<string> {"-1", "word2"}));
        await Assert.ThrowsAsync<ArgumentException>(() => testWord.Run(new List<string> {"1"}));
        await Assert.ThrowsAsync<ArgumentException>(() => testWord.Run(new List<string> {"a"}));
    }
    
}