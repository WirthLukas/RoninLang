using Ronin.Compiler.Parsing.AST;

namespace Ronin.Compiler.Parsing.Parsers.Expression
{
    public class TermParser : Parser
    {
        public override TokenNode Parse()
        {
            var left = ParseSymbol(new FactorParser());
            var currentSymbol = Scanner.CurrentToken.Symbol;

            while (currentSymbol == (uint)Symbol.Mul || currentSymbol == (uint)Symbol.Div)
            {
                ParseAlternatives(Symbol.Mul, Symbol.Div);
                // should be true, because scanner had the token
                var operatorToken = LastParsedToken;
                var right = ParseSymbol(new FactorParser());
                left = new BinOpNode(operatorToken, left, right);
                currentSymbol = Scanner.CurrentToken.Symbol;
            }

            return left;
        }
    }
}
