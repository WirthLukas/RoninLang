using RoninLang.Compiler.Parsing.Block;
using RoninLang.Core;

namespace RoninLang.Compiler.Parsing
{
    public class RoninParser : Parser
    {
        protected override void ParseSpecificPart()
        {
            // fun
            ParseSymbol(Symbol.Fun);
            
            // main
            int main = ParseIdentifier();
            string clearName = LastParsedToken.ClearName;
            
            // TODO Check for main
            
            // parenthesis
            ParseSymbol(Symbol.LPar);
            ParseSymbol(Symbol.RPar);
            
            // ToDo: TypeDef Parser

            // Block Parser
            // {} or =>
            var block = Parser.Factory.Create<BlockParser>();
            ParseSymbol(block);
        }
    }
}