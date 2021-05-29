using System;

namespace Prettify
{
    public static partial class DateTimeExtension
    {
        /// <summary>
        ///  x - y
        /// </summary>
        /// <returns></returns>
        public static IPrettify<TimeSpan> Prettify(this DateTime x,DateTime y)
        {
            var t = x - y;
            return t.Prettify();
        }
    }
}
