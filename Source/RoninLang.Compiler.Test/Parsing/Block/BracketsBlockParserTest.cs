using NUnit.Framework;
using RoninLang.Compiler.ErrorHandling;
using RoninLang.Compiler.IO;
using RoninLang.Compiler.Parsing;
using RoninLang.Compiler.Parsing.Block;
using RoninLang.Compiler.Scanning;
using RoninLang.Core.ErrorHandling;
using RoninLang.Core.Scanning;

namespace RoninLang.Compiler.Test.Parsing.Block
{
    public class BracketsBlockParserTest
    {
        private void SetupTestEnvironment(string source)
        {
            Parser.Factory.Setup();
            var sr = new StringSourceReader(source);
            var serviceManager = ServiceManager.Instance;
            
            serviceManager.Reset<IErrorHandler>(new ErrorHandler(sr));
            serviceManager.Reset<IScanner>(new Scanner(sr, new RoninNameManager(sr)));
            // TODO Add symbol table
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
        
        [Test]
        public void Test_BlockWithSyntaxError() {
            SetupTestEnvironment("{ var x = 10 }");
            Assert.IsFalse(Parser.Factory.Create<BracketsBlockParser>().Parse());
        }
    }
}