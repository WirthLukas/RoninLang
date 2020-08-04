using System.Collections.Generic;

namespace RoninLang.Compiler.SymbolTable
{
    public class Scope
    {
        private Scope _parent = null;
        private List<string> _variables = new List<string>();
        private Dictionary<string, Scope> _subScopes = new Dictionary<string, Scope>();
        private readonly string _name;

        public Scope(string name)
        {
            _name = name;
        }
        
        public void AddScope(Scope scope)
        {
            scope._parent = this;
            _subScopes.Add(scope._name, scope);
        }
    }
}