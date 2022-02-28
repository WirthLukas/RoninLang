using Ronin.Compiler.Parsing.AST;
using Ronin.Core;

namespace Ronin.Compiler.Parsing.Parsers.Expression
{
    public class AddExpressionParser : Parser
    {
        public override TokenNode Parse()
        {
            //var currentSymbol = Scanner.CurrentToken.Symbol;
            //TokenNode left;

            //if (currentSymbol == (uint) Symbol.Plus || currentSymbol == (uint) Symbol.Minus)
            //{
            //    //ParseAlternatives(Symbol.Plus, Symbol.Minus);
            //    ParseSymbol((Symbol)currentSymbol);
            //    left = new UnaryOpNode(LastParsedToken, ParseSymbol(new TermParser()));
            //}
            //else
            //{
            //    left = ParseSymbol(new TermParser());
            //}

            var left = ParseSymbol<TermParser>();
            var currentSymbol = (Symbol) Scanner.CurrentToken.Symbol;

            while (currentSymbol == Symbol.Plus || currentSymbol == Symbol.Minus)
            {
                ParseAlternatives(Symbol.Plus, Symbol.Minus);
                // should be true, because scanner had the token
                var operatorToken = LastParsedToken;
                var right = ParseSymbol<TermParser>();
                left = new BinOpNode(operatorToken, left, right);
                currentSymbol = (Symbol) Scanner.CurrentToken.Symbol;
            }

            return left;
        }
    }
}
