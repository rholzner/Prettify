using System;

namespace Prettify
{
    public class PrettifyException : Exception
    {
        public PrettifyException() { }
        public PrettifyException(string? message) : base(message) { }
    }
}
