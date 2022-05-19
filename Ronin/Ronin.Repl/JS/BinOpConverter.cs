using Ronin.Compiler;
using Ronin.Compiler.Parsing.AST;
using Ronin.Core.CodeGeneration;
using System;

namespace Ronin.Repl.JS;

internal class BinOpConverter : Converter<BinOpNode>
{
    public BinOpConverter(BinOpNode node, GetConverterFunc getConverterForFunc) : base(node, getConverterForFunc)
    {
    }

    public override string Convert(ConverterContext context)
    {      
        string op = (Symbol)Node.Token.Symbol switch
        {
            Symbol.Plus => "+",
            Symbol.Minus => "-",
            Symbol.Mul => "*",
            Symbol.Div => "/",
            Symbol.Equals => "===",
            Symbol.LessThan => "<",
            Symbol.GreatherThan => ">",
            Symbol.GTEquals => ">=",
            Symbol.LTEquals => "<=",
            Symbol.NotEquals => "!==",
            Symbol.And => "&&",
            Symbol.Or => "||",
            _ => throw new NotSupportedException()
        };

        string left = GetConverterFor(Node.Left).Convert(context);
        string right = GetConverterFor(Node.Right).Convert(context);

        return $"{left} {op} {right}";
    }
}
