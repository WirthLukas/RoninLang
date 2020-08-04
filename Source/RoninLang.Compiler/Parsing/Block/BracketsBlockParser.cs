using RoninLang.Core;

namespace RoninLang.Compiler.Parsing.Block
{
    public class BracketsBlockParser : Parser
    {
        protected override void ParseSpecificPart()
        {
            ParseSymbol(Symbol.LBracket);
            
            // TODO Inside Block

            ParseSymbol(Symbol.RBracket);
        }
    }
}