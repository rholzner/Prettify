using System;

namespace Prettify
{
    public static partial class TimeSpanExtension
    {
        class PrettifyActionSpecial : IPrettifyAction<TimeSpan>
        {
            private string text;
            private readonly string placeHolder;
            private readonly int low;
            private readonly int top;
            private readonly Func<double, double> func;
            private readonly PrettifyOnTimeStamp prettifyOnTotal;
            private readonly Func<double, string> toString;

            public PrettifyActionSpecial(string text, string placeHolder, int low, int top, Func<double, double> func, PrettifyOnTimeStamp prettifyOnTotal, Func<double, string> toString)
            {
                this.text = text;
                this.placeHolder = placeHolder;
                this.low = low;
                this.top = top;
                this.func = func;
                this.prettifyOnTotal = prettifyOnTotal;
                this.toString = toString;
            }
            public int QueNr { get; set; }

            public IPrettifyActionResult Exicute(TimeSpan timeSpan)
            {
                if ((low != 0 && top != 0))
                {
                    double h = 0;
                    switch (prettifyOnTotal)
                    {
                        case PrettifyOnTimeStamp.Milliseconds:
                            if (low < timeSpan.TotalMilliseconds && timeSpan.TotalMilliseconds < top)
                                h = func(timeSpan.TotalMilliseconds);
                            break;
                        case PrettifyOnTimeStamp.Seconds:
                            if (low < timeSpan.TotalSeconds && timeSpan.TotalSeconds < top)
                                h = func(timeSpan.TotalSeconds);
                            break;
                        case PrettifyOnTimeStamp.Minutes:
                            if (low < timeSpan.TotalMinutes && timeSpan.TotalMinutes < top)
                                h = func(timeSpan.TotalMinutes);
                            break;
                        case PrettifyOnTimeStamp.Hours:
                            if (low < timeSpan.TotalHours && timeSpan.TotalHours < top)
                                h = func(timeSpan.TotalHours);
                            break;
                        case PrettifyOnTimeStamp.Days:
                            if (low < timeSpan.TotalDays && timeSpan.TotalDays < top)
                                h = func(timeSpan.TotalDays);
                            break;
                    }

                    text = text.Replace(placeHolder, toString(h));
                    return new PrettifyActionResult() { ResultValue = text };
                }
                else
                {
                    if (timeSpan.TotalDays < 0)
                    {
                        var h = Math.Round(timeSpan.TotalDays, MidpointRounding.AwayFromZero);

                        text = text.Replace(placeHolder, h.ToString());

                        return new PrettifyActionResult() { ResultValue = text };
                    }
                }


                return new PrettifyActionResult() { ResultValue = "", MoveToNextRule = true };
            }
        }
    }
}
