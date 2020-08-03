using System.Collections.Generic;
using System.Linq;
using RoninLang.Core;
using RoninLang.Core.IO;

namespace RoninLang.Compiler
{
    public class RoninNameManager : NameManager
    {
        private readonly Dictionary<string, int> _names = new Dictionary<string, int>();
        
        public RoninNameManager(ISourceReader sourceReader) : base(sourceReader) { }

        public override bool IsAValidCharacterForNames(char c)
            => IsAValidStartOfName(c) || char.IsDigit(c);

        public override bool IsAValidStartOfName(char c)
            => char.IsLetter(c) || c == '_' || c == '$';
        
        /// <summary>
        /// Reads a name. readName() is called if and only if
        /// SourceReader.getCurrentChar() contains a letter. readName() scans the
        /// identifier starting with this letter, checks whether it is a keyword and
        /// returns the appropriate token. If the name read is a keyword, Token.sy is
        /// set to the corresponding Symbol. If the name read is an identifier,
        /// Token.sy is set to IDENTIFIER and Token.value is set to a unique
        /// identifier (spix).
        ///
        /// After a call of readName SourceReader.getCurrentChar() returns the first
        /// character of the source code that is not part of the identifier.
        /// </summary>
        /// <param name="token">Token which corresponds to the name read.</param>
        public override Token ReadName()
        {
            string name = base.ReadString();
            Symbol readSymbol = GetTokenTypeOf(name);

            var token = new Token { Symbol = readSymbol, ClearName = name };

            if (readSymbol == Symbol.Identifier)
            {
                int spix = AddName(name);
                token.Value = spix;
            }

            return token;
        }

        private Symbol GetTokenTypeOf(string s)
        {
            Symbol symbol = KeywordTable.GetSymbolOf(s);
            return symbol == Symbol.NoSy ? Symbol.Identifier : symbol;
        }

        private int AddName(string name)
        {
            int spix;

            if (_names.ContainsKey(name))
                spix = _names[name];
            else
            {
                spix = _names.Count;
                _names.Add(name, spix);
            }

            return spix;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spix">unique identifier</param>
        /// <returns></returns>
        public override string GetStringName(int spix)
            => _names
                .SingleOrDefault(item => item.Value == spix)
                .Key;
    }
}