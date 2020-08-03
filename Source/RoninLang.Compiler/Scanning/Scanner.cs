using System;
using RoninLang.Core;
using RoninLang.Core.IO;
using RoninLang.Core.Scanning;

namespace RoninLang.Compiler.Scanning
{
    public class Scanner : IScanner
    {
        private readonly ISourceReader _sourceReader;
        private readonly NameManager _nameManager;
        private Token _currentToken;

        public int CurrentLine => _sourceReader.CurrentLine;
        public Token CurrentToken => _currentToken;

        public Scanner(ISourceReader sourceReader, NameManager nameManager)
        {
            _sourceReader = sourceReader ?? throw new ArgumentNullException(nameof(sourceReader));
            _nameManager = nameManager ?? throw new ArgumentNullException(nameof(nameManager));
        }
        
        public void NextToken()
        {
            bool done = false;

            do
            {
                if (_sourceReader.CurrentChar == null)
                {
                    _currentToken = new Token(Symbol.EofSy);
                    done = true;
                }
                else if (char.IsDigit(_sourceReader.CurrentChar.Value))
                {
                    _currentToken = HandleDigit();
                    done = true;
                }
                else if (_nameManager.IsAValidStartOfName(_sourceReader.CurrentChar.Value))
                {
                    _currentToken = HandleName();
                    done = true;
                }
                else
                {
                    _currentToken = HandleTerminals();
                }
                
            } while (!done && _currentToken.Symbol == Symbol.NoSy);
        }

        private Token HandleDigit() => new Token { Symbol = Symbol.Number, Value = NumberAnalyzer.ReadNumber(_sourceReader) };

        private Token HandleName() => _nameManager.ReadName();

        private Token HandleTerminals() => new Token(TerminalsManager.ReadTerminalSymbol(_sourceReader));
    }
}