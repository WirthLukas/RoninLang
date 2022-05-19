using System.Collections.Generic;

namespace Ronin.Compiler;

internal static class KeywordTable
{
    private static readonly Dictionary<string, Symbol> Keywords = new()
    {
        { "var", Symbol.Var },
        { "if", Symbol.If },
        { "else", Symbol.Else},
        { "while", Symbol.While},
        { "do", Symbol.Do},
        { "true", Symbol.True },
        { "false", Symbol.False },
    };

    internal static Symbol GetSymbolOf(string identifier) => Keywords.ContainsKey(identifier) ? Keywords[identifier] : Symbol.Identifier;
}
