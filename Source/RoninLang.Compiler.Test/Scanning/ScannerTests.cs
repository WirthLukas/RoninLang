using System;
using NUnit.Framework;
using RoninLang.Compiler;
using RoninLang.Compiler.IO;
using RoninLang.Compiler.Scanning;
using RoninLang.Core;
using RoninLang.Core.IO;
using RoninLang.Core.Scanning;

namespace RoninLagn.Compiler.Test.Scanning
{
    public class ScannerTests
    {
        private ISourceReader _sr;
        private IScanner _scanner;
        
        // [SetUp]
        // public void Setup()
        // {
        // }

        private void SetupTestEnvironment(string source)
        {
            _sr = new StringSourceReader(source);
            _scanner = new Scanner(_sr, new RoninNameManager(_sr));
        }
        
        [Test]
        public void Test_CommentLine() {
            SetupTestEnvironment("// This is a comment line");
            _scanner.NextToken();

            Assert.IsTrue(_scanner.CurrentToken.Symbol == Symbol.EofSy, "EOFSY expected");
        }
        
        [Test]
        public void Test_Comment() {
            SetupTestEnvironment("// This is a comment line \n fun");
            _scanner.NextToken();

            Assert.AreEqual(Symbol.Fun, _scanner.CurrentToken.Symbol, "FunSy expected");
        }
        
        [Test]
        public void Test_SimpleSymbols() {
            Console.WriteLine("testSimpleSymbols");
        
            // original "();=+-*/%,"
            SetupTestEnvironment("();={}");

            var expectedSymbols = new[]
            {
                Symbol.LPar, Symbol.RPar, Symbol.Semicolon, Symbol.Assign,
                Symbol.LBracket, Symbol.RBracket
            };

            foreach (var symbol in expectedSymbols)
            {
                _scanner.NextToken();
                Assert.AreEqual(symbol, _scanner.CurrentToken.Symbol, $"{symbol.ToString()} expected");
            }
        }
        
        // What should that do?
        // [Test]
        // public void Test_GetCurrentTokenKeepsUnchanged() {
        //     SetupTestEnvironment("(;");
        //     _scanner.NextToken();
        //     var firstToken = _scanner.CurrentToken;
        //     _scanner.NextToken();
        //     var secondToken = _scanner.CurrentToken;
        //     assertTrue(firstToken != secondToken);
        // }
        
        [Test]
        public void Test_Number() {
            Console.WriteLine("testNumber");
        
            SetupTestEnvironment("42;");
        
            _scanner.NextToken();
            Assert.AreEqual(Symbol.Number, _scanner.CurrentToken.Symbol, "NumberSymbol exprected");
            Assert.AreEqual(42, _scanner.CurrentToken.Value);
            Assert.AreEqual(';', _sr.CurrentChar, "Expected next char is not ;");
        }
        
        [Test]
        public void Test_Name() {
            SetupTestEnvironment("aVar; anotherVar;");
        
            _scanner.NextToken();
            Assert.AreEqual(Symbol.Identifier, _scanner.CurrentToken.Symbol);
            int spix1 = _scanner.CurrentToken.Value;
        
            _scanner.NextToken();
            Assert.AreEqual(Symbol.Semicolon, _scanner.CurrentToken.Symbol);
            
            _scanner.NextToken();
            Assert.AreEqual(Symbol.Identifier, _scanner.CurrentToken.Symbol);
            int spix2 = _scanner.CurrentToken.Value;
        
            Assert.IsTrue(spix1 != spix2, "Expected spix1 != spix2");
            Assert.AreEqual(';', _sr.CurrentChar, "Expected next char");
        }
        
        [Test]
        public void Test_SameName() {
            Console.WriteLine("testSameName");
        
            SetupTestEnvironment("aVar; aVar2; aVar");
        
            _scanner.NextToken();
            Assert.AreEqual(Symbol.Identifier, _scanner.CurrentToken.Symbol);
            int spix1 = _scanner.CurrentToken.Value;
        
            _scanner.NextToken();
            Assert.AreEqual(Symbol.Semicolon, _scanner.CurrentToken.Symbol);
        
            _scanner.NextToken();
            Assert.AreEqual(Symbol.Identifier, _scanner.CurrentToken.Symbol);
            int spix2 = _scanner.CurrentToken.Value;

            _scanner.NextToken();
            Assert.AreEqual(Symbol.Semicolon, _scanner.CurrentToken.Symbol);
        
            _scanner.NextToken();
            Assert.AreEqual(Symbol.Identifier, _scanner.CurrentToken.Symbol);
            int spix3 = _scanner.CurrentToken.Value;
            
            Assert.IsTrue(spix1 != spix2, "Expected spix1 != spix2");
            Assert.IsTrue(spix1 == spix3, "Expected spix1 = spix2");
        }
        
        // TODO: add string feature
        // [Test]
        // public void Test_String() {
        //     Console.WriteLine("testString");
        //
        //     SetupTestEnvironment("'a string' \"another string\"");
        //     _scanner.NextToken();
        //     StringToken st = (StringToken)scanner.getCurrentToken();
        //     assertEquals("Sy ", Symbol.STRING, st.getSymbol());
        //     assertEquals("Start addr ", 0, st.getAddress());
        //     assertEquals("Length ", 8, st.getLength());
        //
        //     scanner.nextToken();
        //     st = (StringToken)scanner.getCurrentToken();
        //     assertEquals("Sy ", Symbol.STRING, st.getSymbol());
        //     assertEquals("Start addr ", 8, st.getAddress());
        //     assertEquals("Length ", 14, st.getLength());
        // }
        
        [Test]
        public void Test_Keywords() {
            Console.WriteLine("testKeywords");
        
            // SetupTestEnvironment("unit foo; do put x; a < 0; a != 0; true; false; putln; done done foo;");
            SetupTestEnvironment("fun foo() { var int x = 10; }");
            
            var expectedSymbols = new []
            {
                Symbol.Fun, Symbol.Identifier, Symbol.LPar, Symbol.RPar, Symbol.LBracket, Symbol.Var,
                Symbol.Int, Symbol.Identifier, Symbol.Assign, Symbol.Number, Symbol.Semicolon, Symbol.RBracket
            };
        
            foreach (Symbol symbol in expectedSymbols) {
                _scanner.NextToken();
                Assert.AreEqual(symbol, _scanner.CurrentToken.Symbol);
            }
        
            _scanner.NextToken();
            Assert.AreEqual(Symbol.EofSy, _scanner.CurrentToken.Symbol, "EOFSY expected");
            _scanner.NextToken();
            Assert.AreEqual(Symbol.EofSy, _scanner.CurrentToken.Symbol, "EOFSY expected");
        }
    }
}