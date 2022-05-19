using Ronin.Compiler;
using Ronin.Compiler.Parsing.AST;
using Ronin.Core.CodeGeneration;
using System;

namespace Ronin.Repl.JS;

internal class UnaryOpConverter : Converter<UnaryOpNode>
{
    public UnaryOpConverter(UnaryOpNode node, GetConverterFunc getConverterForFunc) : base(node, getConverterForFunc)
    {
    }

    public override string Convert(ConverterContext context)
    {
        char op = (Symbol) Node.Token.Symbol switch
        {
            Symbol.Plus => '+',
            Symbol.Minus => '-',
            Symbol.Not => '!',
            _ => throw new NotSupportedException()
        };

        string convertedNode = GetConverterFor(Node.Node).Convert(context);

        return $"{op}{convertedNode}";
    }
}
