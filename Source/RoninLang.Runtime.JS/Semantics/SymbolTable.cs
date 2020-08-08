using System.Collections.Generic;
using RoninLang.Core.Semantics;

namespace RoninLang.Runtime.JS.Semantics
{
    public class SymbolTable : ISymbolTable
    {
        private readonly Scope _globalScope = new Scope("$$Global$$");
        private readonly Dictionary<string, Scope> _scopes;
        // TODO if Scope will be a struct, we have to consider that we have then to deal with references
        // Cause we would only make a copy when we assign something to this variable
        private Scope _currentScope;

        public SymbolTable()
        {
            _scopes = new Dictionary<string, Scope>() { { _globalScope.Name, _globalScope } };
            _currentScope = _globalScope;
        }
        
        public bool NewFunction(string name)
        {
            if (_scopes.ContainsKey(name))
                return false;
            
            var scope = new Scope(name);
            _scopes.Add(name, scope);
            _currentScope = scope;
            return true;
        }

        public void NewVariable(string name)
        {
            if (_currentScope.ContainsVariableName(name))
            {
                // variable already defined
            }
            else
            {
                // Add Variable
            }
        }
    }
}