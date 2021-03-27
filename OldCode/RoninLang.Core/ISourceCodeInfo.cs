namespace RoninLang.Core
{
    public interface ISourceCodeInfo
    {
        int CurrentCol { get; }
        int CurrentLine { get; }
    }
}