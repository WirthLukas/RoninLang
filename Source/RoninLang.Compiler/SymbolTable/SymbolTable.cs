using System.Collections.Generic;

namespace RoninLang.Compiler.SymbolTable
{
    public class SymbolTable
    {
        private readonly Scope _globalScope = new Scope("global");
        private readonly Dictionary<string, Scope> _functionScopes = new Dictionary<string, Scope>();
        private Scope _currentScope;

        public SymbolTable()
        {
            _currentScope = _globalScope;
        }
        
        public void NewFunction(string name)
        {
            if (_functionScopes.ContainsKey(name)) return;
            
            var scope = new Scope(name);
            _functionScopes.Add(name, scope);
            _currentScope = scope;
        }

        public void AddVariable(string name)
        {
            
        }
    }
}