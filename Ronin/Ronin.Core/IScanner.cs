
namespace Ronin.Core
{
    public interface IScanner
    {
        ref readonly Token CurrentToken { get; }

        ref readonly Token NextToken();
    }
}
