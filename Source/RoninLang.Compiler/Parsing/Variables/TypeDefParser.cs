using RoninLang.Core;

namespace RoninLang.Compiler.Parsing.Variables
{
    public class TypeDefParser : Parser
    {
        protected override void ParseSpecificPart()
        {
            ParseSymbol(Symbol.Colon);
            ParseAlternatives(Symbol.Int);
        }
    }
}