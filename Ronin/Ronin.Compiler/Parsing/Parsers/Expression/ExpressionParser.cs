using Ronin.Compiler.Parsing.AST;
using Ronin.Core;

namespace Ronin.Compiler.Parsing.Parsers.Expression
{
    public class ExpressionParser : Parser
    {
        public override TokenNode Parse()
        {
            TokenNode node;

            if ((Symbol) CurrentToken.Symbol == Symbol.Not)
            {
                Token operatorToken = ParseSymbol(Symbol.Not);
                node = new UnaryOpNode(operatorToken, ParseSymbol<CompExpressionParser>());
            }
            else
            {
                node = ParseSymbol<CompExpressionParser>();

                if ((Symbol) CurrentToken.Symbol is Symbol.And or Symbol.Or)
                {
                    Token operatorToken = ParseAlternatives(Symbol.And, Symbol.Or);
                    TokenNode right = ParseSymbol<CompExpressionParser>();
                    node = new BinOpNode(operatorToken, left: node, right: right);
                }
            }

            return node;
        }
    }
}
