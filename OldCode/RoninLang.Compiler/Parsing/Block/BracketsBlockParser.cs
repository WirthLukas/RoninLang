using RoninLang.Core;

namespace RoninLang.Compiler.Parsing.Block
{
    public class BracketsBlockParser : Parser
    {
        protected override void ParseSpecificPart()
        {
            ParseSymbol(Symbol.LBracket);

            while (ParsingSuccessfulUntilNow && Scanner.CurrentToken.Symbol != Symbol.RBracket && Scanner.CurrentToken.Symbol != Symbol.NoSy)
            {
                ParseSymbol(Parser.Factory.Create<StatementParser>());
            }

            ParseSymbol(Symbol.RBracket);
        }
    }
}