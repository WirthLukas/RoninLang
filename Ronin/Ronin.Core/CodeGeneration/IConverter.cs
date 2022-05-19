namespace Ronin.Core.CodeGeneration;

public interface IConverter<out T> where T : TokenNode
{
    T Node { get; }

    string Convert(ConverterContext context);
}
