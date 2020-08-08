namespace RoninLang.Core.Semantics
{
    public interface ISymbolTable
    {
        bool NewFunction(string name);
        void NewVariable(string name);
    }
}