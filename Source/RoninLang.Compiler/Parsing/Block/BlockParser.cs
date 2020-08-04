using RoninLang.Compiler.ErrorHandling;
using RoninLang.Core;
using RoninLang.Core.ErrorHandling;

namespace RoninLang.Compiler.Parsing.Block
{
    public class BlockParser : Parser
    {
        protected override void ParseSpecificPart()
        {
            Parser blockParser = LastParsedToken.Symbol switch
            {
                Symbol.LBracket => Parser.Factory.Create<BracketsBlockParser>(),
                Symbol.Arrow => Parser.Factory.Create<ArrowBlockParser>(),
                _ => null
            };

            if (blockParser == null)
            {
                ServiceManager.Instance
                    .GetService<IErrorHandler>()
                    .ThrowGeneralSyntaxError("expected Block Opening ('{' or => Symbol)");
            }
            else
            {
                ParseSymbol(blockParser);
            }
        }
    }
}