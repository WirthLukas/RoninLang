namespace RoninLang.Core.IO
{
    public interface ISourceReader : ISourceCodeInfo
    {
        char? CurrentChar { get; }
        
        void NextChar();
    }
}