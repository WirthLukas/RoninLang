
namespace Ronin.Compiler
{
    public enum Symbol
    {
        // Special
        NoSy, EofSy, IllegalSy,

        // Keywords
        Var, If, Else, While, Do, True, False,

        // Classes
        Number, Identifier, Bool,

        // Arithmethic
        Plus, Minus, Mul, Div,

        // Relational Operators
        LessThan, GreatherThan, Equals, LTEquals, GTEquals, NotEquals,

        // Boolean Operators
        Not, And, Or,

        // Delimiters
        Assign, LPar, RPar, Semicolon, LBracket, RBracket
    }
}
