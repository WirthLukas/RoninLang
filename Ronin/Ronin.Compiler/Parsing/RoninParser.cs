using Ronin.Compiler.Parsing.AST;
using Ronin.Compiler.Parsing.Parsers.Expression;
using Ronin.Compiler.Parsing.Parsers.Types;

namespace Ronin.Compiler.Parsing
{
    public class RoninParser : Parser
    {
        public override TokenNode Parse()
        {
            if ((Symbol) CurrentToken.Symbol == Symbol.Var)
            {
                return ParseSymbol<VariableDeclarationParser>();
            }

            return ParseSymbol(new ExpressionParser());
        }
    }
}
