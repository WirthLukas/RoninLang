using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ronin.Core
{
    public interface ISourceReader
    {
        int CurrentCol { get; }
        int CurrentLine { get; }
        char? CurrentChar { get; }

        char? NextChar();
    }
}
