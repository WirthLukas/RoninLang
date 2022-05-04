using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ronin.Compiler;

internal class KeywordTable
{
    private static readonly Dictionary<string, Symbol> Keywords = new()
    {
        { "var", Symbol.Var },
        { "if", Symbol.If },
        { "else", Symbol.Else}
    };

    internal static Symbol GetSymbolOf(string identifier) => Keywords.ContainsKey(identifier) ? Keywords[identifier] : Symbol.Identifier;
}
