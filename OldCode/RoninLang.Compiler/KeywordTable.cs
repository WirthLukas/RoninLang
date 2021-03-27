using System.Collections.Generic;
using RoninLang.Core;

namespace RoninLang.Compiler
{
    public static class KeywordTable
    {
        private static readonly Dictionary<string, Symbol> KeywordsToSymbolMap = new Dictionary<string, Symbol>
        {
            { "fun", Symbol.Fun },
            { "int", Symbol.Int },
            { "var", Symbol.Var },
            { "val", Symbol.Val }
        };

        public static Symbol GetSymbolOf(string keyword)
        {
            return KeywordsToSymbolMap.ContainsKey(keyword)
                ? KeywordsToSymbolMap[keyword]
                : Symbol.NoSy;
        }
    }
}