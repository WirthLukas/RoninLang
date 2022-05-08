using System;
using System.Collections.Generic;
using System.Linq;
using Ronin.Core;

namespace Ronin.Compiler.IO
{
    public class StringSourceReader : ISourceReader
    {
        private readonly string _source;
        private int _currentIndex = 0;

        public int CurrentCol => _currentIndex + 1;
        public int CurrentLine => 1;

        public char? CurrentChar
        {
            get
            {
                if (_currentIndex >= _source.Length)
                    return null;

                return _source[_currentIndex];
            }
        }

        public StringSourceReader(string source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public char? NextChar()
        {
            _currentIndex = _currentIndex >= _source.Length
                ? _currentIndex
                : _currentIndex + 1;

            return CurrentChar;
        }

        public char? PeekNextChar()
        {
            return _currentIndex + 1 >= _source.Length ? null : _source[_currentIndex + 1];
        }

        public char[] PeekNextChars(int amount)
        {
            return GetNextChars(amount).ToArray();

            IEnumerable<char> GetNextChars(int amount)
            {
                int finalIndex = _currentIndex + amount;
                for (int i = _currentIndex; i < finalIndex && i < _source.Length; i++)
                {
                    yield return _source[i];
                }
            }
        }
    }
}
