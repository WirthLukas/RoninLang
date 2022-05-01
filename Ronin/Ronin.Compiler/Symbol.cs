
namespace Ronin.Compiler
{
    public enum Symbol
    {
        // Special
        NoSy, EofSy, IllegalSy,

        // Keywords
        Var,

        // Classes
        Number, Identifier,

        /// Arithmethic
        Plus, Minus, Mul, Div,

        // Delimiters
        Assign, LPar, RPar, Semicolon
    }
}
