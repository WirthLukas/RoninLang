using Ronin.Compiler.Parsing.AST;
using Ronin.Compiler.ErrorHandling;
using Ronin.Core;

namespace Ronin.Compiler.Parsing.Parsers.Expression;

internal class VariableAccessParser : Parser
{
    public override TokenNode Parse()
    {
        Token accessToken = ParseSymbol(Symbol.Identifier);
        //Token accessToken = LastParsedToken;

        if (!SymbolTable.ExistsVariable(accessToken.Text ?? ""))
        {
            ErrorHandler.ThrowNameUndefined(accessToken.Text ?? "");
            ParsingSuccessfulUntilNow = false;
        }

        return new VariableAccessNode(accessToken);
    }
}
