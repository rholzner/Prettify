namespace Prettify
{
    public interface IPrettifyActionResult
    {
        string ResultValue { get; }
        bool MoveToNextRule { get; }
    }
}
