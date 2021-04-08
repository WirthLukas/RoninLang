using Ronin.Compiler.Parsing.AST;

namespace Ronin.Compiler.Parsing.Parsers.Expression
{
    internal class FactorParser : Parser
    {
        public override TokenNode Parse()
        {
            TokenNode node = null;

            switch (Scanner.CurrentToken.Symbol)
            {
                case (uint)Symbol.Plus:
                case (uint)Symbol.Minus:
                    ParseAlternatives(Symbol.Plus, Symbol.Minus);
                    node = new UnaryOpNode(LastParsedToken, ParseSymbol(new FactorParser()));
                    break;

                case (uint)Symbol.Number:
                    int val = ParseNumber();
                    node = new TokenNode(LastParsedToken);
                    break;

                case (uint)Symbol.LPar:
                    node = ParseExpression();
                    break;

                default:
                    // TODO: Error Handling
                    break;
            }

            return node;
        }

        private TokenNode ParseExpression()
        {
            ParseSymbol(Symbol.LPar);
            var result = ParseSymbol(new ExpressionParser());
            ParseSymbol(Symbol.RPar);
            return result;
        }
    }
}
