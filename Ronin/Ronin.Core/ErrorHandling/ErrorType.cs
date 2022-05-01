using System;

namespace Ronin.Core.ErrorHandling
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

        public static readonly ErrorType IntegerOverflow = new (1, ErrorClass.Lexical, "Integer overflow");

        public static readonly ErrorType SymbolExpected = new (21, ErrorClass.Syntax, "{0} expected but found {1}");

        public static readonly ErrorType NameUndefined = new (51, ErrorClass.Semantical, "Identifier {0} is not defined in this context");
        public static readonly ErrorType NameAlreadyDefined = new(54, ErrorClass.Semantical, "Identifier {0} is already defined");
    }
}
