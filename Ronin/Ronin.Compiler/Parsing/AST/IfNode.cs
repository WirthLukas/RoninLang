using Ronin.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ronin.Compiler.Parsing.AST;

public class IfNode : TokenNode
{
    public IEnumerable<Case> Cases { get; }
    public TokenNode? ElseCase { get; }

    public IfNode(IEnumerable<Case> cases, TokenNode? elseCase)
        : base(new Token(symbol: (uint)Symbol.NoSy))
    {
        Cases = cases ?? throw new ArgumentNullException(nameof(cases));
        ElseCase = elseCase;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("{If: [");

        foreach (var @case in Cases)
        {
            sb.AppendLine(@case.ToString() + ",");
        }

        sb.Append("]}");

        return sb.ToString();
    }

    public record Case (TokenNode Condition, TokenNode Block);
}
