using System;
using Ronin.Core;

namespace Ronin.Compiler.Scanning
{
    public class Scanner : IScanner
    {
        private readonly ISourceReader _sourceReader;
        private Token _currentToken;

        public Token CurrentToken => _currentToken;

        public Scanner(ISourceReader sourceReader)
        {
            _sourceReader = sourceReader ?? throw new ArgumentNullException(nameof(sourceReader));
        }

        public Token NextToken()
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
                    _currentToken = HandleDigit(_sourceReader);
                    done = true;
                }
                else
                {
                    _currentToken = HandleTerminals(_sourceReader);
                }
            } while (!done && _currentToken.Symbol == (int) Symbol.NoSy);

            return _currentToken;
        }

        private static Token HandleDigit(ISourceReader sourceReader) => 
            new Token(symbol: (int)Symbol.Number,
                value: NumberAnalyzer.ReadNumber(sourceReader));

        private static Token HandleTerminals(ISourceReader sourceReader) => 
            new Token((int) TerminalsManager.ReadTerminalSymbol(sourceReader));
    }
}
