namespace Prettify
{
    public static partial class TimeSpanExtension
    {
        class PrettifyActionResult : IPrettifyActionResult
        {
            public string ResultValue { get; set; }
            public bool MoveToNextRule { get; set; }
        }
    }
}
