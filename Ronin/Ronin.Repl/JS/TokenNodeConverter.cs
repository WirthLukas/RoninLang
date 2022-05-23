using Ronin.Compiler;
using Ronin.Core;
using Ronin.Core.CodeGeneration;
using System;

namespace Ronin.Repl.JS;

internal class TokenNodeConverter : Converter<TokenNode>
{
    public TokenNodeConverter(TokenNode node, GetConverterFunc getConverterForFunc) : base(node, getConverterForFunc)
    {
    }

    public override string Convert(ConverterContext context)
    {
        return (Symbol)Node.Token.Symbol switch
        {
            Symbol.Number => Node.Token.Value.ToString() ?? throw new AccessViolationException("No number available"),
            Symbol.Bool => Node.Token.Text ?? throw new AccessViolationException("No bool text available"),
            _ => throw new NotSupportedException()
        };
    }
}
