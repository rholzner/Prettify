using System;

namespace Prettify
{
    public static partial class TimeSpanExtension
    {
        class PrettifyActionDaysLeft : IPrettifyAction<TimeSpan>
        {
            private string text;
            private readonly string placeHolder;
            private readonly int low;
            private readonly int top;
            public PrettifyActionDaysLeft(string text, string placeHolder, int low, int top)
            {
                this.text = text;
                this.placeHolder = placeHolder;
                this.low = low;
                this.top = top;
            }
            public int QueNr { get; set; }

            public IPrettifyActionResult Exicute(TimeSpan timeSpan)
            {
                if ((low != 0 && top != 0) && (low < timeSpan.TotalDays && timeSpan.TotalDays < top))
                {
                    var h = Math.Round(timeSpan.TotalDays, MidpointRounding.AwayFromZero);
                    text = text.Replace(placeHolder, h.ToString());
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
