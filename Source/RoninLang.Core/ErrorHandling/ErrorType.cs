using System;

namespace RoninLang.Core.ErrorHandling
{
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
    }

    public enum ErrorClass : byte
    {
        Lexical
    }
}