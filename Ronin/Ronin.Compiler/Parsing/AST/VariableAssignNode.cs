using Ronin.Core;

namespace Ronin.Compiler.Parsing.AST;

public class VariableAssignNode : TokenNode
{
    public TokenNode Value;
    // Identifiy if var or val

    public VariableAssignNode(Token identifier, TokenNode value) : base(identifier)
    {
        Value = value;
    }

    public override string ToString() => $"{{Variable:{Token.Text} = {Value}}}";
}
