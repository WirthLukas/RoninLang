using System;
using RoninLang.Compiler.ErrorHandling;
using RoninLang.Core;
using RoninLang.Core.ErrorHandling;
using RoninLang.Core.Parsing;
using RoninLang.Core.Scanning;
using RoninLang.Core.Semantics;

namespace RoninLang.Compiler.Parsing
{
    public abstract partial class Parser : IParser
    {
        private Token _lastParsedToken;
        
        protected readonly IScanner Scanner;
        protected readonly ISymbolTable SymbolTable;
        // protected readonly ICodeGenerator CodeGenerator;

        /// <summary>
        /// true if parse() was not called yet or parsing was successful, false otherwise.
        /// </summary>
        public bool ParsingSuccessfulUntilNow { get; protected set; } = true;
        protected Token LastParsedToken => _lastParsedToken;

        protected Parser()
        {
            Scanner = ServiceManager.Instance.GetService<IScanner>() ?? throw new AccessViolationException("No IScanner Service Registered");
            SymbolTable = ServiceManager.Instance.GetService<ISymbolTable>() ?? throw new AccessViolationException("No ISymbolTable Service Registered");
            // CodeGenerator = ServiceManager.Instance.GetService<ICodeGenerator>() ?? throw new AccessViolationException("No ICodeGenerator Service Registered");
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
                _lastParsedToken = Scanner.CurrentToken;
                ParsingSuccessfulUntilNow = _lastParsedToken.Symbol == symbol;

                if (!ParsingSuccessfulUntilNow)
                {
                    ServiceManager.Instance
                        .GetService<IErrorHandler>()?
                        .ThrowSymbolExpectedError(symbol, _lastParsedToken.Symbol);
                }
            }
            
            Scanner.NextToken();
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

        protected void ParseAlternatives(Symbol firstOption, params Symbol[] furtherOptions)
        {
            if (ParsingSuccessfulUntilNow)
            {
                _lastParsedToken = Scanner.CurrentToken;
                var currentSymbol = _lastParsedToken.Symbol;
                bool symbolFound = currentSymbol == firstOption;

                foreach (var option in furtherOptions)
                    symbolFound = symbolFound || currentSymbol == option;

                ParsingSuccessfulUntilNow = symbolFound;

                if (!symbolFound)
                {
                    // TODO Raise Error
                }
            }
            
            Scanner.NextToken();
        }

        protected void Semantics(Action semanticAction)
        {
            if (ParsingSuccessfulUntilNow)
                semanticAction();
        }
    }
}