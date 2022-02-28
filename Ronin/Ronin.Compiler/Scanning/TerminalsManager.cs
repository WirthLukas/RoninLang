using Ronin.Core;

namespace Ronin.Compiler.Scanning
{
    internal class TerminalsManager
    {
        /// <summary>
        /// Skips the next terminal letter (uses nextChar Method of ISourceReader)
        ///
        /// Example:
        /// * letter: ;     =>      Returns Symbol.Semicolon and goes to the next Char
        /// * Letter: =     =>      Goes to the Next Char. if this one os also a = it returns
        ///                         a Equals Symbol, otherwise a Assign Symbol
        ///                         Therefore it skips 2 Chars
        /// </summary>
        /// <param name="sourceReader">SourceReader implementation</param>
        /// <returns>The Symbol of the letter(s)</returns>
        public static Symbol ReadTerminalSymbol(ISourceReader sourceReader)
        {
            var result = Symbol.NoSy;

            switch (sourceReader.CurrentChar)
            {
                case ' ':
                case '\t':
                case '\n':
                    // Skip Letter
                    sourceReader.NextChar();
                    break;

                case '+':
                    result = Symbol.Plus;
                    sourceReader.NextChar();
                    break;

                case '-':
                    result = Symbol.Minus;
                    sourceReader.NextChar();
                    break;

                case '*':
                    result = Symbol.Mul;
                    sourceReader.NextChar();
                    break;

                case '/':
                    result = Symbol.Div;
                    sourceReader.NextChar();
                    break;

                case '=':
                    //sourceReader.NextChar();

                    //if (sourceReader.CurrentChar == '=')
                    //{

                    //}
                    //else
                    //{
                        
                    //}
                    result = Symbol.Assign;
                    sourceReader.NextChar();
                    break;

                case '(':
                    result = Symbol.LPar;
                    sourceReader.NextChar();
                    break;

                case ')':
                    result = Symbol.RPar;
                    sourceReader.NextChar();
                    break;

                default:
                    result = Symbol.IllegalSy;
                    sourceReader.NextChar();
                    break;
            }

            return result;
        }
    }
}
