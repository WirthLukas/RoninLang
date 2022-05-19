using Ronin.Compiler.Parsing.AST;
using Ronin.Core.CodeGeneration;
using System;

namespace Ronin.Repl.JS;

internal class VariableAssignConverter : Converter<VariableAssignNode>
{
    public VariableAssignConverter(VariableAssignNode node, GetConverterFunc getConverterForFunc) : base(node, getConverterForFunc)
    {
    }

    public override string Convert(ConverterContext context)
    {
        string assignedValue = GetConverterFor(Node.Value).Convert(context);

        return $"let {Node.Token.Text} = {assignedValue}";
    }
}
