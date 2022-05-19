using Ronin.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ronin.Compiler.Parsing.AST;

// Probably just a TokenNOde?
public class VariableAccessNode : TokenNode
{

    public VariableAccessNode(Token identifier) : base(identifier)
    {
    }

    public override string ToString() => $"{{Variable:{Token.Text}}}";
}
