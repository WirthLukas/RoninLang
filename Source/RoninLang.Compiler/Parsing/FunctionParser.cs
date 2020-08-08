using RoninLang.Compiler.Parsing.Block;
using RoninLang.Compiler.Parsing.Variables;
using RoninLang.Core;

namespace RoninLang.Compiler.Parsing
{
    public class FunctionParser : Parser
    {
        public string FunctionName { get; private set; }
        
        protected override void ParseSpecificPart()
        {
            // fun
            ParseSymbol(Symbol.Fun);
            
            // name of function
            int name = ParseIdentifier();
            FunctionName = LastParsedToken.ClearName;
            
            // Semantic Check
            Semantics(() =>
            {
                SymbolTable.NewFunction(FunctionName);
                // CodeGenerator.NewFunction(FunctionName);
            });
            
            // parenthesis
            ParseSymbol(Symbol.LPar);
            ParseSymbol(Symbol.RPar);

            if (LastParsedToken.Symbol == Symbol.Colon)
            {
                ParseSymbol(Factory.Create<TypeDefParser>());
            }

            // Block Parser
            // {} or =>
            var block = Parser.Factory.Create<BlockParser>();
            ParseSymbol(block);
        }
    }
}