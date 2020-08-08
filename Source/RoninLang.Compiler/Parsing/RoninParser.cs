
namespace RoninLang.Compiler.Parsing
{
    public class RoninParser : Parser
    {
        protected override void ParseSpecificPart()
        {
            var function = Parser.Factory.Create<FunctionParser>();
            ParseSymbol(function);
        }
    }
}