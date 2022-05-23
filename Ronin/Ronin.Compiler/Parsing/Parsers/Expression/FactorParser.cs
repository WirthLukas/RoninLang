using Ronin.Compiler.ErrorHandling;
using Ronin.Compiler.Parsing.AST;
using Ronin.Core;

namespace Ronin.Compiler.Parsing.Parsers.Expression
{
    internal class FactorParser : Parser
    {
        public override TokenNode Parse()
        {
            TokenNode? node = null;

            switch ((Symbol) CurrentToken.Symbol)
            {
                case Symbol.Plus:
                case Symbol.Minus:
                    ParseAlternatives(Symbol.Plus, Symbol.Minus);
                    node = new UnaryOpNode(LastParsedToken, ParseSymbol<FactorParser>());
                    break;

                case Symbol.Identifier:
                    node = ParseSymbol<VariableAccessParser>();
                    break;

                case Symbol.Number:
                    /*int val = */ParseNumber();
                    node = new TokenNode(LastParsedToken);
                    break;

                case Symbol.LPar:
                    ParseSymbol(Symbol.LPar);

                    node = CurrentToken.Symbol == (uint)Symbol.Identifier
                        ? ParseSymbol<VariableAssignmentParser>()
                        : ParseSymbol<ExpressionParser>();

                    ParseSymbol(Symbol.RPar);
                    break;

                default:
                    ErrorHandler.ThrowSymbolExpectedError(
                        "number or '('", Token.SymbolConverter(Scanner.CurrentToken.Symbol));
                    ParsingSuccessfulUntilNow = false;
                    break;
            }

            return node ?? new TokenNode(new Token((uint)Symbol.IllegalSy));
        }
    }
}
