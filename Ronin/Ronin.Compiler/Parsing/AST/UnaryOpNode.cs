using Ronin.Core;

namespace Ronin.Compiler.Parsing.AST
{
    public class UnaryOpNode : TokenNode
    {
        public TokenNode Node { get; }

        public UnaryOpNode(Token op, TokenNode node) : base(op)
        {
            Node = node;
        }

        public override string ToString() => $"{{{Token}, {Node}}}";
    }
}
