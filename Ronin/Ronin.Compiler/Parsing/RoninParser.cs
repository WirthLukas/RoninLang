using Ronin.Compiler.Parsing.AST;
using Ronin.Compiler.Parsing.Parsers.Expression;

namespace Ronin.Compiler.Parsing
{
    public class RoninParser : Parser
    {
        public override TokenNode Parse()
        {
            return ParseSymbol(new ExpressionParser());
        }
    }
}
