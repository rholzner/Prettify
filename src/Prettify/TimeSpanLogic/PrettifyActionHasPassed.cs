using System;

namespace Prettify
{
    public static partial class TimeSpanExtension
    {
        class PrettifyActionHasPassed : IPrettifyAction<TimeSpan>
        {
            private readonly string text;

            public PrettifyActionHasPassed(string text)
            {
                this.text = text;
            }

            public PrettifyActionHasPassed(string text,int QueNr)
            {
                this.text = text;
            }

            public int QueNr { get; set; }

            public IPrettifyActionResult Exicute(TimeSpan timeSpan)
            {
                if (timeSpan < TimeSpan.Zero)
                {
                    return new PrettifyActionResult() { ResultValue = text };
                }
                return new PrettifyActionResult() { ResultValue = "", MoveToNextRule = true };
            }
        }
    }
}
