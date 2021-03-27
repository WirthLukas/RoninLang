using RoninLang.Compiler.ErrorHandling;
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
            var variableName = LastParsedToken.ClearName;
            
            Semantics(() =>
            {
                bool success = SymbolTable.NewVariable(variableName);

                if (!success)
                {
                    ErrorHandler.ThrowNameAlreadyDefined("variable", variableName, "scope");
                }
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
                ErrorHandler.ThrowGeneralSyntaxError("variable declaration needs either a type definition or an assignment");
                ParsingSuccessfulUntilNow = false;
            }
            
            ParseSymbol(Symbol.Semicolon);
        }
    }
}