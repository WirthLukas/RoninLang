namespace RoninLang.Compiler.Semantics
{
    public readonly struct Variable
    {
        public readonly string Name;
        public readonly Scope Scope;

        public Variable(string name, Scope scope)
            => (Name, Scope) = (name, scope);
    }
}