using Ronin.Compiler.Parsing.AST;
using Ronin.Core.CodeGeneration;
using System.Text;

namespace Ronin.Repl.JS;

internal class WhileLoopConverter : Converter<WhileNode>
{
    public WhileLoopConverter(WhileNode node, GetConverterFunc getConverterForFunc) : base(node, getConverterForFunc)
    {
    }

    public override string Convert(ConverterContext context)
    {
        var sb = new StringBuilder();

        string condition = GetConverterFor(Node.Condition).Convert(context);
        context.IntendationLevel++;
        string block = GetConverterFor(Node.Block).Convert(context);
        context.IntendationLevel--;

        if (Node.WhileType == WhileNode.Type.While)
        {
            sb.Append($"while ({condition}) ");
            sb.Append(block);
            sb.AppendLine();
        }
        else
        {
            sb.Append("do ");
            sb.Append(block);
            sb.AppendLine($"while ({condition});");
        }

        return sb.ToString();
    }
}
