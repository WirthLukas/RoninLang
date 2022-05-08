using Ronin.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Ronin.Compiler.Scanning;

public class Scanner : IScanner
{
    private readonly List<IScannerRule> _rules = new();
    private readonly ISourceReader _sourceReader;
    private IScannerRule? _noMatchRule;
    private Token _currentToken;

    public ref readonly Token CurrentToken => ref _currentToken;

    public Scanner(ISourceReader sourceReader)
    {
        _sourceReader = sourceReader ?? throw new ArgumentNullException(nameof(sourceReader));
    }

    public IScanner AddRule(IScannerRule rule)
    {
        _rules.Add(rule ?? throw new ArgumentNullException(nameof(rule)));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IScanner AddRule<T>() where T : IScannerRule, new() => AddRule(new T());

    public IScanner AddRuleForNoMatch(IScannerRule rule)
    {
        _noMatchRule = rule ?? throw new ArgumentNullException(nameof(rule));
        return this;
    }

    public ref readonly Token NextToken()
    {
        if (_rules.Count == 0) throw new Exception("No Rules in Scanner");

        Token? result;

        do
        {
            IScannerRule? rule = _rules.FirstOrDefault(sr => sr.Match(_sourceReader));

            if (rule is null && _noMatchRule is not null)
            {
                rule = _noMatchRule;
            }

            result = rule?.GetToken(_sourceReader);
        } while (result is null || result.Value.Symbol is (uint)Symbol.NoSy);

        _currentToken = result ?? throw new Exception("Token is null");
        return ref _currentToken;
    }
}
