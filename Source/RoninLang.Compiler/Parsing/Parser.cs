using System;
using RoninLang.Compiler.ErrorHandling;
using RoninLang.Core;
using RoninLang.Core.ErrorHandling;
using RoninLang.Core.Parsing;
using RoninLang.Core.Scanning;

#nullable enable

namespace RoninLang.Compiler.Parsing
{
    public abstract partial class Parser : IParser
    {
        private readonly IScanner _scanner;
        private Token _lastParsedToken;
        
        /// <summary>
        /// true if parse() was not called yet or parsing was successful, false otherwise.
        /// </summary>
        public bool ParsingSuccessfulUntilNow { get; protected set; } = true;

        protected IScanner Scanner => _scanner;
        protected Token LastParsedToken => _lastParsedToken;

        protected Parser()
        {
            _scanner = Factory.Scanner ?? throw new AccessViolationException("ParserFactory not initialized!");
        }

        public bool Parse()
        {
            ParsingSuccessfulUntilNow = true;
            ParseSpecificPart();
            return ParsingSuccessfulUntilNow;
        }

        protected abstract void ParseSpecificPart();

        /// <summary>
        /// Parses a terminal symbol
        /// </summary>
        /// <param name="symbol">Symbol to be parsed</param>
        protected void ParseSymbol(Symbol symbol)
        {
            if (ParsingSuccessfulUntilNow)
            {
                _lastParsedToken = _scanner.CurrentToken;
                ParsingSuccessfulUntilNow = _lastParsedToken.Symbol == symbol;

                if (!ParsingSuccessfulUntilNow)
                {
                    ServiceManager.Instance
                        .GetService<IErrorHandler>()
                        .ThrowSymbolExpectedError(symbol, _lastParsedToken.Symbol);
                }
            }
            
            _scanner.NextToken();
        }

        /// <summary>
        /// Parses a non-terminal symbol represented by an extra parser.
        /// </summary>
        /// <param name="p">Parser representing the non-terminal symbol.</param>
        protected void ParseSymbol(Parser p)
        {
            ParsingSuccessfulUntilNow = ParsingSuccessfulUntilNow && p.Parse();
            _lastParsedToken = p.LastParsedToken;
        }

        /// <summary>
        /// Parses an identifier.
        /// </summary>
        /// <returns>The name (spix) of the identifier.</returns>
        protected int ParseIdentifier()
        {
            ParseSymbol(Symbol.Identifier);
            return _lastParsedToken.Value;    // returning spix
        }

        /// <summary>
        /// Parses a number.
        /// </summary>
        /// <returns>The value of the parsed number</returns>
        protected int ParseNumber()
        {
            ParseSymbol(Symbol.Number);
            return _lastParsedToken.Value;
        }
    }
}