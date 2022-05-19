using Ronin.Compiler.Parsing.AST;
using Ronin.Core.CodeGeneration;
using System;
using System.Text;

namespace Ronin.Repl.JS;

internal class BlockConverter : Converter<BlockNode>
{
    public BlockConverter(BlockNode node, GetConverterFunc getConverterForFunc) : base(node, getConverterForFunc)
    {
    }

    public override string Convert(ConverterContext context)
    {
        var sb = new StringBuilder();
        sb.AppendLine("{");

        foreach (var statement in Node.Statements)
        {
            string convertedStatement = GetConverterFor(statement)
                .Convert(context);

            sb.AppendLine($"{new string('\t', context.IntendationLevel)}{convertedStatement};");
        }

        sb.Append("}");
        return sb.ToString();
    }
}
