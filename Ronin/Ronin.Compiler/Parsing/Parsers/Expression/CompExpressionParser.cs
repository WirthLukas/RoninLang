using Ronin.Compiler.Parsing.AST;
using Ronin.Core;

namespace Ronin.Compiler.Parsing.Parsers.Expression;

public class CompExpressionParser : Parser
{
    public override TokenNode Parse()
    {
        var left = ParseSymbol<ArithExpressionParser>();
        var currentSymbol = (Symbol) CurrentToken.Symbol;

        if (currentSymbol is Symbol.LessThan or Symbol.LTEquals
                          or Symbol.Equals or Symbol.GTEquals
                          or Symbol.GreatherThan or Symbol.NotEquals)
        {
            Token tokenOperator = ParseAlternatives(Symbol.LessThan, Symbol.LTEquals, Symbol.Equals, Symbol.GTEquals,
                              Symbol.GreatherThan, Symbol.NotEquals);

            var right = ParseSymbol<ArithExpressionParser>();
            left = new BinOpNode(tokenOperator, left, right);
        }

        return left;
    }
}
