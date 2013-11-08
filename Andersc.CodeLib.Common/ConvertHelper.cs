using System;
using System.Collections.Generic;
using System.Text;

namespace Andersc.CodeLib.Common
{
    /// <summary>
    /// TODO: Add generics support. e.g. Convert.To<int>("12");
    /// </summary>
    public static class ConvertHelper
    {
        private static string stringFormatWithThousandSeparator = "{0:#,0}";

        public static readonly string FORMAT_DATETIME_MMDDYYYY = "MM/dd/yyyy";
        public static readonly string FORMAT_DATETIME_DDMMYYYY = "dd/MM/yyyy";
        public static readonly string FORMAT_DATETIME_YYYYMMDD = "yyyy-MM-dd";

        public static int ToInt32(object obj)
        {
            if (obj == null) { throw new ArgumentNullException("obj", "obj can not be null."); }

            int result;
            bool canConvert = int.TryParse(obj.ToString(), out result);
            if (canConvert)
            {
                return result;
            }
            else
            {
                throw new InvalidCastException("obj can not be convert to int type.");
            }
        }

        public static decimal ToDecimal(object obj)
        {
            if (obj == null) { throw new ArgumentNullException("obj", "obj can not be null."); }

            decimal result;
            bool canConvert = decimal.TryParse(obj.ToString(), out result);
            if (canConvert)
            {
                return result;
            }
            else
            {
                throw new InvalidCastException("obj can not be convert to decimal type.");
            }
        }

        public static int? ToNullableInt32(object obj)
        {
            if (obj == null) { return null; }

            int result;
            bool canConvert = int.TryParse(obj.ToString(), out result);
            if (canConvert)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static decimal? ToNullableDecimal(object obj)
        {
            if (obj == null) { return null; }

            decimal result;
            bool canConvert = decimal.TryParse(obj.ToString(), out result);
            if (canConvert)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static double? ToNullableDouble(object obj)
        {
            if (obj == null) { return null; }

            double result;
            bool canConvert = double.TryParse(obj.ToString(), out result);
            if (canConvert)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static string NullableInt32ToString(int? input)
        {
            if (input.HasValue)
            {
                return input.Value.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string NullableDateTimeToString(DateTime? input)
        {
            if (input.HasValue)
            {
                return input.Value.ToString("MM-dd-yyyy");
            }
            else
            {
                return string.Empty;
            }
        }

        public static string NullableDecimalToString(decimal? input)
        {
            if (input.HasValue)
            {
                return input.Value.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ToStringWithThousandSeparator(int number)
        {
            return string.Format(stringFormatWithThousandSeparator, number);
        }

        public static string ToStringWithThousandSeparator(double number)
        {
            return string.Format(stringFormatWithThousandSeparator, number);
        }

        public static string ToStringWithThousandSeparator(decimal number)
        {
            return string.Format(stringFormatWithThousandSeparator, number);
        }

        public static string ToString(object input)
        {
            if (input == null) { return null; }

            return input.ToString();
        }

        public static string DateToStringMdy(DateTime date)
        {
            return date.ToString(FORMAT_DATETIME_MMDDYYYY);
        }

        public static string DateToStringYmd(DateTime date)
        {
            return date.ToString(FORMAT_DATETIME_YYYYMMDD);
        }
    }
}
