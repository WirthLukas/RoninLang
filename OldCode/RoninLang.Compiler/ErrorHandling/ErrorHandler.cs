using System;
using System.Collections.Generic;
using System.Linq;
using RoninLang.Core;
using RoninLang.Core.ErrorHandling;

namespace RoninLang.Compiler.ErrorHandling
{
    public class ErrorHandler : IErrorHandler
    {
        private readonly List<IError> _errors = new List<IError>();
        private readonly ISourceCodeInfo _sourceCodeInfo;

        public int Count => _errors.Count;
        public IEnumerable<IError> AllErrors => _errors.ToArray();
        public IError LastError => _errors.Last();

        public ErrorHandler(ISourceCodeInfo sourceCodeInfo) => _sourceCodeInfo = sourceCodeInfo;

        public int GetCountFor(ErrorClass errorClass) => _errors.Count(e => e.ErrorClass == errorClass);

        public void Raise(IError e)
        {
            e.LineNumber = _sourceCodeInfo.CurrentLine;
            _errors.Add(e);
            Console.Error.WriteLine($"{e.ErrorClass}: {e.LineNumber} : {e.Message}");
        }
    }
}