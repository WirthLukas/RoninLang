using System.Collections.Generic;

namespace Ronin.Core.ErrorHandling
{
    public interface IErrorHandler
    {
        int Count { get; }

        IEnumerable<IError> AllErrors { get; }
        IError LastError { get; }

        int GetCountFor(ErrorClass errorClass);
        void Raise(IError e);
    }
}
