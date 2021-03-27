using System.Collections.Generic;
using System.Linq;
using RoninLang.Core.Semantics;

namespace RoninLang.Compiler.Semantics
{
    public class SymbolTable : ISymbolTable
    {
        public const string GlobalScopeName = "~~Global~~";
        
        private readonly Scope _globalScope = new Scope(GlobalScopeName);
        private readonly Dictionary<string, Scope> _scopes;
        // TODO if Scope will be a struct, we have to consider that we have then to deal with references
        // Cause we would only make a copy when we assign something to this variable
        private Scope _currentScope;

        public Scope GlobalScope => _globalScope;
        public Scope CurrentScope => _currentScope;
        public Scope[] Scopes => _scopes.Values.ToArray();
        
        public SymbolTable()
        {
            _scopes = new Dictionary<string, Scope> { { _globalScope.Name, _globalScope } };
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

        public bool NewVariable(string name) => _currentScope.AddVariable(new Variable(name, _currentScope));
    }
}