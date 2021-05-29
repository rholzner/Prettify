using System.Collections.Generic;

namespace Prettify
{
    public interface IPrettify<T>
    {
        /// <summary>
        /// if nothing matches rules return empty string, instead of default timespan ToString
        /// </summary>
        bool OnNoMatchReturnEmptyString { get; }

        /// <summary>
        /// if true throw exceptions
        /// </summary>
        bool SilentFail { get; }

        /// <summary>
        /// Timespan in focus
        /// </summary>
        T TimeSpan { get; }

        /// <summary>
        /// first matching rule format will be set
        /// </summary>
        List<IPrettifyAction<T>> Actions { get; }
    }
}
