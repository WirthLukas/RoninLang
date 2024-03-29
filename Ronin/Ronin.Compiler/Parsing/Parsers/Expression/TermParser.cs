﻿using Ronin.Compiler.Parsing.AST;
using Ronin.Core;

namespace Ronin.Compiler.Parsing.Parsers.Expression;

public class TermParser : Parser
{
    public override TokenNode Parse()
    {
        var left = ParseSymbol<FactorParser>();
        var currentSymbol = (Symbol) CurrentToken.Symbol;

        while (currentSymbol == Symbol.Mul || currentSymbol == Symbol.Div)
        {
            ParseAlternatives(Symbol.Mul, Symbol.Div);
            // should be true, because scanner had the token
            var operatorToken = LastParsedToken;
            var right = ParseSymbol<FactorParser>();
            left = new BinOpNode(operatorToken, left, right);
            currentSymbol = (Symbol) CurrentToken.Symbol;
        }

        return left;
    }
}
