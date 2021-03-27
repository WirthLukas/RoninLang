using System.Collections.Generic;

namespace RoninLang.Compiler.Semantics
{
    public class Scope
    {
        public string Name { get; }
        private readonly Dictionary<string, Variable> _variables = new Dictionary<string, Variable>();
        
        public Scope(string name) => Name = name;

        public bool ContainsVariableName(string name) => _variables.ContainsKey(name);

        public bool AddVariable(Variable variable)
        {
            if (ContainsVariableName(variable.Name))
                return false;
            
            _variables.Add(variable.Name, variable);
            return true;
        }
    }
}