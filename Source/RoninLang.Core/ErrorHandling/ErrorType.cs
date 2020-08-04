using System;

namespace RoninLang.Core.ErrorHandling
{
    public enum ErrorClass : byte
    {
        Lexical, Syntax
    }
    
    public struct ErrorType
    {
        public readonly int ErrorNumber;
        public readonly ErrorClass ErrorClass;
        public readonly string ErrorMessage;

        public ErrorType(int errorNumber, ErrorClass errorClass, string errorMessage)
        {
            ErrorNumber = errorNumber;
            ErrorClass = errorClass;
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        }
        
        public static readonly ErrorType IntegerOverflow = new ErrorType(1, ErrorClass.Lexical, "Integer overflow");
        
        public static readonly ErrorType SymbolExpected = new ErrorType(21, ErrorClass.Syntax, "{0} expected but found {1}");
        public static readonly ErrorType GeneralSyntaxError = new ErrorType(49, ErrorClass.Syntax, "Syntax Error: {0}");
    }
}