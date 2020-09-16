using NUnit.Framework;
using RoninLang.Compiler.ErrorHandling;
using RoninLang.Compiler.IO;
using RoninLang.Compiler.Parsing;
using RoninLang.Compiler.Parsing.Block;
using RoninLang.Compiler.Scanning;
using RoninLang.Compiler.Semantics;
using RoninLang.Core.ErrorHandling;
using RoninLang.Core.Scanning;
using RoninLang.Core.Semantics;

namespace RoninLang.Compiler.Test.Parsing.Block
{
    public class BracketsBlockParserTest
    {
        private void SetupTestEnvironment(string source)
        {
            Parser.Factory.Setup();
            var sr = new StringSourceReader(source);
            var serviceManager = ServiceManager.Instance;

            var scanner = new Scanner(sr, new RoninNameManager(sr));
            scanner.NextToken();
            
            serviceManager.Reset<IErrorHandler>(new ErrorHandler(sr));
            serviceManager.Reset<IScanner>(scanner);
            serviceManager.Reset<ISymbolTable>(new SymbolTable());
        }
        
        [Test]
        public void Test_EmptyBlock() {
            SetupTestEnvironment("{}");
            Assert.IsTrue(Parser.Factory.Create<BracketsBlockParser>().Parse());
        }
        
        [Test]
        public void Test_UnfinishedBlock() {
            SetupTestEnvironment("{");
            Assert.IsFalse(Parser.Factory.Create<BracketsBlockParser>().Parse());
        }
        
        [Test]
        public void Test_NonEmptyBlock() {
            SetupTestEnvironment("{ var x = 10; }");
            Assert.IsTrue(Parser.Factory.Create<BracketsBlockParser>().Parse());
        }
        
        // [Test]
        // public void Test_GeneralBlock() {
        //     SetupTestEnvironment("{ some code }");
        //     Assert.IsTrue(Parser.Factory.Create<BracketsBlockParser>().Parse());
        // }
        
        // [Test]
        // public void Test_BlockWithSyntaxError() {
        //     SetupTestEnvironment("{ var x = 10 }");
        //     Assert.IsFalse(Parser.Factory.Create<BracketsBlockParser>().Parse());
        // }
    }
}