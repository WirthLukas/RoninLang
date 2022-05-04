using Ronin.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ronin.Compiler.Parsing.AST;

public class BlockNode : TokenNode
{
    public IEnumerable<TokenNode> Statements { get; }

    public BlockNode(IEnumerable<TokenNode> statements)
        : base(new Token(symbol: (uint) Symbol.NoSy))
    {
        Statements = statements ?? throw new ArgumentNullException(nameof(statements));
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("{Block: [");

        foreach (var statement in Statements)
        {
            sb.AppendLine(statement.ToString() + ",");
        }

        sb.Append("]}");

        return sb.ToString();
    }
}
