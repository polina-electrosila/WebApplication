using Microsoft.AspNetCore.Mvc;
using WebVocabulary.CommandRequests;
using WebVocabulary.Data;
using WebVocabulary.Data.Interfaces;
using WebVocabulary.Manager;
using WebVocabulary.Manager.Interfaces;

namespace WebVocabulary.Controllers;

[ApiController]
[Route("api/[action]")]

public class DictionaryControllers : ControllerBase
{
    private ICommandManager _manager;
    private IDictionaryContext _dictionaryContext;
    
    public DictionaryControllers(IDictionaryContext dictionaryContext, ICommandManager manager)
    {
        _dictionaryContext = dictionaryContext;
        _manager = manager;
    }

    // Добавить слово
    [HttpPost]
    public async Task<ActionResult> AddWord([FromBody] AddWordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.WordToAddRussian) || string.IsNullOrWhiteSpace(request.WordToAddEnglish) ||
            string.IsNullOrWhiteSpace(request.WordToAddUserLevel))
        {
            return BadRequest("Не хватает данных");
        }
        
        return Ok(await _manager.Execute(CommandName.AddWord, new List<string>() { request.WordToAddRussian, request.WordToAddEnglish, request.WordToAddUserLevel }));
    }

    // Удалить слово
    [HttpDelete]
    public async Task<ActionResult> RemoveWord([FromBody] RemoveWordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.WordToRemoveRussian))
        {
            return BadRequest("Не хватает данных");
        }
        
        return Ok(await _manager.Execute(CommandName.RemoveWord, new List<string> { request.WordToRemoveRussian }));
    }

    // Выдать список слов для теста (5 слов - русское написание + id)
    [HttpGet]
    public async Task<ActionResult<Word>> GetTestingWord()
    {
        return Ok(await _manager.Execute(CommandName.GetWord));
    }

    // Протестить слово
    [HttpPost]
    public async Task<ActionResult<Word>> TestWord([FromBody] TestWordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserAnswer))
            return BadRequest();
        
        return Ok(await _manager.Execute(CommandName.TestWord, new List<string>() { request.TestingWordId, request.UserAnswer }));
    }
}