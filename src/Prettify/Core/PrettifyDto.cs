using System;
using System.Collections.Generic;

namespace Prettify
{
    public static partial class TimeSpanExtension
    {
        class PrettifyDto : IPrettify<TimeSpan>
        {
            public TimeSpan TimeSpan { get; set; }
            public List<IPrettifyAction<TimeSpan>> Actions { get; set; }
            public bool SilentFail { get; set; }
            public bool OnNoMatchReturnEmptyString { get; set; }
        }
    }
}
