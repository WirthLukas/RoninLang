namespace Ronin.Core
{
    public interface IParser<out TNode>
    {
        bool ParsingSuccessfulUntilNow { get; }

        TNode Parse();
    }
}