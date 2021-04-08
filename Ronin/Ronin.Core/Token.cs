using System;
using System.Text;

namespace Ronin.Core
{
    public readonly struct Token
    {
        public readonly uint Symbol;
        public readonly int? Value; 

        public Token(uint symbol = 0, int? value = null)
        {
            Symbol = symbol;
            Value = value;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(SymbolConverter(Symbol));
            if (Value != null) sb.Append($":{Value}");
            return sb.ToString();
        }

        public static Func<uint, string> SymbolConverter = symbol => symbol.ToString();
    }
}
