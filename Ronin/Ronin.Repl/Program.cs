using System;
using System.Collections.Generic;
using Ronin.Compiler;
using Ronin.Compiler.IO;
using Ronin.Compiler.Parsing;
using Ronin.Compiler.Scanning;
using Ronin.Core;
using Ronin.Core.ErrorHandling;

namespace Ronin.Repl
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Token.SymbolConverter = symbol => ((Symbol) symbol).ToString();
            string text = "";

            while (text != "break()")
            {
                Console.Write("ronin > ");
                text = Console.ReadLine() ?? "";

                if (text != "break()")
                {
                    var sourceReader = new StringSourceReader(text);
                    var errorHandler = new ErrorHandler(sourceReader);
                    var scanner = new Scanner(sourceReader, errorHandler, new RoninNameManager(sourceReader));
                    scanner.NextToken();
                    //foreach (var token in MakeTokens(scanner))
                    //{
                    //    Console.Write($"{token},");
                    //}

                    //Console.WriteLine();
                    ServiceManager.Instance.Reset<IScanner>(scanner);
                    ServiceManager.Instance.Reset<IErrorHandler>(errorHandler);
                    var parser = new RoninParser();
                    var result = parser.Parse();
                    Console.WriteLine(result);
                }
            }
        }

        private static IEnumerable<Token> MakeTokens(Scanner scanner)
        {
            while (scanner.CurrentToken.Symbol != (int) Symbol.EofSy)
            {
                yield return scanner.NextToken();
            }
        }
    }
}
