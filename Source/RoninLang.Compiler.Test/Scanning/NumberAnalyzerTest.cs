using NUnit.Framework;
using RoninLang.Compiler.IO;
using RoninLang.Compiler.Scanning;
using RoninLang.Core.IO;

namespace RoninLagn.Compiler.Test.Scanning
{
    public class NumberAnalyzerTest
    {
        private ISourceReader _sourceReader;

        private void SetupTestEnvironment(string source)
        {
            _sourceReader = new StringSourceReader(source);
        }
        
        [Test]
        public void Test_ReadNumber() {
            SetupTestEnvironment("42;");
        
            int expResult = 42;
            int result = NumberAnalyzer.ReadNumber(_sourceReader);
            Assert.AreEqual(expResult, result);
            Assert.AreEqual(';', _sourceReader.CurrentChar);
        }
        
        [Test]
        public void Test_LargestNumber() {
            SetupTestEnvironment(NumberAnalyzer.MaxInteger + ";");
            int result = NumberAnalyzer.ReadNumber(_sourceReader);
            Assert.AreEqual(NumberAnalyzer.MaxInteger, result);
            Assert.AreEqual(';', _sourceReader.CurrentChar);
        }
        
        // TODO Test ErrorHandler for NumberAnalyzer
        // [Test]
        // public void Test_ReadNumberOverflow() {
        //     Console.WriteLine("readNumberOverflow");
        //     
        //     SetupTestEnvironment(NumberAnalyzer.MaxInteger + 1 + ";");
        //     int result = NumberAnalyzer.ReadNumber(_sourceReader);
        //     Assert.AreEqual(1, errorHandler.getCount());
        //     Assert.AreEqual(ErrorType.INTEGER_OVERFLOW.getNumber(), errorHandler.getLastError().getNumber());
        //     Assert.AreEqual(';', _sourceReader.CurrentChar);
        // }
    }
}