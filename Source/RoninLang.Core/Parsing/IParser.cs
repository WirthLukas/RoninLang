namespace RoninLang.Core.Parsing
{
    public interface IParser
    {
        bool ParsingSuccessfulUntilNow { get; }

        bool Parse();
    }
}