using System;

namespace RoninLang.Compiler.Parsing
{
    public abstract partial class Parser
    {
        public static class Factory
        {
            public static bool IsReady { get; private set; }

            public static void Setup()
            {
                IsReady = true;
            }
            
            public static void Shutdown() => IsReady = false;

            public static T Create<T>() where T : Parser, new()
            {
                if (!IsReady)
                    throw new AccessViolationException("Cannot create Parser, cause Parser.Factory is not initialized");
                
                return new T();
            }
        }
    }
}