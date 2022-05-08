
namespace Ronin.Core.ErrorHandling
{
    public interface IError
    {
        ErrorClass ErrorClass { get; }
        int LineNumber { get; set; }
        int Column { get; set; }
        int Number { get; }
        string Message { get; }
    }
}
