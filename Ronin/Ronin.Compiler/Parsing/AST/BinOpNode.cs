using Ronin.Core;

namespace Ronin.Compiler.Parsing.AST
{
    public class BinOpNode : TokenNode
    {
        public TokenNode Left { get; }
        public TokenNode Right { get; }

        public BinOpNode(Token op, TokenNode left, TokenNode right) : base(op)
        {
            Left = left;
            Right = right;
        }

        public override string ToString() => $"{{{Left}, {Token}, {Right}}}";
    }
}
