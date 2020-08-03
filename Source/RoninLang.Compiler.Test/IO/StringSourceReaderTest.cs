using System;
using NUnit.Framework;
using RoninLang.Compiler.IO;

namespace RoninLang.Compiler.Test.IO
{
    public class StringSourceReaderTest
    {
        private const string TestString = "This is no source";
        
        [Test]
        public void Test_FirstChars() {
            Console.WriteLine("testNextChars");
            var sr = new StringSourceReader(TestString);
            
            Assert.IsTrue(sr.CurrentChar == 'T');
            Assert.AreEqual(1, sr.CurrentCol);
            Assert.AreEqual(1, sr.CurrentLine);

            sr.NextChar();
            Assert.IsTrue(sr.CurrentChar == 'h');
            Assert.AreEqual(2, sr.CurrentCol);
            Assert.AreEqual(1, sr.CurrentLine);
        }
        
        [Test]
        public void Test_LastCharAndBeyond(){
            Console.WriteLine("testLastCharAndBeyond");
            var sr = new StringSourceReader(TestString);
            
            for (int i = 1; i < TestString.Length; i++) {
                sr.NextChar();
            }
            
            Assert.IsTrue(sr.CurrentChar == 'e');
            Assert.AreEqual(TestString.Length, sr.CurrentCol);
            Assert.AreEqual(1, sr.CurrentLine);
            
            sr.NextChar();
            Assert.IsNull(sr.CurrentChar, "Reading beyond last char not ok");
            sr.NextChar();
            Assert.IsNull(sr.CurrentChar, "Reading beyond last char not ok");
        }
    }
}