using NUnit.Framework;
using RoninLang.Compiler.ErrorHandling;
using RoninLang.Compiler.IO;
using RoninLang.Compiler.Parsing;
using RoninLang.Compiler.Parsing.Variables;
using RoninLang.Compiler.Scanning;
using RoninLang.Compiler.Semantics;
using RoninLang.Core;
using RoninLang.Core.ErrorHandling;
using RoninLang.Core.Scanning;
using RoninLang.Core.Semantics;

namespace RoninLang.Compiler.Test.Parsing.Variables
{
    public class VariableDeclarationParserTest
    {
        private IScanner _scanner;
        
        private void SetupTestEnvironment(string source)
        {
            var serviceManager = ServiceManager.Instance;
            var sourceReader = new StringSourceReader(source);
            _scanner = new Scanner(sourceReader, new RoninNameManager(sourceReader));
            
            serviceManager.Reset<IErrorHandler>(new ErrorHandler(sourceReader));
            serviceManager.Reset<IScanner>(_scanner);
            serviceManager.Reset<ISymbolTable>(new SymbolTable());
        }
        
        [Test]
        public void Test_SimpleInt() {
            SetupTestEnvironment("var x: int = 10;");
            var parser = Parser.Factory.Create<VariableDeclarationParser>();
            
            bool expResult = true;
            bool result = parser.Parse();
            Assert.AreEqual(expResult, result);
            Assert.AreEqual(Symbol.EofSy, _scanner.CurrentToken.Symbol);
        }
    }
}