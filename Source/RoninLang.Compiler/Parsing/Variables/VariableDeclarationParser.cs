using RoninLang.Compiler.Parsing.Expressions;
using RoninLang.Core;

namespace RoninLang.Compiler.Parsing.Variables
{
    public class VariableDeclarationParser : Parser
    {
        protected override void ParseSpecificPart()
        {
            ParseAlternatives(Symbol.Var, Symbol.Val);
            int name = ParseIdentifier();
            
            Semantics(() =>
            {
                // validate variable
            });

            if (LastParsedToken.Symbol == Symbol.Colon)
            {
                ParseSymbol(Factory.Create<TypeDefParser>());
            }

            if (LastParsedToken.Symbol == Symbol.Assign)
            {
                ParseSymbol(Symbol.Assign);
                ParseSymbol(Factory.Create<ExpressionParser>());
            }
            else
            {
                // no type definition nor an assignment => raise error
                // TODO Raise an Error

                ParsingSuccessfulUntilNow = false;
            }
            
            ParseSymbol(Symbol.Semicolon);
        }
    }
}