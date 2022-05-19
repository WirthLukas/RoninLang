using Ronin.Compiler.Parsing.AST;
using Ronin.Core;

namespace Ronin.Compiler.Parsing.Parsers.Expression;

public class CompExpressionParser : Parser
{
    public override TokenNode Parse()
    {
        TokenNode node;

        if (CurrentToken.Symbol == (uint)Symbol.Bool)
        {
            Token boolValueToken = ParseSymbol(Symbol.Bool);
            node = new TokenNode(boolValueToken);
        }
        else
        {
            node = ParseSymbol<ArithExpressionParser>();        // left value of operation
            var currentSymbol = (Symbol)CurrentToken.Symbol;

            if (currentSymbol is Symbol.LessThan or Symbol.LTEquals
                              or Symbol.Equals or Symbol.GTEquals
                              or Symbol.GreatherThan or Symbol.NotEquals)
            {
                Token tokenOperator = ParseAlternatives(Symbol.LessThan, Symbol.LTEquals, Symbol.Equals, Symbol.GTEquals,
                                  Symbol.GreatherThan, Symbol.NotEquals);

                var right = ParseSymbol<ArithExpressionParser>();
                node = new BinOpNode(tokenOperator, node, right);
            }
        }
        
        return node;
    }
}
