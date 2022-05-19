using System;
using System.Text;

namespace Ronin.Core
{
    public abstract class NameManager
    {
        protected readonly ISourceReader SourceReader;

        protected NameManager(ISourceReader sourceReader)
        {
            SourceReader = sourceReader ?? throw new ArgumentNullException(nameof(sourceReader));
        }

        protected string ReadString()
        {
            var sb = new StringBuilder();

            while (SourceReader.CurrentChar != null && IsValidCharacterForNames(SourceReader.CurrentChar.Value))
            {
                sb.Append(SourceReader.CurrentChar);
                SourceReader.NextChar();
            }

            return sb.ToString();
        }

        public abstract bool IsKeyword(string keyword);
        public abstract bool IsValidCharacterForNames(char c);
        public abstract bool IsValidStartOfName(char c);
        public abstract Token ReadName();
        public abstract string GetStringName();
    }
}
