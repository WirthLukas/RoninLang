using Ronin.Compiler.Parsing.AST;
using Ronin.Compiler.Parsing.Parsers.Expression;
using Ronin.Compiler.ErrorHandling;
using Ronin.Core;

namespace Ronin.Compiler.Parsing.Parsers.Types;

internal class VariableDeclarationParser : Parser
{
    public override TokenNode Parse()
    {
        ParseSymbol(Symbol.Var);
        // different between var and val
        Token identifierToken = ParseSymbol(Symbol.Identifier);
        //Token identifierToken = LastParsedToken;

        bool variableNotDefined = SymbolTable.AddVariable(identifierToken.Text ?? "");

        if (!variableNotDefined)
        {
            ErrorHandler.ThrowNameAlreadyDefined(identifierToken.Text ?? "");
            ParsingSuccessfulUntilNow = false;
        }

        ParseSymbol(Symbol.Assign);
        TokenNode valueNode = ParseSymbol<ExpressionParser>();
        ParseSymbol(Symbol.Semicolon);
        return new VariableAssignNode(identifierToken, valueNode);
    }
}
