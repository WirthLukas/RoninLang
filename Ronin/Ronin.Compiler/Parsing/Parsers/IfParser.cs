using Ronin.Compiler.Parsing.AST;
using Ronin.Compiler.Parsing.Parsers.Expression;
using System.Collections.Generic;

namespace Ronin.Compiler.Parsing.Parsers;

public class IfParser : Parser
{
    public override TokenNode Parse()
    {
        ParseSymbol(Symbol.If);
        ParseSymbol(Symbol.LPar);
        var cases = new List<IfNode.Case>();

        var conditionExpr = ParseSymbol<ExpressionParser>();

        ParseSymbol(Symbol.RPar);

        var blockNode = ParseSymbol<BlockParser>();
        cases.Add(new IfNode.Case(conditionExpr, blockNode));

        TokenNode? elseCase = null;

        if ((Symbol) CurrentToken.Symbol == Symbol.Else)
        {
            ParseSymbol(Symbol.Else);
            elseCase = ParseSymbol<BlockParser>();
        }

        return new IfNode(cases, elseCase);
    }
}
