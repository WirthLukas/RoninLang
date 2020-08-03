using NUnit.Framework;
using RoninLang.Compiler.ErrorHandling;
using RoninLang.Core.ErrorHandling;

namespace RoninLang.Compiler.Test.ErrorHandling
{
    public class ErrorTest
    {
        [Test]
        public void Test_ErrorsWithoutFormatString() {
            var error = new Error(ErrorType.IntegerOverflow);
            CheckErrorObject(error, 1, ErrorClass.Lexical, "Integer overflow");

            // TODO: Add Test for further Error Types
            // error = new Error(Error.ErrorType.INVALID_STRING);
            // checkErrorObject(error, 2, Error.ErrorClass.LEXICAL, "Non terminated string constant");
            //
            // error = new Error(Error.ErrorType.POSITIVE_ARRAY_SIZE_EXPECTED);
            // checkErrorObject(error, 56, Error.ErrorClass.SEMANTICAL, "Array size must be a positive integer");        
        }
        
        private void CheckErrorObject(Error error, int errorNumber, ErrorClass errorClass, string errorMessage) {
            Assert.AreEqual(errorClass, error.ErrorClass);
            Assert.AreEqual(errorNumber, error.Number);
            Assert.AreEqual(errorMessage, error.Message);
        }
    }
}