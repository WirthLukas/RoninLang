using System;
using NUnit.Framework;
using RoninLang.Compiler;
using RoninLang.Compiler.ErrorHandling;
using RoninLang.Compiler.IO;
using RoninLang.Compiler.Scanning;
using RoninLang.Core.ErrorHandling;
using RoninLang.Core.IO;

namespace RoninLagn.Compiler.Test.Scanning
{
    public class NumberAnalyzerTest
    {
        private ISourceReader _sourceReader;
        private IErrorHandler _errorHandler;
        private readonly ServiceManager _services = ServiceManager.Instance;

        private void SetupTestEnvironment(string source)
        {
            _sourceReader = new StringSourceReader(source);
            _errorHandler = new ErrorHandler(_sourceReader);
            _services.Reset<IErrorHandler>(_errorHandler);
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
        
        [Test]
        public void Test_ReadNumberOverflow() {
            Console.WriteLine("readNumberOverflow");
            
            SetupTestEnvironment(NumberAnalyzer.MaxInteger + 1 + ";");
            int result = NumberAnalyzer.ReadNumber(_sourceReader);
            Assert.AreEqual(1, _errorHandler.Count);
            Assert.AreEqual(ErrorType.IntegerOverflow.ErrorNumber, _errorHandler.LastError.Number);
            Assert.AreEqual(';', _sourceReader.CurrentChar);
        }
    }
}