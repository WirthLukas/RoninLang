using System;
using System.Text;
using RoninLang.Core.IO;

namespace RoninLang.Core
{
    public abstract class NameManager
    {
        private readonly ISourceReader _sourceReader;
        
        protected NameManager(ISourceReader sourceReader)
        {
            _sourceReader = sourceReader ?? throw new ArgumentNullException(nameof(sourceReader));
        }

        protected string ReadString()
        {
            var sb = new StringBuilder();
            
            while (_sourceReader.CurrentChar != null && IsAValidCharacterForNames(_sourceReader.CurrentChar.Value))
            {
                sb.Append(_sourceReader.CurrentChar);
                _sourceReader.NextChar();
            }
            
            return sb.ToString();
        }

        public abstract bool IsAValidCharacterForNames(char c);
        public abstract bool IsAValidStartOfName(char c);
        public abstract Token ReadName();
        public abstract string GetStringName(int spix);
    }
}