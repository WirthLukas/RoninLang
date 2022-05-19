using Ronin.Core;

namespace Ronin.Compiler.Parsing.AST;

public class WhileNode : TokenNode
{
    public TokenNode Condition { get; }
    public TokenNode Block { get; }
    public WhileNode.Type WhileType { get; }

    public WhileNode(Token whileToken, TokenNode condition, TokenNode block)
        : base(whileToken)
    {
        WhileType = whileToken.Symbol == (uint)Symbol.While ? Type.While : Type.DoWhile;
        Condition = condition;
        Block = block;
    }

    public override string ToString() => $"{{ {WhileType}:{Condition}:{Block} }}";

    public enum Type: byte { While, DoWhile }
}
