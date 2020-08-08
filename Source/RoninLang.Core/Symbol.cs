namespace RoninLang.Core
{
    public enum Symbol
    {
        // Special
        NoSy, EofSy, IllegalSy,
            
        // Keywords
        Fun, Int, Var, Val,
            
        // Classes
        Identifier, Number, String,
            
        // Delimiters
        Assign, Semicolon, Colon, LPar, RPar, LBracket, RBracket, Arrow
    }
}