using RoninLang.Core;
using RoninLang.Core.IO;

namespace RoninLang.Compiler.Scanning
{
    public static class TerminalsManager
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

                case null:
                    result = Symbol.EofSy;
                    break;

                case ';':
                    result = Symbol.Semicolon;
                    sourceReader.NextChar();
                    break;

                case '=':
                    sourceReader.NextChar();

                    // Is it a Equals Symbol
                    //if (sourceReader.CurrentChar == '=')
                    //{
                        // result = Symbol.Equlas;
                        // sourceReader.NextChar();
                    //}
                    if (sourceReader.CurrentChar == '>')
                    {
                        result = Symbol.Arrow;
                        sourceReader.NextChar();
                    }
                    else
                    {
                        result = Symbol.Assign;
                    }

                    break;

                case '/':
                    sourceReader.NextChar();

                    if (sourceReader.CurrentChar == '/')
                    {
                        sourceReader.NextChar();
                        SkipLineComment(sourceReader);
                    }
                    else
                    {
                        // result = Symbol.Div
                    }

                    break;

                case '{':
                    result = Symbol.LBracket;
                    sourceReader.NextChar();
                    break;

                case '}':
                    result = Symbol.RBracket;
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

                case '"':
                    // String Manager
                    break;

                default:
                    result = Symbol.IllegalSy;
                    sourceReader.NextChar();
                    break;
            }

            return result;
        }

        private static void SkipLineComment(ISourceReader sourceReader)
        {
            char? current = sourceReader.CurrentChar;
            
            while (current != null && current != '\n')
            {
                sourceReader.NextChar();
                current = sourceReader.CurrentChar;
            }
        }
    }
}