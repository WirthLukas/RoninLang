using System;
using RoninLang.Core.IO;

namespace RoninLang.Compiler.IO
{
    public class StringSourceReader : ISourceReader
    {
        private readonly string _source;
        private int _currentIndex;

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

        public StringSourceReader(string sourceString)
        {
            _source = sourceString ?? throw new ArgumentNullException(nameof(sourceString));
            _currentIndex = 0;
        }

        public void NextChar()
            => _currentIndex = _currentIndex >= _source.Length
                ? _currentIndex
                : _currentIndex + 1;
    }
}