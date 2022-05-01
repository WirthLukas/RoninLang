using System;
using System.Collections.Generic;

namespace Ronin.Compiler.Semantics;

public class SymbolTable
{
    private readonly List<string> _variableNames = new ();

    public bool AddVariable(string name)
    {
        ArgumentNullException.ThrowIfNull (name, nameof(name));

        if (_variableNames.Contains (name))
        {
            return false;
        }

        _variableNames.Add (name);
        return true;
    }

    public bool ExistsVariable(string name)
    {
        return _variableNames.Contains (name);
    }
}
