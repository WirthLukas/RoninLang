using Ronin.Compiler.Parsing.AST;
using Ronin.Compiler.Parsing.Parsers.Expression;
using Ronin.Compiler.Parsing.Parsers.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        else
        {
            result = ParseSymbol(new ExpressionParser());
            ParseSymbol(Symbol.Semicolon);
        }

        return result;
    }
}
