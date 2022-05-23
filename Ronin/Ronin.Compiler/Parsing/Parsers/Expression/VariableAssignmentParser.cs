using Ronin.Compiler.Parsing.AST;
using Ronin.Compiler.ErrorHandling;
using Ronin.Core;

namespace Ronin.Compiler.Parsing.Parsers.Expression;

internal class VariableAssignmentParser : Parser
{
    public override TokenNode Parse()
    {
        TokenNode variableNode = ParseSymbol<VariableAccessParser>();

        if (SymbolTable.ExistsVariable(variableNode.Token.Text ?? throw new System.Exception("There is no identifier in IdentifierToken")))
        {
            ErrorHandler.ThrowNameUndefined(variableNode.Token.Text);
        }

        ParseSymbol(Symbol.Assign);
        TokenNode expressionNode = ParseSymbol<ExpressionParser>();
        return new VariableAssignNode(
            identifier: variableNode.Token,
            value: expressionNode,
            includeVariableCreation: false);
    }
}
