using System;
using System.Runtime.CompilerServices;
using Ronin.Core;
using Ronin.Core.ErrorHandling;

namespace Ronin.Compiler.Scanning
{
    public interface IOldScanner
    {
        ref readonly Token CurrentToken { get; }

        ref readonly Token NextToken();
    }

    public class OldScanner : IOldScanner
    {
        private readonly ISourceReader _sourceReader;
        private readonly IErrorHandler _errorHandler;
        private readonly NameManager _nameManager;
        private Token _currentToken;

        public ref readonly Token CurrentToken => ref _currentToken;

        public OldScanner(ISourceReader sourceReader, IErrorHandler errorHandler, NameManager nameManager)
        {
            _sourceReader = sourceReader ?? throw new ArgumentNullException(nameof(sourceReader));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            _nameManager = nameManager ?? throw new ArgumentNullException(nameof(nameManager));
        }

        public ref readonly Token NextToken()
        {
            bool done = false;

            do
            {
                if (_sourceReader.CurrentChar == null)
                {
                    _currentToken = new Token((int)Symbol.EofSy);
                    done = true;
                }
                else if (char.IsDigit(_sourceReader.CurrentChar.Value))
                {
                    _currentToken = HandleDigit(_sourceReader, _errorHandler);
                    done = true;
                }
                else if (_nameManager.IsValidStartOfName(_sourceReader.CurrentChar.Value))
                {
                    _currentToken = _nameManager.ReadName();
                    done = true;
                }
                else
                {
                    _currentToken = HandleTerminals(_sourceReader);
                }
            } while (!done && _currentToken.Symbol == (uint) Symbol.NoSy);

            return ref _currentToken;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Token HandleDigit(ISourceReader sourceReader, IErrorHandler errorHandler) =>
            new Token(
                symbol: (uint) Symbol.Number,
                value: NumberAnalyzer.ReadNumber(sourceReader, errorHandler));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Token HandleTerminals(ISourceReader sourceReader) => new Token((uint) TerminalsManager.ReadTerminalSymbol(sourceReader));
    }
}
