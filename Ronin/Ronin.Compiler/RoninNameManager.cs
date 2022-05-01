using System;
using System.Collections.Generic;
using Ronin.Core;

namespace Ronin.Compiler
{
    public class RoninNameManager : NameManager
    {
        public RoninNameManager(ISourceReader sourceReader) : base(sourceReader)
        {
        }

        public override bool IsValidCharacterForNames(char c) => IsValidStartOfName(c) || char.IsDigit(c);

        public override bool IsValidStartOfName(char c) => char.IsLetter(c) || c is '_' || c is '$';

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
        public override Token ReadName()
        {
            string name = base.ReadString();
            Symbol readSymbol = KeywordTable.GetSymbolOf(name);

            var token = new Token((uint) readSymbol, text: name);

            //if (readSymbol == Symbol.Identifier)
            //{

            //}

            return token;
        }

        public override string GetStringName()
        {
            throw new NotImplementedException();
        }
    }
}
