namespace RoninLang.Core.Semantics
{
    public interface ISymbolTable
    {
        bool NewFunction(string name);
        bool NewVariable(string name);
    }
}