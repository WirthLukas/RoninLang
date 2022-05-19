using Ronin.Compiler.Parsing.AST;
using Ronin.Compiler.Parsing.Parsers.Expression;
using Ronin.Core;

namespace Ronin.Compiler.Parsing.Parsers;

public class WhileParser : Parser
{
    public override TokenNode Parse()
    {
        Token whileToken = ParseSymbol(Symbol.While);
        ParseSymbol(Symbol.LPar);
        TokenNode expressionNode = ParseSymbol<ExpressionParser>();
        ParseSymbol(Symbol.RPar);
        TokenNode blockNode = ParseSymbol<BlockParser>();

        return new WhileNode(whileToken, expressionNode, blockNode);
    }
}
