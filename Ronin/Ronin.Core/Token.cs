using System;
using System.Text;

namespace Ronin.Core
{
    // https://www.developerfusion.com/article/84519/mastering-structs-in-c/
    // https://blog.submain.com/c_struct/
    // https://getyourbitstogether.com/structs/
    // https://docs.microsoft.com/en-us/dotnet/csharp/write-safe-efficient-code
    public readonly struct Token : IEquatable<Token>
    {
        public static Func<uint, string> SymbolConverter = symbol => symbol.ToString();

        public readonly uint Symbol;
        public readonly int? Value;
        public readonly string? Text;

        public Token(uint symbol = 0, int? value = null, string? text = null)
        {
            Symbol = symbol;
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(SymbolConverter(Symbol));
            if (Value is not null) sb.Append($":{Value}");
            if (Text is not null) sb.Append($":{Text}");
            return sb.ToString();
        }

        #region <<Equality>>

        /// <inheritdoc />
        public bool Equals(Token other)
            => Symbol == other.Symbol && Value == other.Value && Text == other.Text;

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is Token other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(Symbol, Value, Text);

        #endregion

        #region <<Operators>>

        public static bool operator ==(Token token1, Token token2) => token1.Equals(token2);

        public static bool operator !=(Token token1, Token token2) => !(token1 == token2);

        public void Deconstruct(out uint symbol, out int? value, out string? text)
            => (symbol, value, text) = (Symbol, Value, Text);

        #endregion        
    }
}
