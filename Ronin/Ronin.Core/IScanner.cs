
namespace Ronin.Core
{
    public interface IScanner
    {
        Token CurrentToken { get; }

        Token NextToken();
    }
}
