using Ronin.Core;

namespace Ronin.Compiler.Parsing.AST
{
    public class TokenNode
    {
        private readonly Token _token;

        public ref readonly Token Token => ref _token;

        public TokenNode(Token token)
        {
            _token = token;
        }

        public override string ToString() => $"{{{_token}}}";
    }
}
