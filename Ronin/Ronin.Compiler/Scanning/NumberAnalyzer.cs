using Ronin.Compiler.ErrorHandling;
using Ronin.Core;
using Ronin.Core.ErrorHandling;

namespace Ronin.Compiler.Scanning
{
    internal static class NumberAnalyzer
    {
        public const int MaxInteger = 65535;

        /// <summary>
        /// ReadNumber is called if and only if SourceReader.CurrentChar returns a
        /// digit. ReadNumber scans the number beginning with this digit and converts
        /// it to a cardinal number which is returned.
        /// After a call of readNumber SourceReader.CurrentChar returns the first
        /// character of the source code that is not part of the number.
        /// </summary>
        /// <param name="sourceReader">Source reader</param>
        /// <param name="errorHandler">error handler</param>
        /// <returns>The number scanned</returns>
        public static int ReadNumber(ISourceReader sourceReader, IErrorHandler errorHandler)
        {
            int val = 0;
            char? current = sourceReader.CurrentChar;

            while (current != null && char.IsDigit(current.Value))
            {
                val *= 10;
                val += current.Value - '0';
                current = sourceReader.NextChar();
            }

            if (val > MaxInteger)
            {
                errorHandler.ThrowIntegerOverflow();
            }

            return val;
        }
    }
}
