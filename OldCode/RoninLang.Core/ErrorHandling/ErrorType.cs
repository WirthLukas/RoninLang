using System;

namespace RoninLang.Core.ErrorHandling
{
    public enum ErrorClass : byte
    {
        Lexical, Syntax, Semantical
    }
    
    public readonly struct ErrorType
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
        
        // 0: type (Function, Variable); 1: name; 2: context name (Scope, Module)
        public static readonly ErrorType NameAlreadyDefined = new ErrorType(54, ErrorClass.Semantical, "{0} {1} is already defined in current {2}");
    }
}