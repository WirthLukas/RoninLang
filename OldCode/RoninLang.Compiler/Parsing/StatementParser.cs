using RoninLang.Compiler.Parsing.Variables;
using RoninLang.Core;

namespace RoninLang.Compiler.Parsing
{
    public class StatementParser : Parser
    {
        protected override void ParseSpecificPart()
        {
            switch (LastParsedToken.Symbol)
            {
                case Symbol.Var:
                case Symbol.Val:
                    ParseSymbol(Factory.Create<VariableDeclarationParser>());
                    break;
                
                default:
                    // TODO: throw error
                    break;
            }
        }
    }
}