using Ronin.Compiler.Parsing.AST;
using Ronin.Core;

namespace Ronin.Compiler.Parsing.Parsers.Expression
{
    public class AddExpressionParser : Parser
    {
        public override TokenNode Parse()
        {
            var left = ParseSymbol(new TermParser());
            var currentSymbol = Scanner.CurrentToken.Symbol;

            while (currentSymbol == (uint) Symbol.Plus || currentSymbol == (uint) Symbol.Minus)
            {
                ParseAlternatives(Symbol.Plus, Symbol.Minus);
                // should be true, because scanner had the token
                var operatorToken = LastParsedToken;
                var right = ParseSymbol(new TermParser());
                left = new BinOpNode(operatorToken, left, right);
                currentSymbol = Scanner.CurrentToken.Symbol;
            }

            return left;
        }
    }
}
