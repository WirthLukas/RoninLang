using Ronin.Compiler.Parsing.Parsers;
using Ronin.Core;

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
