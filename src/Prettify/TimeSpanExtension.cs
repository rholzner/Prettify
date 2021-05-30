using System;
using System.Collections.Generic;
using System.Linq;

namespace Prettify
{
    public static partial class TimeSpanExtension
    {
        /// <summary>
        /// Start setting up Prettify
        /// </summary>
        public static IPrettify<TimeSpan> Prettify(this TimeSpan timeSpan)
        {
            return new PrettifyDto() { TimeSpan = timeSpan, Actions = new List<IPrettifyAction<TimeSpan>>() };
        }
        /// <summary>
        /// Start setting up Prettify
        /// </summary>
        public static IPrettify<TimeSpan> Prettify(this TimeSpan timeSpan, IEnumerable<IPrettifyAction<TimeSpan>> actions)
        {
            var r = new PrettifyDto() { TimeSpan = timeSpan };
            r.Actions = new List<IPrettifyAction<TimeSpan>>();
            r.Actions.AddRange(actions);
            return r;
        }
        /// <summary>
        /// Print result of Prettify setup
        /// </summary>
        /// <returns>string value of matching rule</returns>
        public static string Print(this IPrettify<TimeSpan> prettify)
        {
            if (!prettify.Actions.Any() && !prettify.SilentFail)
            {
                throw new PrettifyException("no prettify actions has bin configured");
            }

            foreach (var action in prettify.Actions.OrderByDescending(x => x.QueNr))
            {
                var result = action.Exicute(prettify.TimeSpan);
                if (!result.MoveToNextRule)
                {
                    return result.ResultValue;
                }
            }
            if (prettify.SilentFail)
            {
                return "";
            }
            throw new PrettifyResultException("did not match any actions that was setup");
        }
        public static IPrettify<TimeSpan> UseSilentFail(this IPrettify<TimeSpan> prettify)
        {
            if (prettify is PrettifyDto dto)
            {
                dto.SilentFail = true;
            }
            return prettify;
        }

        public static IPrettify<TimeSpan> UseOnNoMatchReturnEmptyString(this IPrettify<TimeSpan> prettify)
        {
            if (prettify is PrettifyDto dto)
            {
                dto.OnNoMatchReturnEmptyString = true;
            }
            return prettify;
        }

        public static IPrettify<TimeSpan> AddCustom(this IPrettify<TimeSpan> prettify, IPrettifyAction<TimeSpan> action)
        {
            if (action.QueNr == 0)
            {
                var lastAddedItem = prettify.Actions.Where(x => x.QueNr != int.MaxValue).OrderBy(x => x.QueNr).FirstOrDefault();
                if (lastAddedItem == null)
                {
                    action.QueNr = 0;
                }
                else
                {
                    action.QueNr = lastAddedItem.QueNr + 1;
                }
            }
            prettify.Actions.Add(action);
            return prettify;
        }

        public static IPrettify<TimeSpan> HasPassed(this IPrettify<TimeSpan> prettify, string text)
        {
            prettify.AddCustom(new PrettifyActionHasPassed(text));
            return prettify;
        }

        /// <summary>
        /// set rule for only days left
        /// </summary>
        /// <param name="text">Text with place holder in it</param>
        /// <param name="placeholder">unique text to replace</param>
        /// <returns></returns>
        public static IPrettify<TimeSpan> NoMatch(this IPrettify<TimeSpan> prettify, string text)
        {
            prettify.AddCustom(new PrettifyActionHasPassed(text, int.MaxValue));
            return prettify;
        }
        private static string ToStringFunc(double s)
        {
            return s.ToString();
        }
        private static double WeekFunc(double d)
        {
            return Math.Round(d / 7, MidpointRounding.AwayFromZero);
        }

        public static IPrettify<TimeSpan> Rule(this IPrettify<TimeSpan> prettify, string text, string placeholder, Func<double, double> func, Func<double, string> ToString, TimeSpan start, TimeSpan end, PrettifyOnTimeStamp prettifyOnTotal)
        {
            ValidateTextAndPlaceHolder(prettify, text, placeholder);

            prettify.AddCustom(new PrettifyActionRule(text, placeholder, start, end, func, ToString,prettifyOnTotal));
            return prettify;
        }
        public static IPrettify<TimeSpan> Rule(this IPrettify<TimeSpan> prettify, string text, string placeholder, Func<double, string> ToString, TimeSpan start, TimeSpan end, PrettifyOnTimeStamp prettifyOnTotal)
        {
            ValidateTextAndPlaceHolder(prettify, text, placeholder);

            prettify.AddCustom(new PrettifyActionRule(text, placeholder, start, end, ToString, prettifyOnTotal));
            return prettify;
        }


        #region Validation
        private static void ValidatedLowAndTop(IPrettify<TimeSpan> prettify, int low, int top)
        {
            if (!prettify.SilentFail && (low != 0 || top != 0))
            {
                if (low == top)
                {
                    throw new PrettifySetupException("low and top can not be same value");
                }
                else if (low > top)
                {
                    throw new PrettifySetupException("low can not be bigger then top");
                }
            }
        }

        private static void ValidateTextAndPlaceHolder(IPrettify<TimeSpan> prettify, string text, string placeholder)
        {
            if (!prettify.SilentFail)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    throw new PrettifySetupException("text is null or empty / withspace");
                }

                if (string.IsNullOrWhiteSpace(placeholder))
                {
                    throw new PrettifySetupException("placeholder is null or empty / withspace");
                }
            }
        }

        #endregion
    }
}
