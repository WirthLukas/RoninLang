using NUnit.Framework;
using RoninLang.Compiler.ErrorHandling;
using RoninLang.Core.ErrorHandling;

namespace RoninLang.Compiler.Test.ErrorHandling
{
    public class ErrorTest
    {
        private void CheckErrorObject(Error error, int errorNumber, ErrorClass errorClass, string errorMessage) {
            Assert.AreEqual(errorClass, error.ErrorClass);
            Assert.AreEqual(errorNumber, error.Number);
            Assert.AreEqual(errorMessage, error.Message);
        }
        
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

        [Test]
        public void Test_ErrorsWithTwoParameters() {
            Error error = new Error(ErrorType.SymbolExpected, "foo", "bar");
            CheckErrorObject(error, 21, ErrorClass.Syntax, "foo expected but found bar");
        
            // error = new Error(Error.ErrorType.OPERATOR_OPERAND_TYPE_MISMATCH, "&&", "bool");
            // checkErrorObject(error, 58, Error.ErrorClass.SEMANTICAL, "Operator && requires a bool operand");
        }
    }
}