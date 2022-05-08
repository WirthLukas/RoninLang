using System;
using System.Collections.Generic;
using System.Linq;

namespace Ronin.Core.ErrorHandling
{
    public class ErrorHandler : IErrorHandler
    {
        private readonly List<IError> _errors = new ();
        private readonly ISourceReader _sourceReader;

        public int Count => _errors.Count;
        public IEnumerable<IError> AllErrors => _errors.ToArray();
        public IError LastError => _errors.Last();

        public ErrorHandler(ISourceReader sourceReader) => _sourceReader = sourceReader;

        public int GetCountFor(ErrorClass errorClass) => _errors.Count(e => e.ErrorClass == errorClass);

        public void Raise(IError e)
        {
            e.LineNumber = _sourceReader.CurrentLine;
            e.Column = _sourceReader.CurrentCol;
            _errors.Add(e);
            Console.Error.WriteLine($"{e.ErrorClass} in line {e.LineNumber} at {e.Column} : {e.Message}");
        }
    }
}
