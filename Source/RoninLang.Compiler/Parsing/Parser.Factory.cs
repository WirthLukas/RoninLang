using System;
using RoninLang.Compiler.Scanning;
using RoninLang.Core;
using RoninLang.Core.IO;
using RoninLang.Core.Scanning;

#nullable enable

namespace RoninLang.Compiler.Parsing
{
    public abstract partial class Parser
    {
        public static class Factory
        {
            private static IScanner? _scanner;

            public static IScanner? Scanner => IsReady ? _scanner : null;
            public static bool IsReady { get; private set; }

            public static void Setup(ISourceReader sourceReader, NameManager nameManager)
            {
                if (sourceReader == null) throw new ArgumentNullException(nameof(sourceReader));
                if (nameManager == null) throw new ArgumentNullException(nameof(nameManager));
                
                _scanner = new Scanner(sourceReader, nameManager);
                // TODO: Should this really be there?
                _scanner.NextToken();
                IsReady = true;
            }

            public static void Setup(IScanner scanner)
            {
                _scanner = scanner ?? throw new ArgumentNullException(nameof(scanner));
                IsReady = true;
            }
            
            public static void Shutdown() => IsReady = false;

            public static T Create<T>() where T : Parser, new()
            {
                if (!IsReady)
                    throw new AccessViolationException("Cannot create Parser, cause Parser.Factory is not initialized");
                
                return new T();
            }
        }
    }
}