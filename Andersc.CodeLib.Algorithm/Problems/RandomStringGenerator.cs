using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Andersc.CodeLib.Common;
using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Algorithm.Problems
{
    /// <summary>
    /// Generate a random string with specified length and other options.
    /// </summary>
    public class RandomStringGenerator
    {
        private static readonly string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string DigitAndLetters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public bool AutoPadding { get; set; }
        public bool IncludeNumbers { get; set; }
        public int Length { get; set; }
        
        private Random Rand { get; set; }
        private long Min { get; set; }
        private long Max { get; set; }

        private string Chars { get; set; }
        private int CharsCount { get; set; }

        public RandomStringGenerator(int length = 6, 
                                     bool autoPadding = true, 
                                     bool includeNumbers = false)
        {
            Length = length;
            AutoPadding = autoPadding;
            IncludeNumbers = includeNumbers;
            Chars = IncludeNumbers ? DigitAndLetters : Letters;
            CharsCount = IncludeNumbers ? DigitAndLetters.Length : Letters.Length;

            Rand = new Random();
            Min = 0;
            Max = (long)Math.Pow(CharsCount, Length);
        }

        public long NextIndex()
        {
            var buf = new byte[8];
            Rand.NextBytes(buf);
            var val = Math.Abs(BitConverter.ToInt64(buf, 0) % (Max - Min)) + Min;
            return val;
        }

        public string NextString()
        {
            return StringOfIndex(NextIndex());
        }

        public string StringOfIndex(long id)
        {
            if (id < 0) { return string.Empty; }

            var nums = new List<char>();
            var n = id;
            while (n > 0)
            {
                nums.Add(Chars[(int)(n % CharsCount)]);
                n = n / CharsCount;
            }

            nums.Reverse();
            var str = string.Join(string.Empty, nums);
            return AutoPadding
                ? str.PadLeft(Length, Chars[0])
                : str;
        }

        public long IndexOfString(string str)
        {
            if (str.IsNullOrEmpty())
            {
                return 0;
            }

            long sum = str[0] - Chars[0];
            for (var i = 0; i < str.Length - 1; i++)
            {
                sum = sum * CharsCount + (str[i + 1] - Chars[0]);
            }

            return sum;
        }
    }
}
