using Ronin.Compiler.Parsing.AST;
using Ronin.Compiler.Parsing.Parsers.Expression;
using Ronin.Core;

namespace Ronin.Compiler.Parsing.Parsers;

public class DoWhileParser : Parser
{
    public override TokenNode Parse()
    {
        var doWhileToken = ParseSymbol(Symbol.Do);
        var blockNode = ParseSymbol<BlockParser>();
        ParseSymbol(Symbol.While);
        ParseSymbol(Symbol.LPar);
        TokenNode expressionNode = ParseSymbol<ExpressionParser>();
        ParseSymbol(Symbol.RPar);
        return new WhileNode(doWhileToken, expressionNode, blockNode);
    }
}
