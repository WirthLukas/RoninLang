using Ronin.Compiler.Parsing.Parsers.Expression;
using Ronin.Compiler.Parsing.Parsers.Types;
using Ronin.Core;

namespace Ronin.Compiler.Parsing.Parsers;

public class StatementParser : Parser
{
    public override TokenNode Parse()
    {
        TokenNode result;

        if ((Symbol)CurrentToken.Symbol == Symbol.Var)
        {
            result = ParseSymbol<VariableDeclarationParser>();
            ParseSymbol(Symbol.Semicolon);
        }
        else if ((Symbol)CurrentToken.Symbol == Symbol.If)
        {
            result = ParseSymbol<IfParser>();
        }
        else if ((Symbol)CurrentToken.Symbol == Symbol.While)
        {
            result = ParseSymbol<WhileParser>();
        }
        else if ((Symbol)CurrentToken.Symbol == Symbol.Do)
        {
            result = ParseSymbol<DoWhileParser>();
            ParseSymbol(Symbol.Semicolon);
        }
        else
        {
            result = ParseSymbol(new ExpressionParser());
            ParseSymbol(Symbol.Semicolon);
        }

        return result;
    }
}
