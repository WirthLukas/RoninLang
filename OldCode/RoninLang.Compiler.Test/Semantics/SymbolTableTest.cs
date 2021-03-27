using NUnit.Framework;
using RoninLang.Compiler.ErrorHandling;
using RoninLang.Compiler.IO;
using RoninLang.Compiler.Semantics;
using RoninLang.Core.ErrorHandling;

namespace RoninLang.Compiler.Test.Semantics
{
    public class SymbolTableTest
    {
        private void SetupTestEnvironment(string source)
        {
            var serviceManager = ServiceManager.Instance;
            var sourceReader = new StringSourceReader(source);
            
            serviceManager.Reset<IErrorHandler>(new ErrorHandler(sourceReader));
        }

        [Test]
        public void Test_BeginningScopeIsGlobal()
        {
            SetupTestEnvironment("var x = 10;");
            var st = new SymbolTable();
            
            Assert.AreEqual(SymbolTable.GlobalScopeName, st.CurrentScope.Name);
        }
        
        [Test]
        public void Test_AddGlobalVariables()
        {
            SetupTestEnvironment("var x = 10;");
            var st = new SymbolTable();
            
            Assert.AreEqual(SymbolTable.GlobalScopeName, st.CurrentScope.Name);

            bool success = st.NewVariable("x");
            Assert.IsTrue(success, "Adding Variable did not work");
            Assert.IsTrue(st.CurrentScope.ContainsVariableName("x"));
            
            success = st.NewVariable("y");
            Assert.IsTrue(success, "Adding Variable did not work");
            Assert.IsTrue(st.CurrentScope.ContainsVariableName("y"));
        }
        
        [Test]
        public void Test_AddVariablesWithSameNameInSameScope()
        {
            SetupTestEnvironment("var x = 10;");
            var st = new SymbolTable();
            
            bool success = st.NewVariable("x");
            Assert.IsTrue(success, "Adding Variable did not work");
            Assert.IsTrue(st.CurrentScope.ContainsVariableName("x"));
            
            success = st.NewVariable("x");
            Assert.IsFalse(success, "Adding variables with same name in same scope worked");
            // Nevertheless, a variable with name x should be there
            Assert.IsTrue(st.CurrentScope.ContainsVariableName("x"));
        }
        
        [Test]
        public void Test_CreateFunction()
        {
            SetupTestEnvironment("var x = 10;");
            var st = new SymbolTable();
            
            Assert.AreEqual(SymbolTable.GlobalScopeName, st.CurrentScope.Name);

            bool success = st.NewFunction("main");
            Assert.IsTrue(success, "could not create function");
            Assert.AreEqual("main", st.CurrentScope.Name);
            
            success = st.NewFunction("foo");
            Assert.IsTrue(success, "could not create function");
            Assert.AreEqual("foo", st.CurrentScope.Name);
        }
        
        [Test]
        public void Test_CreateFunctionsWithSameName()
        {
            SetupTestEnvironment("var x = 10;");
            var st = new SymbolTable();

            bool success = st.NewFunction("main");
            Assert.IsTrue(success, "could not create function");
            Assert.AreEqual("main", st.CurrentScope.Name);
            
            success = st.NewFunction("main");
            Assert.IsFalse(success, "could not create function");
            // current scope should be the function created before
            Assert.AreEqual("main", st.CurrentScope.Name);
        }
        
        [Test]
        public void Test_AddVariableInsideAFunction()
        {
            SetupTestEnvironment("var x = 10;");
            var st = new SymbolTable();

            bool success = st.NewFunction("main");
            Assert.IsTrue(success, "could not create function");
            Assert.AreEqual("main", st.CurrentScope.Name);

            success = st.NewVariable("x");
            Assert.IsTrue(success, "could not create variable");
            // current scope should be the function created before
            Assert.IsTrue(st.CurrentScope.ContainsVariableName("x"));
        }
    }
}