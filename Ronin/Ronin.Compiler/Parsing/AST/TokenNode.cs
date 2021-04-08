using Ronin.Core;

namespace Ronin.Compiler.Parsing.AST
{
    public class TokenNode
    {
        public Token Token { get; }

        public TokenNode(Token token)
        {
            Token = token;
        }

        public override string ToString() => $"{{{Token}}}";
    }
}
