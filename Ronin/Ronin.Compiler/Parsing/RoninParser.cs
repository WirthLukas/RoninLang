using Ronin.Compiler.Parsing.AST;
using Ronin.Compiler.Parsing.Parsers;
using Ronin.Compiler.Parsing.Parsers.Expression;
using Ronin.Compiler.Parsing.Parsers.Types;

namespace Ronin.Compiler.Parsing
{
    public class RoninParser : Parser
    {
        public override TokenNode Parse()
        {
            return ParseSymbol<StatementParser>();
        }
    }
}
