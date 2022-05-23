using Ronin.Core;

namespace Ronin.Compiler.Parsing.AST;

public class VariableAssignNode : TokenNode
{
    public TokenNode Value;
    // Identifiy if var or val
    public bool IncludeVariableCreation;

    public VariableAssignNode(Token identifier, TokenNode value, bool includeVariableCreation) : base(identifier)
    {
        Value = value;
        IncludeVariableCreation = includeVariableCreation;
    }

    public override string ToString() => $"{{Variable:{Token.Text} = {Value}}}";
}
