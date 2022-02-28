using System;
using System.Text;

namespace Ronin.Core
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

            while (_sourceReader.CurrentChar != null && IsValidCharacterForNames(_sourceReader.CurrentChar.Value))
            {
                sb.Append(_sourceReader.CurrentChar);
                _sourceReader.NextChar();
            }

            return sb.ToString();
        }

        public abstract bool IsValidCharacterForNames(char c);
        public abstract bool IsValidStartOfName(char c);
        public abstract Token ReadName();
        public abstract string GetStringName();
    }
}
