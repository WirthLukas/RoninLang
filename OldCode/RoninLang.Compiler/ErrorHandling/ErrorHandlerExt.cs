using RoninLang.Core;
using RoninLang.Core.ErrorHandling;

namespace RoninLang.Compiler.ErrorHandling
{
    public static class ErrorHandlerExt
    {
        public static void ThrowIntegerOverflow(this IErrorHandler @this)
            => @this.Raise(new Error(ErrorType.IntegerOverflow));

        public static void ThrowSymbolExpectedError(this IErrorHandler @this, Symbol expectedSymbol, Symbol actualSymbol)
            => @this.Raise(
                new Error(
                        ErrorType.SymbolExpected, 
                        new [] { expectedSymbol.ToString(), actualSymbol.ToString() }
                    )
                );

        public static void ThrowGeneralSyntaxError(this IErrorHandler @this, string msg)
            => @this.Raise(
                new Error(ErrorType.GeneralSyntaxError, msg)
            );

        public static void ThrowNameAlreadyDefined(this IErrorHandler @this, string type, string name, string contextName)
            => @this.Raise(new Error(ErrorType.NameAlreadyDefined, new [] { type, name, contextName }));
    }
}