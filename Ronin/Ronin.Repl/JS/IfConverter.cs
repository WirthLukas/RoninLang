using Ronin.Compiler.Parsing.AST;
using Ronin.Core.CodeGeneration;
using System;
using System.Text;

namespace Ronin.Repl.JS;

internal class IfConverter : Converter<IfNode>
{
    public IfConverter(IfNode node, GetConverterFunc getConverterForFunc) : base(node, getConverterForFunc)
    {
    }

    public override string Convert(ConverterContext context)
    {
        var sb = new StringBuilder();
        bool firstRun = true;

        foreach (var ifCase in Node.Cases)
        {
            if (firstRun)
            {
                firstRun = false;
            }
            else
            {
                sb.Append(" else ");
            }

            string condition = GetConverterFor(ifCase.Condition).Convert(context);
            context.IntendationLevel++;
            string block = GetConverterFor(ifCase.Block).Convert(context);
            context.IntendationLevel--;

            sb.Append($"if ({condition}) ");
            sb.Append(block);
        }

        if (Node.ElseCase is not null)
        {
            sb.Append("else ");
            context.IntendationLevel++;
            string block = GetConverterFor(Node.ElseCase).Convert(context);
            context.IntendationLevel--;
            sb.AppendLine(block);
        }

        return sb.ToString();
    }
}
