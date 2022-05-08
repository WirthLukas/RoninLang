using Ronin.Compiler.Parsing.AST;
using Ronin.Compiler.Parsing.Parsers.Expression;
using System.Collections.Generic;

namespace Ronin.Compiler.Parsing.Parsers;

public class IfParser : Parser
{
    public override TokenNode Parse()
    {
        var cases = new List<IfNode.Case>();
        TokenNode? elseBlock = null;

        cases.Add(ParseIfPart());

        while ((Symbol) CurrentToken.Symbol == Symbol.Else && elseBlock is null)
        {
            ParseSymbol(Symbol.Else);

            if ((Symbol)(CurrentToken.Symbol) == Symbol.If)
            {
                cases.Add(ParseIfPart());
            }
            else
            {
                elseBlock = ParseSymbol<BlockParser>();
            }
        }

        return new IfNode(cases, elseBlock);
    }

    private IfNode.Case ParseIfPart()
    {
        ParseSymbol(Symbol.If);
        ParseSymbol(Symbol.LPar);

        TokenNode conditionExpr = ParseSymbol<ExpressionParser>();
        ParseSymbol(Symbol.RPar);

        TokenNode blockNode = ParseSymbol<BlockParser>();
        return new IfNode.Case(conditionExpr, blockNode);
    }
}
