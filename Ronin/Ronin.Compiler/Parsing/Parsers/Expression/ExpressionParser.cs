using Ronin.Compiler.Parsing.AST;

namespace Ronin.Compiler.Parsing.Parsers.Expression
{
    public class ExpressionParser : Parser
    {
        public override TokenNode Parse()
        {
            return ParseSymbol<AddExpressionParser>();
        }
    }
}
