using System;
using Ronin.Compiler.Parsing.AST;
using Ronin.Core;

namespace Ronin.Compiler.Parsing
{
    public abstract class Parser
    {
        private Token _lastParsedToken;
        protected readonly IScanner Scanner;

        /// <summary>
        /// true if parse() was not called yet or parsing was successful, false otherwise.
        /// </summary>
        public bool ParsingSuccessfulUntilNow { get; protected set; } = true;

        protected Token LastParsedToken => _lastParsedToken;

        protected Parser()
        {
            Scanner = ServiceManager.Instance.GetService<IScanner>() ?? throw new AccessViolationException("No IScanner Service Registered");
        }

        public abstract TokenNode Parse();

        /// <summary>
        /// Parses a terminal symbol
        /// </summary>
        /// <param name="symbol">Symbol to be parsed</param>
        protected void ParseSymbol(Symbol symbol)
        {
            if (ParsingSuccessfulUntilNow)
            {
                _lastParsedToken = Scanner.CurrentToken;
                ParsingSuccessfulUntilNow = _lastParsedToken.Symbol == (uint) symbol;

                if (!ParsingSuccessfulUntilNow)
                {
                    // TODO: ErrorHandling
                }
            }

            Scanner.NextToken();
        }

        /// <summary>
        /// Parses a non-terminal symbol represented by an extra parser.
        /// </summary>
        /// <param name="p">Parser representing the non-terminal symbol.</param>
        protected TokenNode ParseSymbol(Parser p)
        {
            var result = p.Parse();
            ParsingSuccessfulUntilNow = ParsingSuccessfulUntilNow && p.ParsingSuccessfulUntilNow;
            _lastParsedToken = p.LastParsedToken;
            return result;
        }

        /// <summary>
        /// Parses a number.
        /// </summary>
        /// <returns>The value of the parsed number</returns>
        protected int ParseNumber()
        {
            ParseSymbol(Symbol.Number);
            return _lastParsedToken.Value ?? 0;
        }

        protected void ParseAlternatives(Symbol firstOption, params Symbol[] furtherOptions)
        {
            if (ParsingSuccessfulUntilNow)
            {
                _lastParsedToken = Scanner.CurrentToken;
                var currentSymbol = _lastParsedToken.Symbol;
                bool symbolFound = currentSymbol == (uint) firstOption;

                foreach (var option in furtherOptions)
                    symbolFound = symbolFound || currentSymbol == (uint) option;

                ParsingSuccessfulUntilNow = symbolFound;

                if (!symbolFound)
                {
                    // TODO Optimize Smybol not found
                    // ErrorHandler.ThrowSymbolExpectedError(firstOption, _lastParsedToken.Symbol);
                }
            }

            Scanner.NextToken();
        }
    }
}
