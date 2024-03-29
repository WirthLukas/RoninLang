﻿using RoninLang.Core;

namespace RoninLang.Compiler.Parsing.Block
{
    public class ArrowBlockParser : Parser
    {
        protected override void ParseSpecificPart()
        {
            ParseSymbol(Symbol.Arrow);
            ParseSymbol(Factory.Create<StatementParser>());
        }
    }
}