namespace RoninLang.Core.Scanning
{
    public interface IScanner
    {
        int CurrentLine { get; }
        Token CurrentToken { get; }

        void NextToken();
    }
}