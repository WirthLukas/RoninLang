using Ronin.Core;
using Ronin.Core.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ronin.Compiler.Scanning;

public record SingleCharRule(char Target, Symbol Result) : IScannerRule
{
    public bool Match(ISourcePeeker sourcePeeker) => sourcePeeker.CurrentChar == Target;

    public Token? GetToken(ISourceReader sourceReader)
    {
        var token = new Token((uint) Result);
        sourceReader.NextChar();
        return token;
    }
}

public record MultiCharRule(string Target, Symbol Result) : IScannerRule
{
    public Token? GetToken(ISourceReader sourceReader)
    {
        var token = new Token((uint)Result);
        
        for (int i = 0; i < Target.Length; i++)
            sourceReader.NextChar();

        return token;
    }

    public bool Match(ISourcePeeker sourcePeeker)
    {
        var currentText = new string(sourcePeeker.PeekNextChars(Target.Length));
        return currentText == Target;
    }
}

public class IgnoreCommentRule : IScannerRule
{
    public Token? GetToken(ISourceReader sourceReader)
    {
        // Skip Comment
        return new Token((uint)Symbol.NoSy);
    }

    public bool Match(ISourcePeeker sourcePeeker) => sourcePeeker.CurrentChar == '/' && sourcePeeker.PeekNextChar() == '/';
}

public class ArithmeticRule : IScannerRule
{
    public Token? GetToken(ISourceReader sourceReader)
    {
        Symbol symbol = sourceReader.CurrentChar switch
        {
            '+' => Symbol.Plus,
            '-' => Symbol.Minus,
            '*' => Symbol.Mul,
            '/' => Symbol.Div,
            _ => throw new Exception("Should not be possible")
        };

        sourceReader.NextChar();
        return new Token((uint)symbol);
    }

    public bool Match(ISourcePeeker sourcePeeker) => sourcePeeker.CurrentChar is '+' or '-' or '*' or '/';
}

public class ComparisonRule : IScannerRule
{
    public Token? GetToken(ISourceReader sourceReader)
    {
        var currentChar = sourceReader.CurrentChar;
        var nextChar = sourceReader.PeekNextChar();
        Symbol? symbol = null;

        if (currentChar is '<')
        {
            symbol = Symbol.LessThan;
            sourceReader.NextChar();

            if (nextChar is '=')
            {
                symbol = Symbol.LTEquals;
                sourceReader.NextChar();
            }
        }
        else if (currentChar is '>')
        {
            symbol = Symbol.GreatherThan;
            sourceReader.NextChar();

            if (nextChar is '=')
            {
                symbol = Symbol.GTEquals;
                sourceReader.NextChar();
            }
        }
        else if (currentChar is '!')
        {
            symbol = Symbol.Not;
            sourceReader.NextChar();

            if (nextChar is '=')
            {
                symbol = Symbol.NotEquals;
                sourceReader.NextChar();
            }
        }
        else if (currentChar is '=' && nextChar is '=')
        {
            symbol = Symbol.Equals;
            sourceReader.NextChar();
            sourceReader.NextChar();
        }
        else if (currentChar is '&' && nextChar is '&')
        {
            symbol = Symbol.And;
            sourceReader.NextChar();
            sourceReader.NextChar();
        }
        else if (currentChar is '|' && nextChar is '|')
        {
            symbol = Symbol.Or;
            sourceReader.NextChar();
            sourceReader.NextChar();
        }

        return new Token((uint) (symbol is null ? Symbol.NoSy : symbol));
    }

    public bool Match(ISourcePeeker sourcePeeker)
    {
        var currentChar = sourcePeeker.CurrentChar;
        var nextChar = sourcePeeker.PeekNextChar();

        return currentChar is '<' or '>' or '!'
            || currentChar is '<' or '>' or '!' or '=' && nextChar is '='
            || currentChar is '&' && nextChar is '&'
            || currentChar is '|' && nextChar is '|';
    }
}

public record IdentifierRule(NameManager NameManager) : IScannerRule
{
    public Token? GetToken(ISourceReader sourceReader)
    {
        return NameManager.ReadName();
    }

    public bool Match(ISourcePeeker sourcePeeker)
    {
        return sourcePeeker.CurrentChar.HasValue && NameManager.IsValidStartOfName(sourcePeeker.CurrentChar.Value);
    }
}

public record NumberLiteralRule(IErrorHandler ErrorHandler) : IScannerRule
{
    public Token? GetToken(ISourceReader sourceReader)
    {
        return new Token((uint)Symbol.Number, value: NumberAnalyzer.ReadNumber(sourceReader, ErrorHandler));
    }

    public bool Match(ISourcePeeker sourcePeeker)
    {
        return sourcePeeker.CurrentChar.HasValue && char.IsDigit(sourcePeeker.CurrentChar.Value);
    }
}

public record BoolRule(NameManager NameManager) : IScannerRule
{
    public Token? GetToken(ISourceReader sourceReader)
    {
        Token result = NameManager.ReadName();
        // text contains the value true or false
        var boolToken = new Token((uint)Symbol.Bool, text: result.Text); 
        return boolToken;
    }

    public bool Match(ISourcePeeker sourcePeeker)
    {
        // check if there is keyword true or false
        return NameManager.IsKeyword("true") || NameManager.IsKeyword("false");
    }
}

public record EndOfSourceRule(uint EndOfSourceSymbol = 1) : IScannerRule
{
    public Token? GetToken(ISourceReader sourceReader) => new Token(EndOfSourceSymbol);

    public bool Match(ISourcePeeker sourcePeeker) => sourcePeeker.CurrentChar is null;
}

public record NoMatchFoundRule(uint IllegalSymbol = 0) : IScannerRule
{
    public Token? GetToken(ISourceReader sourceReader)
    {
        sourceReader.NextChar();
        return new Token(IllegalSymbol);
    }

    public bool Match(ISourcePeeker sourcePeeker) => true;
}

public class IgnoreWhitespacesRule : IScannerRule
{
    public IEnumerable<char> WhiteSpaceCharacters { get; }

    public IgnoreWhitespacesRule() : this(null) {}

    public IgnoreWhitespacesRule(IEnumerable<char>? whitespaceCharacters = null)
    {
        WhiteSpaceCharacters = whitespaceCharacters?? new char[] { ' ', '\t', '\n' };
    }

    public Token? GetToken(ISourceReader sourceReader)
    {
        sourceReader.NextChar();
        return null;
    }

    public bool Match(ISourcePeeker sourcePeeker)
    {
        return sourcePeeker.CurrentChar.HasValue
            && WhiteSpaceCharacters.Contains(sourcePeeker.CurrentChar.Value);
    }
}
