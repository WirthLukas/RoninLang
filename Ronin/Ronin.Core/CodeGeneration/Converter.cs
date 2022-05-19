using System;

namespace Ronin.Core.CodeGeneration;

public delegate IConverter<TokenNode> GetConverterFunc(TokenNode node);

public abstract class Converter<T> : IConverter<T> where T : TokenNode
{
    public T Node { get; }
    protected GetConverterFunc GetConverterFor { get; }

    public Converter(T node, GetConverterFunc getConverterForFunc)
    {
        Node = node ?? throw new ArgumentNullException(nameof(node));
        GetConverterFor = getConverterForFunc ?? throw new ArgumentNullException(nameof(getConverterForFunc));
    }

    public abstract string Convert(ConverterContext context);
}
