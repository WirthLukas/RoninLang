using RoninLang.Core.ErrorHandling;

namespace RoninLang.Compiler.ErrorHandling
{
    public static class ErrorHandlerExt
    {
        public static void ThrowIntegerOverflow(this IErrorHandler @this)
            => @this.Raise(new Error(ErrorType.IntegerOverflow));
    }
}