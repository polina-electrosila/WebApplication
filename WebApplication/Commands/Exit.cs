using WebVocabulary.Data;

namespace WebVocabulary.Commands;

public class Exit : Command
{
    public async override Task<Response> Run(IReadOnlyList<string> args)
    {
        Environment.Exit(0);
        return Result;
    }
}