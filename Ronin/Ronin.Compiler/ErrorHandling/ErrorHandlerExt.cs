using Ronin.Core.ErrorHandling;

namespace Ronin.Compiler.ErrorHandling
{
    public static class ErrorHandlerExt
    {
        public static void ThrowIntegerOverflow(this IErrorHandler @this)
            => @this.Raise(new Error(ErrorType.IntegerOverflow));

        public static void ThrowSymbolExpectedError(this IErrorHandler @this, string expectedSymbol, string actualSymbol)
            => @this.Raise(
                new Error(
                    ErrorType.SymbolExpected,
                    new[] { expectedSymbol, actualSymbol }
                )
            );

        public static void ThrowNameAlreadyDefined(this IErrorHandler @this, string variableName)
        {
            @this.Raise(new Error(ErrorType.NameAlreadyDefined, variableName));
        }

        public static void ThrowNameUndefined(this IErrorHandler @this, string variableName)
        {
            @this.Raise(new Error(ErrorType.NameUndefined, variableName));
        }
    }
}
