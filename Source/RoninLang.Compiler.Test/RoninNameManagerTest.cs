using System;
using NUnit.Framework;
using RoninLang.Compiler.IO;
using RoninLang.Core;
using RoninLang.Core.IO;

namespace RoninLang.Compiler.Test
{
    public class RoninNameManagerTest
    {
        private RoninNameManager _nameManager;

        [SetUp]
        public void Setup()
        {
            _nameManager = new RoninNameManager(new StringSourceReader("var1;"));
        }
        
        [Test]
        public void Test_IsAValidStartOfName() {
            for (char c = 'a'; c <= 'z'; c++) {
                Assert.IsTrue(_nameManager.IsAValidStartOfName(c));
            }

            for (char c = 'A'; c <= 'Z'; c++) {
                Assert.IsTrue(_nameManager.IsAValidStartOfName(c));
            }

            Assert.IsTrue(_nameManager.IsAValidStartOfName('_'));
            Assert.IsTrue(_nameManager.IsAValidStartOfName('$'));
        }
        
        [Test]
        public void Test_InvalidStartsOfName() {
            for (char c = '0'; c <= '9'; c++) {
                Assert.IsFalse(_nameManager.IsAValidStartOfName(c));
            }

            char[] invalidStarts = {
                '!', '"', '§', '%', '&', '/', '(', ')', '<', '>', ',', ':', '-', '.', ';', ',', ':', '-', '#', '+', '*', '´', '`'
            };

            foreach (var c in invalidStarts)
            {
                Assert.IsFalse(_nameManager.IsAValidStartOfName(c));
            }
        }
        
        [Test]
        public void Test_ValidNameChars() {
            // additionally to all valid starts of name
            for (char c = '0'; c <= '9'; c++) {
                Assert.IsTrue(_nameManager.IsAValidCharacterForNames(c));
            }
        }
        
        [Test]
        public void Test_InvalidNameChars() {
            char[] invalidChars = {
                '!', '"', '§', '%', '&', '/', '(', ')', '<', '>', ',', ':', '-', '.', ';', ',', ':', '-', '#', '+', '*', '´', '`'
            };
            
            foreach (var c in invalidChars) {
                Assert.IsFalse(_nameManager.IsAValidCharacterForNames(c));
            }
        }
        
        [Test]
        public void Test_ReadName() {
            var sr = new StringSourceReader("var1; var2;");
            var nameManager = new RoninNameManager(sr);
            Token token = nameManager.ReadName();

            Assert.AreEqual(Symbol.Identifier, token.Symbol);
            Assert.AreEqual(0, token.Value);
            Assert.AreEqual("var1", token.ClearName);
            Assert.AreEqual(';', sr.CurrentChar);

            sr.NextChar();
            sr.NextChar();

            token = nameManager.ReadName();
            Assert.AreEqual(Symbol.Identifier, token.Symbol, "IDENT ");
            Assert.AreEqual(1, token.Value, "Spix ");
            Assert.AreEqual("var2", token.ClearName);
            Assert.AreEqual(';', sr.CurrentChar, "Current char ");
        }
        
        [Test]
        public void Test_ReadWeirdNames() {
            ISourceReader srWeird = new StringSourceReader("_;__;_$;$_;$$");
            var nm = new RoninNameManager(srWeird);

            var token = nm.ReadName();
            Assert.AreEqual(Symbol.Identifier, token.Symbol);
            Assert.AreEqual("_", token.ClearName);

            srWeird.NextChar();
            token = nm.ReadName();
            Assert.AreEqual(Symbol.Identifier, token.Symbol);
            Assert.AreEqual("__", token.ClearName);

            srWeird.NextChar();
            token = nm.ReadName();
            Assert.AreEqual(Symbol.Identifier, token.Symbol);
            Assert.AreEqual("_$", token.ClearName);

            srWeird.NextChar();
            token = nm.ReadName();
            Assert.AreEqual(Symbol.Identifier, token.Symbol);
            Assert.AreEqual("$_", token.ClearName);

            srWeird.NextChar();
            token = nm.ReadName();
            Assert.AreEqual(Symbol.Identifier, token.Symbol);
            Assert.AreEqual("$$", token.ClearName);
        }
        
        [Test]
        public void Test_ReadNameDouble() {
            var srMany = new StringSourceReader("var1; var2; bla; blu; var2; blu;");
            var nameManagerMany = new RoninNameManager(srMany);

            srMany.NextChar();
            var token = nameManagerMany.ReadName(); // var1
            srMany.NextChar();
            srMany.NextChar();

            token = nameManagerMany.ReadName(); // var2
            int var2Spix = token.Value;
            srMany.NextChar();
            srMany.NextChar();

            token = nameManagerMany.ReadName(); // bla
            srMany.NextChar();
            srMany.NextChar();

            token = nameManagerMany.ReadName(); // blu
            int bluSpix = token.Value;
            srMany.NextChar();
            srMany.NextChar();

            token = nameManagerMany.ReadName(); // var2
            srMany.NextChar();
            srMany.NextChar();
            Assert.AreEqual(var2Spix, token.Value);

            token = nameManagerMany.ReadName(); // blu
            srMany.NextChar();
            srMany.NextChar();
            Assert.AreEqual(bluSpix, token.Value);
        }
        
        [Test]
        public void Test_GetStringName() {
            Console.WriteLine("GetStringName");
            
            var sr = new StringSourceReader("var1; var2;");
            var nameManager = new RoninNameManager(sr);

            Token token = nameManager.ReadName();

            Assert.AreEqual("var1", nameManager.GetStringName(token.Value));

            sr.NextChar();
            sr.NextChar();

            token = nameManager.ReadName();

            Assert.AreEqual("var2", nameManager.GetStringName(token.Value));
        }
        
        [Test]
        public void Test_Keywords() {
            Console.WriteLine("Test Keywords");
            var srKeywords = new StringSourceReader("fun, int, var, val,");
            var nmKeywords = new RoninNameManager(srKeywords);
            
            var token = nmKeywords.ReadName();

            var expTokens = new [] { Symbol.Fun, Symbol.Int, Symbol.Var, Symbol.Val };

            foreach (var s in expTokens) {
                Assert.AreEqual(s, token.Symbol);
                Assert.AreEqual(',', srKeywords.CurrentChar);
                srKeywords.NextChar();
                srKeywords.NextChar();
                token = nmKeywords.ReadName();
            }
        }
    }
}