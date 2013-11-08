using System;
using System.Collections.Generic;
using System.Text;

namespace Andersc.CodeLib.Common
{
    public enum DateTimeUnit
    {
        /// <summary>
        /// one in a million of a second. abbr: mics.
        /// </summary>
        //MicroSecond

        /// <summary>
        /// one in a thousand of a second. abbr: mils.
        /// </summary>
        MilliSecond,
        /// <summary>
        /// abbr: s.
        /// </summary>
        Second,
        /// <summary>
        /// abbr: mi
        /// </summary>
        Minute,
        /// <summary>
        /// abbr: h
        /// </summary>
        Hour,
        /// <summary>
        /// abbr: d
        /// </summary>
        Day,
        /// <summary>
        /// abbr: w
        /// </summary>
        Week,
        /// <summary>
        /// abbr: m
        /// </summary>
        Month,
        /// <summary>
        /// abbr: y
        /// </summary>
        Year
    }

    public class DateTimeHelper
    {
        public const int DaysOfWeek = 7;

        /// <summary>
        /// Get a datetime unit by unit string.
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>corresponding unit.</returns>
        /// <exception cref="ArgumentException">if the unit argument is null or empty.</exception>
        /// <exception cref="Exception">if the unit string is invalid.</exception>
        public static DateTimeUnit GetDateTimeUnit(string unit)
        {
            if (string.IsNullOrEmpty(unit))
            {
                throw new ArgumentException("unit must not be null or empty.");
            }

            switch (unit.ToLower())
            {
                case "y":
                case "year":
                    return DateTimeUnit.Year;
                case "m":
                case "month":
                    return DateTimeUnit.Month;
                case "w":
                case "week":
                    return DateTimeUnit.Week;
                case "d":
                case "day":
                    return DateTimeUnit.Day;
                case "h":
                case "hour":
                    return DateTimeUnit.Hour;
                case "mi":
                case "minute":
                    return DateTimeUnit.Minute;
                case "s":
                case "second":
                    return DateTimeUnit.Second;
                case "mils":
                case "millisecond":
                    return DateTimeUnit.MilliSecond;
                default:
                    throw new ArgumentException("invalid unit string.");
            }
        }

        /// <summary>
        /// Gets the date time unit.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="unit">The unit.</param>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public static DateTime AddDateTime(DateTime original, DateTimeUnit unit, double amount)
        {
            DateTime newDateTime = original;
            switch (unit)
            {
                case DateTimeUnit.Year:
                    newDateTime = newDateTime.AddYears((int)amount);
                    break;
                case DateTimeUnit.Month:
                    newDateTime = newDateTime.AddMonths((int)amount);
                    break;
                case DateTimeUnit.Week:
                    newDateTime = newDateTime.AddDays(DaysOfWeek * amount);
                    break;
                case DateTimeUnit.Day:
                    newDateTime = newDateTime.AddDays(amount);
                    break;
                case DateTimeUnit.Hour:
                    newDateTime = newDateTime.AddHours(amount);
                    break;
                case DateTimeUnit.Minute:
                    newDateTime = newDateTime.AddMinutes(amount);
                    break;
                case DateTimeUnit.Second:
                    newDateTime = newDateTime.AddSeconds(amount);
                    break;
                case DateTimeUnit.MilliSecond:
                    newDateTime = newDateTime.AddMilliseconds(amount);
                    break;
                default:
                    break;
            }

            return newDateTime;
        }
    }
}
