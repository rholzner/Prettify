using System;

namespace Prettify
{
    public static partial class TimeSpanExtension
    {
        class PrettifyActionRule : IPrettifyAction<TimeSpan>
        {
            private string text;
            private readonly string placeHolder;
            private readonly TimeSpan start;
            private readonly TimeSpan end;
            private readonly Func<double, double> func;
            private readonly Func<double, string> toString;
            private readonly PrettifyOnTimeStamp prettifyOnTotal;

            public PrettifyActionRule(string text, string placeHolder, TimeSpan start, TimeSpan end, Func<double, double> func, Func<double, string> toString, PrettifyOnTimeStamp prettifyOnTotal)
            {
                this.text = text;
                this.placeHolder = placeHolder;
                this.start = start;
                this.end = end;
                this.func = func;
                this.toString = toString;
                this.prettifyOnTotal = prettifyOnTotal;
            }

            public PrettifyActionRule(string text, string placeHolder, TimeSpan start, TimeSpan end, Func<double, string> toString, PrettifyOnTimeStamp prettifyOnTotal)
            {
                this.text = text;
                this.placeHolder = placeHolder;
                this.start = start;
                this.end = end;
                this.toString = toString;
                this.prettifyOnTotal = prettifyOnTotal;
            }
            public int QueNr { get; set; }

            public IPrettifyActionResult Exicute(TimeSpan timeSpan)
            {
                if ((start < timeSpan && timeSpan < end))
                {

                    double h = 0;
                    switch (prettifyOnTotal)
                    {
                        case PrettifyOnTimeStamp.Milliseconds:
                            h = timeSpan.TotalMilliseconds;
                            break;
                        case PrettifyOnTimeStamp.Seconds:
                            h = timeSpan.TotalSeconds;
                            break;
                        case PrettifyOnTimeStamp.Minutes:
                            h = timeSpan.TotalMinutes;
                            break;
                        case PrettifyOnTimeStamp.Hours:
                            h = timeSpan.TotalHours;
                            break;
                        case PrettifyOnTimeStamp.Days:
                            h = timeSpan.TotalDays;
                            break;
                    }
                    if (func != null)
                    {
                        h = func(h);
                    }

                    text = text.Replace(placeHolder, h.ToString());

                    return new PrettifyActionResult() { ResultValue = text };
                }

                return new PrettifyActionResult() { ResultValue = "", MoveToNextRule = true };
            }
        }
    }
}
