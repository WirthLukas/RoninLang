using System;
using System.Runtime.CompilerServices;
using Ronin.Compiler.ErrorHandling;
using Ronin.Compiler.Parsing.AST;
using Ronin.Compiler.Semantics;
using Ronin.Core;
using Ronin.Core.ErrorHandling;

namespace Ronin.Compiler.Parsing
{
    public abstract class Parser : IParser<TokenNode>
    {
        private Token _lastParsedToken;
        protected readonly IScanner Scanner;
        protected readonly IErrorHandler ErrorHandler;
        protected readonly SymbolTable SymbolTable;

        /// <summary>
        /// true if parse() was not called yet or parsing was successful, false otherwise.
        /// </summary>
        public bool ParsingSuccessfulUntilNow { get; protected set; } = true;

        protected ref readonly Token LastParsedToken => ref _lastParsedToken;
        protected ref readonly Token CurrentToken => ref Scanner.CurrentToken;

        protected Parser()
        {
            
            var serviceManager = ServiceManager.Instance;
            Scanner = serviceManager.GetService<IScanner>() ?? throw new AccessViolationException("No IScanner Service Registered");
            ErrorHandler = serviceManager.GetService<IErrorHandler>() ?? throw new AccessViolationException("No IErrorHandler Service Registered");
            SymbolTable = serviceManager.GetService<SymbolTable>() ?? throw new AccessViolationException("No SymbolTable Service Registered");
        }

        public abstract TokenNode Parse();

        /// <summary>
        /// Parses a terminal symbol
        /// </summary>
        /// <param name="symbol">Symbol to be parsed</param>
        protected Token ParseSymbol(Symbol symbol, Action<IErrorHandler>? raiseErrorAction = null)
        {
            if (ParsingSuccessfulUntilNow)
            {
                _lastParsedToken = Scanner.CurrentToken;
                ParsingSuccessfulUntilNow = _lastParsedToken.Symbol == (uint) symbol;

                if (!ParsingSuccessfulUntilNow)
                {
                    if (raiseErrorAction is not null)
                    {
                        raiseErrorAction(ErrorHandler);
                    }
                    else
                    {
                        ErrorHandler.ThrowSymbolExpectedError(
                            symbol.ToString(),
                            Token.SymbolConverter(_lastParsedToken.Symbol));
                    }
                }
            }

            Scanner.NextToken();
            return _lastParsedToken;
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
        /// Parses a non-terminal symbol represented by an extra parser.
        /// This Parser is specified through the type param and the instance will be created
        /// automatically
        /// </summary>
        /// <typeparam name="T">Type of the parser</typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected TokenNode ParseSymbol<T>() where T : Parser, new() => ParseSymbol(new T());

        /// <summary>
        /// Parses a number.
        /// </summary>
        /// <returns>The value of the parsed number</returns>
        protected int ParseNumber()
        {
            ParseSymbol(Symbol.Number);
            return _lastParsedToken.Value ?? 0;
        }

        protected Token ParseAlternatives(Symbol firstOption, params Symbol[] furtherOptions)
        {
            if (ParsingSuccessfulUntilNow)
            {
                _lastParsedToken = Scanner.CurrentToken;
                var currentSymbol = (Symbol) _lastParsedToken.Symbol;
                bool symbolFound = currentSymbol == firstOption;

                foreach (var option in furtherOptions)
                {
                    symbolFound = symbolFound || currentSymbol == option;
                }

                ParsingSuccessfulUntilNow = symbolFound;

                if (!symbolFound)
                {
                    // TODO Optimize Smybol not found
                    ErrorHandler.ThrowSymbolExpectedError(
                            firstOption.ToString(),
                            Token.SymbolConverter(_lastParsedToken.Symbol)
                        );
                }
            }

            Scanner.NextToken();
            return _lastParsedToken;
        }
    }
}
