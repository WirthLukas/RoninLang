using Ronin.Compiler.Parsing.AST;
using System.Collections.Generic;

namespace Ronin.Compiler.Parsing.Parsers;

public class BlockParser : Parser
{
    public override TokenNode Parse()
    {
        ParseSymbol(Symbol.LBracket);
        var statementNodes = new List<TokenNode>();

        while ((Symbol) CurrentToken.Symbol != Symbol.RBracket && (Symbol)CurrentToken.Symbol != Symbol.EofSy)
        {
            statementNodes.Add(ParseSymbol<StatementParser>());
        }

        ParseSymbol(Symbol.RBracket);
        return new BlockNode(statementNodes);
    }
}
