namespace Ronin.Core;

public interface IScanner
{
    ref readonly Token CurrentToken { get; }

    IScanner AddRule(IScannerRule rule);
    IScanner AddRule<T>() where T : IScannerRule, new();
    IScanner AddRuleForNoMatch(IScannerRule rule);
    ref readonly Token NextToken();
}

public interface IScannerRule
{
    bool Match(ISourcePeeker sourcePeeker);
    Token? GetToken(ISourceReader sourceReader);
}
