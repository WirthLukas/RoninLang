using Ronin.Compiler.Parsing.AST;
using Ronin.Core.CodeGeneration;
using System;

namespace Ronin.Repl.JS;

internal class VariableAccessConverter : Converter<VariableAccessNode>
{
    public VariableAccessConverter(VariableAccessNode node, GetConverterFunc getConverterForFunc) : base(node, getConverterForFunc)
    {
    }

    public override string Convert(ConverterContext context)
    {
        return $"{Node.Token.Text}";
    }
}
