namespace Prettify
{
    public interface IPrettifyAction<T>
    {
        int QueNr { get; set; }
        IPrettifyActionResult Exicute(T timeSpan);
    }
}
