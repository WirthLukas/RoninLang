namespace Ronin.Core;

public interface ISourceReader : ISourcePeeker, ISourcePosition
{
    char? NextChar();
}

public interface ISourcePeeker : ISourcePosition
{
    char? CurrentChar { get; }

    char? PeekNextChar();
    char[] PeekNextChars(int amount);
}

public interface ISourcePosition
{
    int CurrentCol { get; }
    int CurrentLine { get; }
}