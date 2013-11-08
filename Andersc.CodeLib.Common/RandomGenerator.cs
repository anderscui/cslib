using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common
{
    public static class RandomGenerator
    {
        public enum CharType
        {
            Digit,
            Letter,
            DigitsAndLetter,
            Punctuation,
            Symbol,
            WhiteSpace
        }

        #region Readonly Fields

        private static readonly char[] Digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        private static readonly char[] Letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 
                                  'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 
                                  'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 
                                  'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        private static readonly char[] DigitsAndLetters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                           'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 
                                           'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 
                                           'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 
                                           'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        private static readonly char[] Punctuations = { '!', '"', '#', '%', '&', '\'', '(', ')', '*', ',', '-', '.', '/', ':', ';', '?', '@', '[', '\\', ']', '_', '{', '}' };

        private static readonly char[] Symbols = { '$', '+', '<', '=', '>', '^', '`', '|', '~', '¢', '£', '¤', '¥', 
                                  '¦', '§', '¨', '©', '¬', '®', '¯', '°', '±', '´', '¶', '¸', '×', '÷' };

        private static readonly char[] WhiteSpaces = { '\t', ' ' };

        private static readonly int CharCount = Digits.Length + Letters.Length + DigitsAndLetters.Length
            + Punctuations.Length + Symbols.Length + WhiteSpaces.Length;

        private static readonly int[] CharCountArray = { Digits.Length, Letters.Length, DigitsAndLetters.Length, Punctuations.Length, Symbols.Length, WhiteSpaces.Length }; 

        #endregion

        // TODO: How to generate a random sequence good enough.
        private static readonly Random random = null;

        static RandomGenerator()
        {
            random = new Random();
        }

        public static int GetRandomInt32(int lowerBound, int upperBound)
        {
            //Random random = new Random(DateTime.Now.Millisecond);
            return random.Next(lowerBound, upperBound);
        }

        public static int[] GetRandomInt32Array(int arrayLength, int lowerBound, int upperBound)
        {
            int[] array = new int[arrayLength];
            for (int i = 0; i < arrayLength; i++)
            {
                array[i] = GetRandomInt32(lowerBound, upperBound);
            }

            return array;
        }

        public static int[] GetUniqueRandomInt32Array(int arrayLength, int lowerBound, int upperBound)
        {
            if (arrayLength > (upperBound - lowerBound)) { throw new ArgumentOutOfRangeException("arrayLength", 
                "array length must be less than equal to the difference between upperBound and lowerBound."); }

            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < arrayLength; i++)
            {
                int current = GetRandomInt32(lowerBound, upperBound);
                while (set.Contains(current))
                {
                    current = GetRandomInt32(lowerBound, upperBound);
                }
                set.Add(current);
            }

            return set.ToArray();
        }

        public static char GetRandomChar()
        {
            int index = GetRandomInt32(1, CharCount + 1);
            int arrayIndex = -1;
            int sum = 0;
            for (int i = 0; i < CharCountArray.Length; i++)
            {
                sum += CharCountArray[i];
                if (index <= sum)
                {
                    arrayIndex = i;
                    break;
                }
            }

            CharType type = (CharType)arrayIndex;
            char[] chars = null;
            switch (type)
            {
                case CharType.Digit:
                    chars = Digits;
                    break;
                case CharType.Letter:
                    chars = Letters;
                    break;
                case CharType.DigitsAndLetter:
                    chars = DigitsAndLetters;
                    break;
                case CharType.Punctuation:
                    chars = Punctuations;
                    break;
                case CharType.Symbol:
                    chars = Symbols;
                    break;
                case CharType.WhiteSpace:
                    chars = WhiteSpaces;
                    break;
                default:
                    chars = DigitsAndLetters;
                    break;
            }

            return GetRandomCharFromArray(chars);
        }

        private static char GetRandomCharFromArray(char[] array)
        {
            int index = GetRandomInt32(0, array.Length);
            return array[index];
        }
    }
}
