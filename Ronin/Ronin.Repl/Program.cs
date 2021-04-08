using System;
using System.Collections.Generic;
using Ronin.Compiler;
using Ronin.Compiler.IO;
using Ronin.Compiler.Parsing;
using Ronin.Compiler.Scanning;
using Ronin.Core;

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
                text = Console.ReadLine();

                if (text != "break()")
                {
                    var scanner = new Scanner(new StringSourceReader(text));
                    scanner.NextToken();
                    //foreach (var token in MakeTokens(scanner))
                    //{
                    //    Console.Write($"{token},");
                    //}

                    //Console.WriteLine();
                    ServiceManager.Instance.Reset<IScanner>(scanner);
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
