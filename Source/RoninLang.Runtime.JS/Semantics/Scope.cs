using System.Collections.Generic;

namespace RoninLang.Runtime.JS.Semantics
{
    public class Scope
    {
        public string Name { get; }
        private readonly Dictionary<string, Variable> _variables = new Dictionary<string, Variable>();
        
        public Scope(string name)
        {
            Name = name;
        }

        public bool ContainsVariableName(string name) => _variables.ContainsKey(name);
    }
}