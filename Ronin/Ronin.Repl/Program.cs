using System;
using System.Collections.Generic;
using Ronin.Compiler;
using Ronin.Compiler.IO;
using Ronin.Compiler.Parsing;
using Ronin.Compiler.Scanning;
using Ronin.Compiler.Semantics;
using Ronin.Core;
using Ronin.Core.CodeGeneration;
using Ronin.Core.ErrorHandling;
using Ronin.Repl.JS;

// TODO: Variable Assign Parser

namespace Ronin.Repl
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Token.SymbolConverter = symbol => ((Symbol) symbol).ToString();
            string text = "";
            var symbolTable = new SymbolTable();

            while (text != "break()")
            {
                Console.Write("ronin > ");
                text = Console.ReadLine() ?? "";

                if (text != "break()")
                {
                    var sourceReader = new StringSourceReader(text);
                    var errorHandler = new ErrorHandler(sourceReader);
                    var nameManager = new RoninNameManager(sourceReader);
                    var scanner = new Scanner(sourceReader)
                        .AddRule<IgnoreCommentRule>()
                        .AddRule<IgnoreWhitespacesRule>()
                        .AddRule<ArithmeticRule>()
                        .AddRule<ComparisonRule>()
                        .AddRule(new SingleCharRule('=', Symbol.Assign))
                        .AddRule(new SingleCharRule('{', Symbol.LBracket))
                        .AddRule(new SingleCharRule('}', Symbol.RBracket))
                        .AddRule(new SingleCharRule('(', Symbol.LPar))
                        .AddRule(new SingleCharRule(')', Symbol.RPar))
                        .AddRule(new SingleCharRule(';', Symbol.Semicolon))
                        .AddRule(new BoolRule(nameManager))
                        .AddRule(new IdentifierRule(nameManager))
                        .AddRule(new NumberLiteralRule(errorHandler))
                        .AddRule(new EndOfSourceRule((uint)Symbol.EofSy))
                        .AddRuleForNoMatch(new NoMatchFoundRule((uint)Symbol.IllegalSy));

                    scanner.NextToken();

                    //foreach (var token in MakeTokens(scanner))
                    //{
                    //    Console.Write($"{token},");
                    //}

                    //Console.WriteLine();
                    ServiceManager.Instance.Reset<IScanner>(scanner);
                    ServiceManager.Instance.Reset<IErrorHandler>(errorHandler);
                    ServiceManager.Instance.Reset(symbolTable);
                    var parser = new RoninParser();
                    var result = parser.Parse();
                    //Console.WriteLine(result);
                    Console.WriteLine(new CodeGenerator(result).Convert(new ConverterContext()));
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
