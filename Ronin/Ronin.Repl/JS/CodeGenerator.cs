using Ronin.Compiler.Parsing.AST;
using Ronin.Core;
using Ronin.Core.CodeGeneration;
using System;

namespace Ronin.Repl.JS;

internal class CodeGenerator : IConverter<TokenNode>
{
    public TokenNode Node { get; }

    public CodeGenerator(TokenNode node)
    {
        Node = node ?? throw new ArgumentNullException(nameof(node));
    }

    public IConverter<TokenNode> GetConverterFor(TokenNode node)
    {
        return node switch
        {
            BinOpNode bon => new BinOpConverter(bon, GetConverterFor),
            UnaryOpNode uon => new UnaryOpConverter(uon, GetConverterFor),
            BlockNode bn => new BlockConverter(bn, GetConverterFor),
            IfNode ifn => new IfConverter(ifn, GetConverterFor),
            WhileNode wn => new WhileLoopConverter(wn, GetConverterFor),
            VariableAssignNode van => new VariableAssignConverter(van, GetConverterFor),
            VariableAccessNode van => new VariableAccessConverter(van, GetConverterFor),
            _ => new TokenNodeConverter(node, GetConverterFor),
        };
    }

    public string Convert(ConverterContext context)
    {
        return GetConverterFor(Node).Convert(context);
    }
}
