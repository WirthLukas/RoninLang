
namespace RoninLang.Core
{
    public struct Token
    {
        public Symbol Symbol;
        public int Value;
        public string ClearName;

        public Token(Symbol symbol = Symbol.NoSy) => (Symbol, Value, ClearName) = (symbol, 0, "");
    }
}