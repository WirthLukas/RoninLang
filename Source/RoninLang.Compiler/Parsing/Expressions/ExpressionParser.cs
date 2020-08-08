using RoninLang.Core;

namespace RoninLang.Compiler.Parsing.Expressions
{
    public class ExpressionParser : Parser
    {
        protected override void ParseSpecificPart()
        {
            switch (LastParsedToken.Symbol)
            {
                case Symbol.Identifier:
                    // parse identifier with reference to a variable
                    break;
                case Symbol.Number:
                    int value = ParseNumber();
                    break;
                default:
                    // TODO: raise an error
                    break;
            }
        }
    }
}