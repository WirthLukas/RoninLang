﻿namespace RoninLang.Core.ErrorHandling
{
    public interface IError
    {
        ErrorClass ErrorClass { get; }
        int LineNumber { get; set; }
        int Number { get; }
        string Message { get; }
    }
}