#region Comments

// 功能：字符串相关处理的工具类。
// 作者：Anders Cui
// 日期：2007-03-26

// 最近修改：Anders Cui
// 最近修改日期：2007-04-02
// 修改内容：添加间隔字符串相关的方法。

#endregion

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Andersc.CodeLib.Common
{
    /// <summary>
    /// A helper class about string.
    /// </summary>
    public static class StringHelper
    {
        // TODO: Add more reg exes.
        private static readonly string REGEX_EMAIL = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        #region 数据类型判断相关方法 ： Number, Time, Date, Enum ...

        // TODO: Using Regular Expression

        /// <summary>
        /// 判断输入字符串是否为数字（此处以float类型来判断，注意其范围）。
        /// </summary>
        /// <param name="input">输入字符串。</param>
        /// <returns>
        /// 	<c>true</c>如果输入字符串可以转换为数字；否则，<c>false</c>。
        /// </returns>
        public static bool IsNumeric(string input)
        {
            float tempValue;
            return float.TryParse(input, out tempValue);

            //return Regex.IsMatch(text, "^\\d+$");
        }

        /// <summary>
        /// 判断输入对象是否为数字（此处以float类型来判断，注意其范围）。
        /// </summary>
        /// <param name="input">输入对象。</param>
        /// <returns>
        /// 	<c>true</c>如果输入对象可以转换为数字；否则，<c>false</c>。
        /// </returns>
        public static bool IsNumeric(object input)
        {
            string value;
            if (input == null)
            {
                value = null;
            }
            else
            {
                value = input.ToString();
            }

            float tempValue;
            return float.TryParse(value, out tempValue);
        }

        /// <summary>
        /// 判断输入字符串是否为整数（此处以int类型来判断，注意其范围）。
        /// </summary>
        /// <param name="input">输入字符串。</param>
        /// <returns>
        /// 	<c>true</c>如果输入字符串可以转换为整数；否则，<c>false</c>。
        /// </returns>
        public static bool IsInt32(string input)
        {
            int tempValue;
            return int.TryParse(input, out tempValue);
        }

        /// <summary>
        /// 判断输入对象是否为整数（此处以int类型来判断，注意其范围）。
        /// </summary>
        /// <param name="input">输入对象。</param>
        /// <returns>
        /// 	<c>true</c>如果输入对象可以转换为整数；否则，<c>false</c>。
        /// </returns>
        public static bool IsInt32(object input)
        {
            string value;
            if (input == null)
            {
                value = null;
            }
            else
            {
                value = input.ToString();
            }

            return IsInt32(value);
        }

        /// <summary>
        /// 判断输入字符串是否为日期（此处以DateTime类型来判断）。
        /// </summary>
        /// <param name="input">输入字符串。</param>
        /// <returns>
        /// 	<c>true</c>如果输入字符串可以转换为整数；否则，<c>false</c>。
        /// </returns>
        public static bool IsDateTime(string input)
        {
            DateTime tempValue;
            return DateTime.TryParse(input, out tempValue);
        }

        /// <summary>
        /// 判断输入字符串是否为日期（此处以DateTime类型来判断）。
        /// </summary>
        /// <param name="input">输入对象。</param>
        /// <returns>
        /// 	<c>true</c>如果输入字符串可以转换为整数；否则，<c>false</c>。
        /// </returns>
        public static bool IsDateTime(object input)
        {
            string value;
            if (input == null)
            {
                value = null;
            }
            else
            {
                value = input.ToString();
            }

            return IsDateTime(value);
        }

        /// <summary>
        /// 将decimal类型的数值保留2位小数，转换为字符串。
        /// </summary>
        /// <param name="value">要四舍五入的decimal值。</param>
        /// <returns>返回2个小数位的字符串，不足2位的末位补0。</returns>
        public static string CutDecimal(decimal value)
        {
            return CutDecimal(value, 2);
        }

        /// <summary>
        /// 将decimal类型的数值四舍五入，保留指定的位数，并转换为字符串。
        /// </summary>
        /// <param name="value">要四舍五入的decimal值。</param>
        /// <param name="decimals">保留的小数位数。</param>
        /// <returns>返回指定数目小数位的字符串，不足位数的末位补0。</returns>
        public static string CutDecimal(decimal value, int decimals)
        {
            value = decimal.Round(value, decimals);
            return value.ToString();
        }

        #endregion

        #region Format Methods TODO:

        public static bool IsEmailFormat(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            Regex validator = new Regex(REGEX_EMAIL);
            return validator.IsMatch(input);
        }

        #endregion

        #region 字符处理相关方法

        /// <summary>
        /// 获取指定字符的ASCII码。
        /// </summary>
        /// <param name="ch">指定字符。</param>
        /// <returns>指定字符的ASCII码。</returns>
        public static short GetASC(char ch)
        {
            return Convert.ToInt16(ch);
        }

        public static int AscW(char input)
        {
            return input;
        }

        public static int AscW(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("input can not be null.", "input");
            }
            return input[0];
        }

        public static string Hex(int number)
        {
            return number.ToString("X");
        }

        #endregion

        #region 通用操作方法

        /// <summary>
        /// 获取字符串从左边算起指定数目的子串。
        /// </summary>
        /// <param name="input">输入字符串。</param>
        /// <param name="charCount">要取出的子串的长度。</param>
        /// <returns>截取后的字符串。</returns>
        /// <example>
        /// <code>
        /// string output = Left("HelloWorld", 5); // output is : "Hello";
        /// </code>
        /// </example>
        public static string Left(string input, int charCount)
        {
            string output = input;

            //checks the parameters
            if (charCount < 0)
            {
                throw new ArgumentOutOfRangeException("charCount", charCount, "charCount必须为一个非负的整数。");
            }

            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            if (output.Length <= charCount)
            {
                return output;
            }

            return output.Substring(0, charCount);
        }

        /// <summary>
        /// 获取字符串从右边算起指定数目的子串。
        /// </summary>
        /// <param name="input">输入字符串。</param>
        /// <param name="charCount">要取出的子串的长度。</param>
        /// <returns>截取后的字符串。</returns>
        /// <example>
        /// <code>
        ///	string output = Right("HelloWorld", 5); // output is : "World";
        /// </code>
        /// </example>
        public static string Right(string input, int charCount)
        {
            string output = input;

            //checks the parameters
            if (charCount < 0)
            {
                throw new ArgumentOutOfRangeException("charCount", charCount, "charCount必须为一个非负的整数。");
            }

            if (string.IsNullOrEmpty(output))
            {
                return input;
            }

            if (output.Length <= charCount)
            {
                return output;
            }

            return output.Substring(output.Length - charCount);
        }

        /// <summary>
        /// Returns a string containing every character within a string after the 
        /// first occurrence of another string.
        /// </summary>
        /// <param name="original">Required. String expression from which the rightmost characters are returned.</param>
        /// <param name="search">The string where the end of it marks the 
        /// characters to return.  If the string is not found, the whole string is 
        /// returned.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if str or searchstring is null.</exception>
        public static string RightAfter(string original, string search)
        {
            return RightAfter(original, search, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Returns a string containing every character within a string after the 
        /// first occurrence of another string.
        /// </summary>
        /// <param name="original">Required. String expression from which the rightmost characters are returned.</param>
        /// <param name="search">The string where the end of it marks the 
        /// characters to return.  If the string is not found, the whole string is 
        /// returned.</param>
        /// <param name="comparisonType">Determines whether or not to use case sensitive search.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if str or searchstring is null.</exception>
        public static string RightAfter(string original, string search, StringComparison comparisonType)
        {
            if (original == null)
                throw new ArgumentNullException("original", "The original string may not be null.");
            if (search == null)
                throw new ArgumentNullException("search", "The searchString string may not be null.");

            //Shortcut.
            if (search.Length > original.Length || search.Length == 0)
                return original;

            int searchIndex = original.IndexOf(search, 0, comparisonType);

            if (searchIndex < 0)
                return original;

            return Right(original, original.Length - (searchIndex + search.Length));
        }

        /// <summary>
        /// Returns a string containing every character within a string before the 
        /// first occurrence of another string.
        /// </summary>
        /// <param name="str">Required. String expression from which the leftmost characters are returned.</param>
        /// <param name="search">The string where the beginning of it marks the 
        /// characters to return.  If the string is not found, the whole string is 
        /// returned.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if str or searchstring is null.</exception>
        public static string LeftBefore(string str, string search)
        {
            return LeftBefore(str, search, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Returns a string containing every character within a string before the 
        /// first occurrence of another string.
        /// </summary>
        /// <param name="original">Required. String expression from which the leftmost characters are returned.</param>
        /// <param name="search">The string where the beginning of it marks the 
        /// characters to return.  If the string is not found, the whole string is 
        /// returned.</param>
        /// <param name="comparisonType">Determines whether or not to use case sensitive search.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if str or searchstring is null.</exception>
        public static string LeftBefore(string original, string search, StringComparison comparisonType)
        {
            if (original == null)
                throw new ArgumentNullException("original", "The original string may not be null.");

            if (search == null)
                throw new ArgumentNullException("search", "Search string may not be null.");

            //Shortcut.
            if (search.Length > original.Length || search.Length == 0)
                return original;

            int searchIndex = original.IndexOf(search, 0, comparisonType);

            if (searchIndex < 0)
                return original;

            return Left(original, searchIndex);
        }

        /// <summary>
        /// 颠倒输入的字符串。
        /// </summary>
        /// <param name="input">输入的字符串。</param>
        /// <returns>颠倒后的字符串。</returns>
        public static string Reverse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            char[] chars = input.ToCharArray();
            Array.Reverse(chars);

            return new string(chars);
        }

        /// <summary>
        /// 获取一个字符串的长度（忽略编码类型）。
        /// </summary>
        /// <param name="input">输入字符串。</param>
        /// <returns>输入字符串的长度（字符的个数）。</returns>
        public static int GetLength(string input)
        {
            return GetLength(input, true);
        }

        /// <summary>
        /// 获取一个字符串的长度。
        /// </summary>
        /// <param name="input">输入字符串。</param>
        /// <param name="ignoreEncoding">指定是否忽略编码类型。</param>
        /// <returns>输入字符串的长度。</returns>
        /// <remarks>如果考虑ignoreEncoding为true，则返回字符串中字符的个数，否则返回字符串的真实长度。</remarks>
        public static int GetLength(string input, bool ignoreEncoding)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input", "input不能为null。");
            }

            if (ignoreEncoding)
            {
                return input.Length;
            }
            else
            {
                byte[] bytes = Encoding.Default.GetBytes(input);
                return bytes.Length;
            }
        }

        /// <summary>
        /// 截断字符串。 
        /// </summary>
        /// <param name="input">输入的字符串。</param>
        /// <param name="characterLimit">截取的长度。</param>
        /// <returns>截断后的字符串。</returns>
        /// <example>这个示例演示了Truncate的用法与输出结果。
        /// <code>
        /// string output = Truncate("loveyou", 4); // output is : "love"
        ///	output = Truncate("love you", 6); // output is also "love"
        /// </code>
        /// </example>
        public static string Truncate(string input, int characterLimit)
        {
            string output = input;

            if (output == null)
            {
                return null;
            }

            // Check if the string is longer than the length limitation
            if ((output.Length > characterLimit) && (characterLimit > 0))
            {
                // cut the string down to the maximum number of characters
                output = output.Substring(0, characterLimit).Trim();

                // Check if the character right after the truncate point was a space
                // if not, we are in the middle of a word and need to remove the rest of it
                // TODO: But if the index is 0, we got "" finally , use Trim()?
                if (input.Substring(output.Length, 1) != " ")
                {
                    int LastSpace = output.LastIndexOf(" ");

                    // if we found a space then, cut back to that space
                    if (LastSpace != -1)
                    {
                        output = output.Substring(0, LastSpace);
                    }
                }
            }
            return output;
        }

        /// <summary>
        /// 截断字符串并在结尾处添加省略号。
        /// </summary>
        /// <param name="input">输入的字符串。</param>
        /// <param name="charactorLimit">截取的长度。</param>
        /// <returns></returns>
        public static string TruncateWithEllipsis(string input, int charactorLimit)
        {
            if (input == null)
            {
                return null;
            }

            return Truncate(input, charactorLimit) + "...";
        }

        /// <summary>
        /// 获取';'字符间隔的字符串项。
        /// </summary>
        /// <param name="source">源字符串。</param>
        /// <param name="key">关键字。</param>
        /// <returns>如果能够找到指定的key，则返回其对应的值；否则返回null。</returns>
        /// <example>
        /// <code>
        /// string source = "id=1;name=bill;age=10";
        /// string key = "id";
        /// string value = StringHelper.GetSplitString(source, key); // value = "1";
        /// </code>
        /// </example>
        public static string SplitString(string source, string key)
        {
            string[] separator = new string[] { ";" };
            return SplitString(source, separator, key);
        }

        /// <summary>
        /// 获取指定字符间隔的字符串项。
        /// </summary>
        /// <param name="source">源字符串。</param>
        /// <param name="separator">间隔字符串。</param>
        /// <param name="key">关键字。</param>
        /// <returns>如果能够找到指定的key，则返回其对应的值；否则返回null。</returns>
        /// <example>
        /// <code>
        /// string source = "id=1;name=bill;age=10";
        /// string separator = ";";
        /// string key = "age";
        /// string value = StringHelper.GetSplitString(source, separator, key); // value = "10";
        /// </code>
        /// </example>
        public static string SplitString(string source, string separator, string key)
        {
            string[] sepas = new string[] { separator };
            return SplitString(source, sepas, key);
        }

        /// <summary>
        /// 获取指定字符数组间隔的字符串项。
        /// </summary>
        /// <param name="source">源字符串。</param>
        /// <param name="separator">间隔字符串数组。</param>
        /// <param name="key">关键字。</param>
        /// <returns>如果能够找到指定的key，则返回其对应的值；否则返回null。</returns>
        /// <example>
        /// <code>
        /// string source = "id=1;name;bill;age=10";
        /// string[] separator = new string[] { ";" };
        /// string key = "name";
        /// string value = StringHelper.GetSplitString(source, separator, key); // value = "bill";
        /// </code>
        /// </example>
        public static string SplitString(string source, string[] separator, string key)
        {
            string[] splits = source.Split(separator, StringSplitOptions.None);
            string result = null;
            foreach (string str in splits)
            {
                int index = str.IndexOf('=');
                string name = Left(str, index);
                if (name.Equals(key))
                {
                    result = str.Substring(index + 1);
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取';'字符间隔的字符串项。
        /// </summary>
        /// <param name="source">源字符串。</param>
        /// <param name="itemIndex">项的位置（基于0的索引）。</param>
        /// <returns>如果能够找到指定位置的项，则返回其对应的值；否则返回null。</returns>
        /// <example>
        /// <code>
        /// string source = "id=1;name=bill;age=10";
        /// int index = "0";
        /// string value = StringHelper.GetSplitString(source, key); // value = "1";
        /// </code>
        /// </example>
        public static string SplitString(string source, int itemIndex)
        {
            string[] separator = new string[] { ";" };
            return SplitString(source, separator, itemIndex);
        }

        /// <summary>
        /// 获取指定字符串间隔的字符串项。
        /// </summary>
        /// <param name="source">源字符串。</param>
        /// <param name="separator">间隔字符串。</param>
        /// <param name="itemIndex">项的位置（基于0的索引）。</param>
        /// <returns>如果能够找到指定位置的项，则返回其对应的值；否则返回null。</returns>
        /// <example>
        /// <code>
        /// string source = "id=1;name=bill;age=10";
        /// string separator = ";";
        /// int index = "1";
        /// string value = StringHelper.GetSplitString(source, key); // value = "bill";
        /// </code>
        /// </example>
        public static string SplitString(string source, string separator, int itemIndex)
        {
            string[] sepas = new string[] { separator };
            return SplitString(source, sepas, itemIndex);
        }

        /// <summary>
        /// 获取指定字符串数组间隔的字符串项。
        /// </summary>
        /// <param name="source">源字符串。</param>
        /// <param name="separator">间隔字符串数组。</param>
        /// <param name="itemIndex">项的位置（基于0的索引）。</param>
        /// <returns>如果能够找到指定位置的项，则返回其对应的值；否则返回null。</returns>
        /// <example>
        /// <code>
        /// string source = "id=1;name=bill;age=10";
        /// string separator = ";";
        /// int index = "1";
        /// string value = StringHelper.GetSplitString(source, key); // value = "bill";
        /// </code>
        /// </example>
        public static string SplitString(string source, string[] separator, int itemIndex)
        {
            string[] splits = source.Split(separator, StringSplitOptions.None);
            string result = null;
            if ((itemIndex >= 0) && (itemIndex < splits.Length))
            {
                string item = splits[itemIndex];
                int index = item.IndexOf('=');
                result = item.Substring(index + 1);
            }

            return result;
        }

        /// <summary>
        /// Parses a camel cased or pascal cased string and returns an array 
        /// of the words within the string.
        /// </summary>
        /// <example>
        /// The string "PascalCasing" will return an array with two 
        /// elements, "Pascal" and "Casing".
        /// </example>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string[] SplitUppercase(string source)
        {
            if (source == null)
                return new string[] { }; //Return empty array.

            if (source.Length == 0)
                return new string[] { "" };

            StringCollection words = new StringCollection();
            int wordStartIndex = 0;

            char[] letters = source.ToCharArray();
            // Skip the first letter. we don't care what case it is.
            for (int i = 1; i < letters.Length; i++)
            {
                if (char.IsUpper(letters[i]))
                {
                    //Grab everything before the current index.
                    words.Add(new String(letters, wordStartIndex, i - wordStartIndex));
                    wordStartIndex = i;
                }
            }
            //We need to have the last word.
            words.Add(new String(letters, wordStartIndex, letters.Length - wordStartIndex));

            //Copy to a string array.
            string[] wordArray = new string[words.Count];
            words.CopyTo(wordArray, 0);
            return wordArray;
        }

        /// <summary>
        /// Parses a camel cased or pascal cased string and returns a new 
        /// string with spaces between the words in the string.
        /// </summary>
        /// <example>
        /// The string "PascalCasing" will return an array with two 
        /// elements, "Pascal" and "Casing".
        /// </example>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string SplitUppercaseToString(string source)
        {
            return string.Join(" ", SplitUppercase(source));
        }

        /// <summary>
        /// Converts text to pascal case...
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string PascalCase(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text", "Cannot PascalCase null text.");

            if (text.Length == 0)
                return text;

            string[] words = text.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    string word = words[i];
                    char firstChar = char.ToUpper(word[0]);
                    words[i] = firstChar + word.Substring(1);
                }
            }
            return string.Join(string.Empty, words);
        }

        public static string SetSplitString(string source, string key, string newValue)
        {
            return SetSplitString(source, ";", key, newValue);
        }

        public static string SetSplitString(string source, string separator, string key, string newValue)
        {
            if (string.IsNullOrEmpty(source)) { return source; }

            string[] splits = source.Split(new string[] { separator }, StringSplitOptions.None);
            if (splits == null || splits.Length == 0) { return source; }

            for (int i = 0; i < splits.Length; i++)
            {
                string str = splits[i];
                int index = str.IndexOf('=');
                string name = Left(str, index);
                if (name.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                {
                    splits[i] = name + "=" + newValue;
                    break;
                }
            }
            return string.Join(separator, splits);
        }

        public static string RemoveSplitString(string source, string key)
        {
            return RemoveSplitString(source, ";", key);
        }

        public static string RemoveSplitString(string source, string separator, string key)
        {
            string[] splits = source.Split(new string[] { separator }, StringSplitOptions.None);
            if (splits == null || splits.Length == 0) { return source; }

            List<string> list = new List<string>();
            for (int i = 0; i < splits.Length; i++)
            {
                string str = splits[i];
                int index = str.IndexOf('=');
                string name = Left(str, index);

                if ((name == null) || name.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }
                list.Add(str);
            }

            return Join(list, separator);
        }

        public static string Replace(string input, string oldValue, string newValue)
        {
            if (input == null) { return input; }

            return input.Replace(oldValue, newValue);
        }

        public static string Replace(object input, string oldValue, string newValue)
        {
            if (input == null) { return null; }

            return Replace(input.ToString(), oldValue, newValue);
        }

        #endregion

        #region SQL操作相关方法

        //TODO:

        /// <summary>
        /// 将字符串中单引号替换为两个单引号，避免拼接SQL时出现问题。
        /// </summary>
        /// <param name="oldStr"></param>
        /// <returns></returns>
        public static string ReplaceSingleComma(string oldStr)
        {
            if (oldStr == null)
            {
                return string.Empty;
            }
            else
            {
                return oldStr.Trim().Replace("'", "''");
            }

        }

        /// <summary>
        /// 将字符串中单引号替换为两个单引号，然后用前后单引号包围字符串。
        /// </summary>
        /// <param name="oldStr"></param>
        /// <returns></returns>
        public static string ReplaceCommaThenWithEnclosedComma(string oldStr)
        {
            if (oldStr == null)
            {
                return null;
            }

            return string.Format("'{0}'", ReplaceSingleComma(oldStr));
        }

        /// <summary>
        /// Escapes the input string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        /// <remarks>TODO</remarks>
        public static string EscapeInputString(string input)
        {
            return Regex.Replace(input, @"[^\w\.@-]", "");
        }

        //TODO: 保留字，转义字符的处理
        #endregion SQL操作相关方法

        #region 中文字符操作相关方法

        private static int MinChineseCode = 19968;
        private static int MaxChineseCode = 40869;
        private static int ChineseCharsCount = 20902;

        #region 中文字符首字母集合

        /// <summary>
        /// 汉字拼音首字母列表 本列表包含了20902个汉字,用于配合 ToChineseSpell 函数使用
        /// 收录的字符的Unicode编码范围为19968至40869
        /// </summary>
        private static string chineseFirstChars =
            "YDYQSXMWZSSXJBYMGCCZQPSSQBYCDSCDQLDYLYBSSJGYZZJJFKCCLZDHWDWZJLJPFYYNWJJTMYHZWZHFLZPPQHGSCYYYNJQYXXGJ"
            + "HHSDSJNKKTMOMLCRXYPSNQSECCQZGGLLYJLMYZZSECYKYYHQWJSSGGYXYZYJWWKDJHYCHMYXJTLXJYQBYXZLDWRDJRWYSRLDZJPC"
            + "BZJJBRCFTLECZSTZFXXZHTRQHYBDLYCZSSYMMRFMYQZPWWJJYFCRWFDFZQPYDDWYXKYJAWJFFXYPSFTZYHHYZYSWCJYXSCLCXXWZ"
            + "ZXNBGNNXBXLZSZSBSGPYSYZDHMDZBQBZCWDZZYYTZHBTSYYBZGNTNXQYWQSKBPHHLXGYBFMJEBJHHGQTJCYSXSTKZHLYCKGLYSMZ"
            + "XYALMELDCCXGZYRJXSDLTYZCQKCNNJWHJTZZCQLJSTSTBNXBTYXCEQXGKWJYFLZQLYHYXSPSFXLMPBYSXXXYDJCZYLLLSJXFHJXP"
            + "JBTFFYABYXBHZZBJYZLWLCZGGBTSSMDTJZXPTHYQTGLJSCQFZKJZJQNLZWLSLHDZBWJNCJZYZSQQYCQYRZCJJWYBRTWPYFTWEXCS"
            + "KDZCTBZHYZZYYJXZCFFZZMJYXXSDZZOTTBZLQWFCKSZSXFYRLNYJMBDTHJXSQQCCSBXYYTSYFBXDZTGBCNSLCYZZPSAZYZZSCJCS"
            + "HZQYDXLBPJLLMQXTYDZXSQJTZPXLCGLQTZWJBHCTSYJSFXYEJJTLBGXSXJMYJQQPFZASYJNTYDJXKJCDJSZCBARTDCLYJQMWNQNC"
            + "LLLKBYBZZSYHQQLTWLCCXTXLLZNTYLNEWYZYXCZXXGRKRMTCNDNJTSYYSSDQDGHSDBJGHRWRQLYBGLXHLGTGXBQJDZPYJSJYJCTM"
            + "RNYMGRZJCZGJMZMGXMPRYXKJNYMSGMZJYMKMFXMLDTGFBHCJHKYLPFMDXLQJJSMTQGZSJLQDLDGJYCALCMZCSDJLLNXDJFFFFJCZ"
            + "FMZFFPFKHKGDPSXKTACJDHHZDDCRRCFQYJKQCCWJDXHWJLYLLZGCFCQDSMLZPBJJPLSBCJGGDCKKDEZSQCCKJGCGKDJTJDLZYCXK"
            + "LQSCGJCLTFPCQCZGWPJDQYZJJBYJHSJDZWGFSJGZKQCCZLLPSPKJGQJHZZLJPLGJGJJTHJJYJZCZMLZLYQBGJWMLJKXZDZNJQSYZ"
            + "MLJLLJKYWXMKJLHSKJGBMCLYYMKXJQLBMLLKMDXXKWYXYSLMLPSJQQJQXYXFJTJDXMXXLLCXQBSYJBGWYMBGGBCYXPJYGPEPFGDJ"
            + "GBHBNSQJYZJKJKHXQFGQZKFHYGKHDKLLSDJQXPQYKYBNQSXQNSZSWHBSXWHXWBZZXDMNSJBSBKBBZKLYLXGWXDRWYQZMYWSJQLCJ"
            + "XXJXKJEQXSCYETLZHLYYYSDZPAQYZCMTLSHTZCFYZYXYLJSDCJQAGYSLCQLYYYSHMRQQKLDXZSCSSSYDYCJYSFSJBFRSSZQSBXXP"
            + "XJYSDRCKGJLGDKZJZBDKTCSYQPYHSTCLDJDHMXMCGXYZHJDDTMHLTXZXYLYMOHYJCLTYFBQQXPFBDFHHTKSQHZYYWCNXXCRWHOWG"
            + "YJLEGWDQCWGFJYCSNTMYTOLBYGWQWESJPWNMLRYDZSZTXYQPZGCWXHNGPYXSHMYQJXZTDPPBFYHZHTJYFDZWKGKZBLDNTSXHQEEG"
            + "ZZYLZMMZYJZGXZXKHKSTXNXXWYLYAPSTHXDWHZYMPXAGKYDXBHNHXKDPJNMYHYLPMGOCSLNZHKXXLPZZLBMLSFBHHGYGYYGGBHSC"
            + "YAQTYWLXTZQCEZYDQDQMMHTKLLSZHLSJZWFYHQSWSCWLQAZYNYTLSXTHAZNKZZSZZLAXXZWWCTGQQTDDYZTCCHYQZFLXPSLZYGPZ"
            + "SZNGLNDQTBDLXGTCTAJDKYWNSYZLJHHZZCWNYYZYWMHYCHHYXHJKZWSXHZYXLYSKQYSPSLYZWMYPPKBYGLKZHTYXAXQSYSHXASMC"
            + "HKDSCRSWJPWXSGZJLWWSCHSJHSQNHCSEGNDAQTBAALZZMSSTDQJCJKTSCJAXPLGGXHHGXXZCXPDMMHLDGTYBYSJMXHMRCPXXJZCK"
            + "ZXSHMLQXXTTHXWZFKHCCZDYTCJYXQHLXDHYPJQXYLSYYDZOZJNYXQEZYSQYAYXWYPDGXDDXSPPYZNDLTWRHXYDXZZJHTCXMCZLHP"
            + "YYYYMHZLLHNXMYLLLMDCPPXHMXDKYCYRDLTXJCHHZZXZLCCLYLNZSHZJZZLNNRLWHYQSNJHXYNTTTKYJPYCHHYEGKCTTWLGQRLGG"
            + "TGTYGYHPYHYLQYQGCWYQKPYYYTTTTLHYHLLTYTTSPLKYZXGZWGPYDSSZZDQXSKCQNMJJZZBXYQMJRTFFBTKHZKBXLJJKDXJTLBWF"
            + "ZPPTKQTZTGPDGNTPJYFALQMKGXBDCLZFHZCLLLLADPMXDJHLCCLGYHDZFGYDDGCYYFGYDXKSSEBDHYKDKDKHNAXXYBPBYYHXZQGA"
            + "FFQYJXDMLJCSQZLLPCHBSXGJYNDYBYQSPZWJLZKSDDTACTBXZDYZYPJZQSJNKKTKNJDJGYYPGTLFYQKASDNTCYHBLWDZHBBYDWJR"
            + "YGKZYHEYYFJMSDTYFZJJHGCXPLXHLDWXXJKYTCYKSSSMTWCTTQZLPBSZDZWZXGZAGYKTYWXLHLSPBCLLOQMMZSSLCMBJCSZZKYDC"
            + "ZJGQQDSMCYTZQQLWZQZXSSFPTTFQMDDZDSHDTDWFHTDYZJYQJQKYPBDJYYXTLJHDRQXXXHAYDHRJLKLYTWHLLRLLRCXYLBWSRSZZ"
            + "SYMKZZHHKYHXKSMDSYDYCJPBZBSQLFCXXXNXKXWYWSDZYQOGGQMMYHCDZTTFJYYBGSTTTYBYKJDHKYXBELHTYPJQNFXFDYKZHQKZ"
            + "BYJTZBXHFDXKDASWTAWAJLDYJSFHBLDNNTNQJTJNCHXFJSRFWHZFMDRYJYJWZPDJKZYJYMPCYZNYNXFBYTFYFWYGDBNZZZDNYTXZ"
            + "EMMQBSQEHXFZMBMFLZZSRXYMJGSXWZJSPRYDJSJGXHJJGLJJYNZZJXHGXKYMLPYYYCXYTWQZSWHWLYRJLPXSLSXMFSWWKLCTNXNY"
            + "NPSJSZHDZEPTXMYYWXYYSYWLXJQZQXZDCLEEELMCPJPCLWBXSQHFWWTFFJTNQJHJQDXHWLBYZNFJLALKYYJLDXHHYCSTYYWNRJYX"
            + "YWTRMDRQHWQCMFJDYZMHMYYXJWMYZQZXTLMRSPWWCHAQBXYGZYPXYYRRCLMPYMGKSJSZYSRMYJSNXTPLNBAPPYPYLXYYZKYNLDZY"
            + "JZCZNNLMZHHARQMPGWQTZMXXMLLHGDZXYHXKYXYCJMFFYYHJFSBSSQLXXNDYCANNMTCJCYPRRNYTYQNYYMBMSXNDLYLYSLJRLXYS"
            + "XQMLLYZLZJJJKYZZCSFBZXXMSTBJGNXYZHLXNMCWSCYZYFZLXBRNNNYLBNRTGZQYSATSWRYHYJZMZDHZGZDWYBSSCSKXSYHYTXXG"
            + "CQGXZZSHYXJSCRHMKKBXCZJYJYMKQHZJFNBHMQHYSNJNZYBKNQMCLGQHWLZNZSWXKHLJHYYBQLBFCDSXDLDSPFZPSKJYZWZXZDDX"
            + "JSMMEGJSCSSMGCLXXKYYYLNYPWWWGYDKZJGGGZGGSYCKNJWNJPCXBJJTQTJWDSSPJXZXNZXUMELPXFSXTLLXCLJXJJLJZXCTPSWX"
            + "LYDHLYQRWHSYCSQYYBYAYWJJJQFWQCQQCJQGXALDBZZYJGKGXPLTZYFXJLTPADKYQHPMATLCPDCKBMTXYBHKLENXDLEEGQDYMSAW"
            + "HZMLJTWYGXLYQZLJEEYYBQQFFNLYXRDSCTGJGXYYNKLLYQKCCTLHJLQMKKZGCYYGLLLJDZGYDHZWXPYSJBZKDZGYZZHYWYFQYTYZ"
            + "SZYEZZLYMHJJHTSMQWYZLKYYWZCSRKQYTLTDXWCTYJKLWSQZWBDCQYNCJSRSZJLKCDCDTLZZZACQQZZDDXYPLXZBQJYLZLLLQDDZ"
            + "QJYJYJZYXNYYYNYJXKXDAZWYRDLJYYYRJLXLLDYXJCYWYWNQCCLDDNYYYNYCKCZHXXCCLGZQJGKWPPCQQJYSBZZXYJSQPXJPZBSB"
            + "DSFNSFPZXHDWZTDWPPTFLZZBZDMYYPQJRSDZSQZSQXBDGCPZSWDWCSQZGMDHZXMWWFYBPDGPHTMJTHZSMMBGZMBZJCFZWFZBBZMQ"
            + "CFMBDMCJXLGPNJBBXGYHYYJGPTZGZMQBQTCGYXJXLWZKYDPDYMGCFTPFXYZTZXDZXTGKMTYBBCLBJASKYTSSQYYMSZXFJEWLXLLS"
            + "ZBQJJJAKLYLXLYCCTSXMCWFKKKBSXLLLLJYXTYLTJYYTDPJHNHNNKBYQNFQYYZBYYESSESSGDYHFHWTCJBSDZZTFDMXHCNJZYMQW"
            + "SRYJDZJQPDQBBSTJGGFBKJBXTGQHNGWJXJGDLLTHZHHYYYYYYSXWTYYYCCBDBPYPZYCCZYJPZYWCBDLFWZCWJDXXHYHLHWZZXJTC"
            + "ZLCDPXUJCZZZLYXJJTXPHFXWPYWXZPTDZZBDZCYHJHMLXBQXSBYLRDTGJRRCTTTHYTCZWMXFYTWWZCWJWXJYWCSKYBZSCCTZQNHX"
            + "NWXXKHKFHTSWOCCJYBCMPZZYKBNNZPBZHHZDLSYDDYTYFJPXYNGFXBYQXCBHXCPSXTYZDMKYSNXSXLHKMZXLYHDHKWHXXSSKQYHH"
            + "CJYXGLHZXCSNHEKDTGZXQYPKDHEXTYKCNYMYYYPKQYYYKXZLTHJQTBYQHXBMYHSQCKWWYLLHCYYLNNEQXQWMCFBDCCMLJGGXDQKT"
            + "LXKGNQCDGZJWYJJLYHHQTTTNWCHMXCXWHWSZJYDJCCDBQCDGDNYXZTHCQRXCBHZTQCBXWGQWYYBXHMBYMYQTYEXMQKYAQYRGYZSL"
            + "FYKKQHYSSQYSHJGJCNXKZYCXSBXYXHYYLSTYCXQTHYSMGSCPMMGCCCCCMTZTASMGQZJHKLOSQYLSWTMXSYQKDZLJQQYPLSYCZTCQ"
            + "QPBBQJZCLPKHQZYYXXDTDDTSJCXFFLLCHQXMJLWCJCXTSPYCXNDTJSHJWXDQQJSKXYAMYLSJHMLALYKXCYYDMNMDQMXMCZNNCYBZ"
            + "KKYFLMCHCMLHXRCJJHSYLNMTJZGZGYWJXSRXCWJGJQHQZDQJDCJJZKJKGDZQGJJYJYLXZXXCDQHHHEYTMHLFSBDJSYYSHFYSTCZQ"
            + "LPBDRFRZTZYKYWHSZYQKWDQZRKMSYNBCRXQBJYFAZPZZEDZCJYWBCJWHYJBQSZYWRYSZPTDKZPFPBNZTKLQYHBBZPNPPTYZZYBQN"
            + "YDCPJMMCYCQMCYFZZDCMNLFPBPLNGQJTBTTNJZPZBBZNJKLJQYLNBZQHKSJZNGGQSZZKYXSHPZSNBCGZKDDZQANZHJKDRTLZLSWJ"
            + "LJZLYWTJNDJZJHXYAYNCBGTZCSSQMNJPJYTYSWXZFKWJQTKHTZPLBHSNJZSYZBWZZZZLSYLSBJHDWWQPSLMMFBJDWAQYZTCJTBNN"
            + "WZXQXCDSLQGDSDPDZHJTQQPSWLYYJZLGYXYZLCTCBJTKTYCZJTQKBSJLGMGZDMCSGPYNJZYQYYKNXRPWSZXMTNCSZZYXYBYHYZAX"
            + "YWQCJTLLCKJJTJHGDXDXYQYZZBYWDLWQCGLZGJGQRQZCZSSBCRPCSKYDZNXJSQGXSSJMYDNSTZTPBDLTKZWXQWQTZEXNQCZGWEZK"
            + "SSBYBRTSSSLCCGBPSZQSZLCCGLLLZXHZQTHCZMQGYZQZNMCOCSZJMMZSQPJYGQLJYJPPLDXRGZYXCCSXHSHGTZNLZWZKJCXTCFCJ"
            + "XLBMQBCZZWPQDNHXLJCTHYZLGYLNLSZZPCXDSCQQHJQKSXZPBAJYEMSMJTZDXLCJYRYYNWJBNGZZTMJXLTBSLYRZPYLSSCNXPHLL"
            + "HYLLQQZQLXYMRSYCXZLMMCZLTZSDWTJJLLNZGGQXPFSKYGYGHBFZPDKMWGHCXMSGDXJMCJZDYCABXJDLNBCDQYGSKYDQTXDJJYXM"
            + "SZQAZDZFSLQXYJSJZYLBTXXWXQQZBJZUFBBLYLWDSLJHXJYZJWTDJCZFQZQZZDZSXZZQLZCDZFJHYSPYMPQZMLPPLFFXJJNZZYLS"
            + "JEYQZFPFZKSYWJJJHRDJZZXTXXGLGHYDXCSKYSWMMZCWYBAZBJKSHFHJCXMHFQHYXXYZFTSJYZFXYXPZLCHMZMBXHZZSXYFYMNCW"
            + "DABAZLXKTCSHHXKXJJZJSTHYGXSXYYHHHJWXKZXSSBZZWHHHCWTZZZPJXSNXQQJGZYZYWLLCWXZFXXYXYHXMKYYSWSQMNLNAYCYS"
            + "PMJKHWCQHYLAJJMZXHMMCNZHBHXCLXTJPLTXYJHDYYLTTXFSZHYXXSJBJYAYRSMXYPLCKDUYHLXRLNLLSTYZYYQYGYHHSCCSMZCT"
            + "ZQXKYQFPYYRPFFLKQUNTSZLLZMWWTCQQYZWTLLMLMPWMBZSSTZRBPDDTLQJJBXZCSRZQQYGWCSXFWZLXCCRSZDZMCYGGDZQSGTJS"
            + "WLJMYMMZYHFBJDGYXCCPSHXNZCSBSJYJGJMPPWAFFYFNXHYZXZYLREMZGZCYZSSZDLLJCSQFNXZKPTXZGXJJGFMYYYSNBTYLBNLH"
            + "PFZDCYFBMGQRRSSSZXYSGTZRNYDZZCDGPJAFJFZKNZBLCZSZPSGCYCJSZLMLRSZBZZLDLSLLYSXSQZQLYXZLSKKBRXBRBZCYCXZZ"
            + "ZEEYFGKLZLYYHGZSGZLFJHGTGWKRAAJYZKZQTSSHJJXDCYZUYJLZYRZDQQHGJZXSSZBYKJPBFRTJXLLFQWJHYLQTYMBLPZDXTZYG"
            + "BDHZZRBGXHWNJTJXLKSCFSMWLSDQYSJTXKZSCFWJLBXFTZLLJZLLQBLSQMQQCGCZFPBPHZCZJLPYYGGDTGWDCFCZQYYYQYSSCLXZ"
            + "SKLZZZGFFCQNWGLHQYZJJCZLQZZYJPJZZBPDCCMHJGXDQDGDLZQMFGPSYTSDYFWWDJZJYSXYYCZCYHZWPBYKXRYLYBHKJKSFXTZJ"
            + "MMCKHLLTNYYMSYXYZPYJQYCSYCWMTJJKQYRHLLQXPSGTLYYCLJSCPXJYZFNMLRGJJTYZBXYZMSJYJHHFZQMSYXRSZCWTLRTQZSST"
            + "KXGQKGSPTGCZNJSJCQCXHMXGGZTQYDJKZDLBZSXJLHYQGGGTHQSZPYHJHHGYYGKGGCWJZZYLCZLXQSFTGZSLLLMLJSKCTBLLZZSZ"
            + "MMNYTPZSXQHJCJYQXYZXZQZCPSHKZZYSXCDFGMWQRLLQXRFZTLYSTCTMJCXJJXHJNXTNRZTZFQYHQGLLGCXSZSJDJLJCYDSJTLNY"
            + "XHSZXCGJZYQPYLFHDJSBPCCZHJJJQZJQDYBSSLLCMYTTMQTBHJQNNYGKYRQYQMZGCJKPDCGMYZHQLLSLLCLMHOLZGDYYFZSLJCQZ"
            + "LYLZQJESHNYLLJXGJXLYSYYYXNBZLJSSZCQQCJYLLZLTJYLLZLLBNYLGQCHXYYXOXCXQKYJXXXYKLXSXXYQXCYKQXQCSGYXXYQXY"
            + "GYTQOHXHXPYXXXULCYEYCHZZCBWQBBWJQZSCSZSSLZYLKDESJZWMYMCYTSDSXXSCJPQQSQYLYYZYCMDJDZYWCBTJSYDJKCYDDJLB"
            + "DJJSODZYSYXQQYXDHHGQQYQHDYXWGMMMAJDYBBBPPBCMUUPLJZSMTXERXJMHQNUTPJDCBSSMSSSTKJTSSMMTRCPLZSZMLQDSDMJM"
            + "QPNQDXCFYNBFSDQXYXHYAYKQYDDLQYYYSSZBYDSLNTFQTZQPZMCHDHCZCWFDXTMYQSPHQYYXSRGJCWTJTZZQMGWJJTJHTQJBBHWZ"
            + "PXXHYQFXXQYWYYHYSCDYDHHQMNMTMWCPBSZPPZZGLMZFOLLCFWHMMSJZTTDHZZYFFYTZZGZYSKYJXQYJZQBHMBZZLYGHGFMSHPZF"
            + "ZSNCLPBQSNJXZSLXXFPMTYJYGBXLLDLXPZJYZJYHHZCYWHJYLSJEXFSZZYWXKZJLUYDTMLYMQJPWXYHXSKTQJEZRPXXZHHMHWQPW"
            + "QLYJJQJJZSZCPHJLCHHNXJLQWZJHBMZYXBDHHYPZLHLHLGFWLCHYYTLHJXCJMSCPXSTKPNHQXSRTYXXTESYJCTLSSLSTDLLLWWYH"
            + "DHRJZSFGXTSYCZYNYHTDHWJSLHTZDQDJZXXQHGYLTZPHCSQFCLNJTCLZPFSTPDYNYLGMJLLYCQHYSSHCHYLHQYQTMZYPBYWRFQYK"
            + "QSYSLZDQJMPXYYSSRHZJNYWTQDFZBWWTWWRXCWHGYHXMKMYYYQMSMZHNGCEPMLQQMTCWCTMMPXJPJJHFXYYZSXZHTYBMSTSYJTTQ"
            + "QQYYLHYNPYQZLCYZHZWSMYLKFJXLWGXYPJYTYSYXYMZCKTTWLKSMZSYLMPWLZWXWQZSSAQSYXYRHSSNTSRAPXCPWCMGDXHXZDZYF"
            + "JHGZTTSBJHGYZSZYSMYCLLLXBTYXHBBZJKSSDMALXHYCFYGMQYPJYCQXJLLLJGSLZGQLYCJCCZOTYXMTMTTLLWTGPXYMZMKLPSZZ"
            + "ZXHKQYSXCTYJZYHXSHYXZKXLZWPSQPYHJWPJPWXQQYLXSDHMRSLZZYZWTTCYXYSZZSHBSCCSTPLWSSCJCHNLCGCHSSPHYLHFHHXJ"
            + "SXYLLNYLSZDHZXYLSXLWZYKCLDYAXZCMDDYSPJTQJZLNWQPSSSWCTSTSZLBLNXSMNYYMJQBQHRZWTYYDCHQLXKPZWBGQYBKFCMZW"
            + "PZLLYYLSZYDWHXPSBCMLJBSCGBHXLQHYRLJXYSWXWXZSLDFHLSLYNJLZYFLYJYCDRJLFSYZFSLLCQYQFGJYHYXZLYLMSTDJCYHBZ"
            + "LLNWLXXYGYYHSMGDHXXHHLZZJZXCZZZCYQZFNGWPYLCPKPYYPMCLQKDGXZGGWQBDXZZKZFBXXLZXJTPJPTTBYTSZZDWSLCHZHSLT"
            + "YXHQLHYXXXYYZYSWTXZKHLXZXZPYHGCHKCFSYHUTJRLXFJXPTZTWHPLYXFCRHXSHXKYXXYHZQDXQWULHYHMJTBFLKHTXCWHJFWJC"
            + "FPQRYQXCYYYQYGRPYWSGSUNGWCHKZDXYFLXXHJJBYZWTSXXNCYJJYMSWZJQRMHXZWFQSYLZJZGBHYNSLBGTTCSYBYXXWXYHXYYXN"
            + "SQYXMQYWRGYQLXBBZLJSYLPSYTJZYHYZAWLRORJMKSCZJXXXYXCHDYXRYXXJDTSQFXLYLTSFFYXLMTYJMJUYYYXLTZCSXQZQHZXL"
            + "YYXZHDNBRXXXJCTYHLBRLMBRLLAXKYLLLJLYXXLYCRYLCJTGJCMTLZLLCYZZPZPCYAWHJJFYBDYYZSMPCKZDQYQPBPCJPDCYZMDP"
            + "BCYYDYCNNPLMTMLRMFMMGWYZBSJGYGSMZQQQZTXMKQWGXLLPJGZBQCDJJJFPKJKCXBLJMSWMDTQJXLDLPPBXCWRCQFBFQJCZAHZG"
            + "MYKPHYYHZYKNDKZMBPJYXPXYHLFPNYYGXJDBKXNXHJMZJXSTRSTLDXSKZYSYBZXJLXYSLBZYSLHXJPFXPQNBYLLJQKYGZMCYZZYM"
            + "CCSLCLHZFWFWYXZMWSXTYNXJHPYYMCYSPMHYSMYDYSHQYZCHMJJMZCAAGCFJBBHPLYZYLXXSDJGXDHKXXTXXNBHRMLYJSLTXMRHN"
            + "LXQJXYZLLYSWQGDLBJHDCGJYQYCMHWFMJYBMBYJYJWYMDPWHXQLDYGPDFXXBCGJSPCKRSSYZJMSLBZZJFLJJJLGXZGYXYXLSZQYX"
            + "BEXYXHGCXBPLDYHWETTWWCJMBTXCHXYQXLLXFLYXLLJLSSFWDPZSMYJCLMWYTCZPCHQEKCQBWLCQYDPLQPPQZQFJQDJHYMMCXTXD"
            + "RMJWRHXCJZYLQXDYYNHYYHRSLSRSYWWZJYMTLTLLGTQCJZYABTCKZCJYCCQLJZQXALMZYHYWLWDXZXQDLLQSHGPJFJLJHJABCQZD"
            + "JGTKHSSTCYJLPSWZLXZXRWGLDLZRLZXTGSLLLLZLYXXWGDZYGBDPHZPBRLWSXQBPFDWOFMWHLYPCBJCCLDMBZPBZZLCYQXLDOMZB"
            + "LZWPDWYYGDSTTHCSQSCCRSSSYSLFYBFNTYJSZDFNDPDHDZZMBBLSLCMYFFGTJJQWFTMTPJWFNLBZCMMJTGBDZLQLPYFHYYMJYLSD"
            + "CHDZJWJCCTLJCLDTLJJCPDDSQDSSZYBNDBJLGGJZXSXNLYCYBJXQYCBYLZCFZPPGKCXZDZFZTJJFJSJXZBNZYJQTTYJYHTYCZHYM"
            + "DJXTTMPXSPLZCDWSLSHXYPZGTFMLCJTYCBPMGDKWYCYZCDSZZYHFLYCTYGWHKJYYLSJCXGYWJCBLLCSNDDBTZBSCLYZCZZSSQDLL"
            + "MQYYHFSLQLLXFTYHABXGWNYWYYPLLSDLDLLBJCYXJZMLHLJDXYYQYTDLLLBUGBFDFBBQJZZMDPJHGCLGMJJPGAEHHBWCQXAXHHHZ"
            + "CHXYPHJAXHLPHJPGPZJQCQZGJJZZUZDMQYYBZZPHYHYBWHAZYJHYKFGDPFQSDLZMLJXKXGALXZDAGLMDGXMWZQYXXDXXPFDMMSSY"
            + "MPFMDMMKXKSYZYSHDZKXSYSMMZZZMSYDNZZCZXFPLSTMZDNMXCKJMZTYYMZMZZMSXHHDCZJEMXXKLJSTLWLSQLYJZLLZJSSDPPMH"
            + "NLZJCZYHMXXHGZCJMDHXTKGRMXFWMCGMWKDTKSXQMMMFZZYDKMSCLCMPCGMHSPXQPZDSSLCXKYXTWLWJYAHZJGZQMCSNXYYMMPML"
            + "KJXMHLMLQMXCTKZMJQYSZJSYSZHSYJZJCDAJZYBSDQJZGWZQQXFKDMSDJLFWEHKZQKJPEYPZYSZCDWYJFFMZZYLTTDZZEFMZLBNP"
            + "PLPLPEPSZALLTYLKCKQZKGENQLWAGYXYDPXLHSXQQWQCQXQCLHYXXMLYCCWLYMQYSKGCHLCJNSZKPYZKCQZQLJPDMDZHLASXLBYD"
            + "WQLWDNBQCRYDDZTJYBKBWSZDXDTNPJDTCTQDFXQQMGNXECLTTBKPWSLCTYQLPWYZZKLPYGZCQQPLLKCCYLPQMZCZQCLJSLQZDJXL"
            + "DDHPZQDLJJXZQDXYZQKZLJCYQDYJPPYPQYKJYRMPCBYMCXKLLZLLFQPYLLLMBSGLCYSSLRSYSQTMXYXZQZFDZUYSYZTFFMZZSMZQ"
            + "HZSSCCMLYXWTPZGXZJGZGSJSGKDDHTQGGZLLBJDZLCBCHYXYZHZFYWXYZYMSDBZZYJGTSMTFXQYXQSTDGSLNXDLRYZZLRYYLXQHT"
            + "XSRTZNGZXBNQQZFMYKMZJBZYMKBPNLYZPBLMCNQYZZZSJZHJCTZKHYZZJRDYZHNPXGLFZTLKGJTCTSSYLLGZRZBBQZZKLPKLCZYS"
            + "SUYXBJFPNJZZXCDWXZYJXZZDJJKGGRSRJKMSMZJLSJYWQSKYHQJSXPJZZZLSNSHRNYPZTWCHKLPSRZLZXYJQXQKYSJYCZTLQZYBB"
            + "YBWZPQDWWYZCYTJCJXCKCWDKKZXSGKDZXWWYYJQYYTCYTDLLXWKCZKKLCCLZCQQDZLQLCSFQCHQHSFSMQZZLNBJJZBSJHTSZDYSJ"
            + "QJPDLZCDCWJKJZZLPYCGMZWDJJBSJQZSYZYHHXJPBJYDSSXDZNCGLQMBTSFSBPDZDLZNFGFJGFSMPXJQLMBLGQCYYXBQKDJJQYRF"
            + "KZTJDHCZKLBSDZCFJTPLLJGXHYXZCSSZZXSTJYGKGCKGYOQXJPLZPBPGTGYJZGHZQZZLBJLSQFZGKQQJZGYCZBZQTLDXRJXBSXXP"
            + "ZXHYZYCLWDXJJHXMFDZPFZHQHQMQGKSLYHTYCGFRZGNQXCLPDLBZCSCZQLLJBLHBZCYPZZPPDYMZZSGYHCKCPZJGSLJLNSCDSLDL"
            + "XBMSTLDDFJMKDJDHZLZXLSZQPQPGJLLYBDSZGQLBZLSLKYYHZTTNTJYQTZZPSZQZTLLJTYYLLQLLQYZQLBDZLSLYYZYMDFSZSNHL"
            + "XZNCZQZPBWSKRFBSYZMTHBLGJPMCZZLSTLXSHTCSYZLZBLFEQHLXFLCJLYLJQCBZLZJHHSSTBRMHXZHJZCLXFNBGXGTQJCZTMSFZ"
            + "KJMSSNXLJKBHSJXNTNLZDNTLMSJXGZJYJCZXYJYJWRWWQNZTNFJSZPZSHZJFYRDJSFSZJZBJFZQZZHZLXFYSBZQLZSGYFTZDCSZX"
            + "ZJBQMSZKJRHYJZCKMJKHCHGTXKXQGLXPXFXTRTYLXJXHDTSJXHJZJXZWZLCQSBTXWXGXTXXHXFTSDKFJHZYJFJXRZSDLLLTQSQQZ"
            + "QWZXSYQTWGWBZCGZLLYZBCLMQQTZHZXZXLJFRMYZFLXYSQXXJKXRMQDZDMMYYBSQBHGZMWFWXGMXLZPYYTGZYCCDXYZXYWGSYJYZ"
            + "NBHPZJSQSYXSXRTFYZGRHZTXSZZTHCBFCLSYXZLZQMZLMPLMXZJXSFLBYZMYQHXJSXRXSQZZZSSLYFRCZJRCRXHHZXQYDYHXSJJH"
            + "ZCXZBTYNSYSXJBQLPXZQPYMLXZKYXLXCJLCYSXXZZLXDLLLJJYHZXGYJWKJRWYHCPSGNRZLFZWFZZNSXGXFLZSXZZZBFCSYJDBRJ"
            + "KRDHHGXJLJJTGXJXXSTJTJXLYXQFCSGSWMSBCTLQZZWLZZKXJMLTMJYHSDDBXGZHDLBMYJFRZFSGCLYJBPMLYSMSXLSZJQQHJZFX"
            + "GFQFQBPXZGYYQXGZTCQWYLTLGWSGWHRLFSFGZJMGMGBGTJFSYZZGZYZAFLSSPMLPFLCWBJZCLJJMZLPJJLYMQDMYYYFBGYGYZMLY"
            + "ZDXQYXRQQQHSYYYQXYLJTYXFSFSLLGNQCYHYCWFHCCCFXPYLYPLLZYXXXXXKQHHXSHJZCFZSCZJXCPZWHHHHHAPYLQALPQAFYHXD"
            + "YLUKMZQGGGDDESRNNZLTZGCHYPPYSQJJHCLLJTOLNJPZLJLHYMHEYDYDSQYCDDHGZUNDZCLZYZLLZNTNYZGSLHSLPJJBDGWXPCDU"
            + "TJCKLKCLWKLLCASSTKZZDNQNTTLYYZSSYSSZZRYLJQKCQDHHCRXRZYDGRGCWCGZQFFFPPJFZYNAKRGYWYQPQXXFKJTSZZXSWZDDF"
            + "BBXTBGTZKZNPZZPZXZPJSZBMQHKCYXYLDKLJNYPKYGHGDZJXXEAHPNZKZTZCMXCXMMJXNKSZQNMNLWBWWXJKYHCPSTMCSQTZJYXT"
            + "PCTPDTNNPGLLLZSJLSPBLPLQHDTNJNLYYRSZFFJFQWDPHZDWMRZCCLODAXNSSNYZRESTYJWJYJDBCFXNMWTTBYLWSTSZGYBLJPXG"
            + "LBOCLHPCBJLTMXZLJYLZXCLTPNCLCKXTPZJSWCYXSFYSZDKNTLBYJCYJLLSTGQCBXRYZXBXKLYLHZLQZLNZCXWJZLJZJNCJHXMNZ"
            + "ZGJZZXTZJXYCYYCXXJYYXJJXSSSJSTSSTTPPGQTCSXWZDCSYFPTFBFHFBBLZJCLZZDBXGCXLQPXKFZFLSYLTUWBMQJHSZBMDDBCY"
            + "SCCLDXYCDDQLYJJWMQLLCSGLJJSYFPYYCCYLTJANTJJPWYCMMGQYYSXDXQMZHSZXPFTWWZQSWQRFKJLZJQQYFBRXJHHFWJJZYQAZ"
            + "MYFRHCYYBYQWLPEXCCZSTYRLTTDMQLYKMBBGMYYJPRKZNPBSXYXBHYZDJDNGHPMFSGMWFZMFQMMBCMZZCJJLCNUXYQLMLRYGQZCY"
            + "XZLWJGCJCGGMCJNFYZZJHYCPRRCMTZQZXHFQGTJXCCJEAQCRJYHPLQLSZDJRBCQHQDYRHYLYXJSYMHZYDWLDFRYHBPYDTSSCNWBX"
            + "GLPZMLZZTQSSCPJMXXYCSJYTYCGHYCJWYRXXLFEMWJNMKLLSWTXHYYYNCMMCWJDQDJZGLLJWJRKHPZGGFLCCSCZMCBLTBHBQJXQD"
            + "SPDJZZGKGLFQYWBZYZJLTSTDHQHCTCBCHFLQMPWDSHYYTQWCNZZJTLBYMBPDYYYXSQKXWYYFLXXNCWCXYPMAELYKKJMZZZBRXYYQ"
            + "JFLJPFHHHYTZZXSGQQMHSPGDZQWBWPJHZJDYSCQWZKTXXSQLZYYMYSDZGRXCKKUJLWPYSYSCSYZLRMLQSYLJXBCXTLWDQZPCYCYK"
            + "PPPNSXFYZJJRCEMHSZMSXLXGLRWGCSTLRSXBZGBZGZTCPLUJLSLYLYMTXMTZPALZXPXJTJWTCYYZLBLXBZLQMYLXPGHDSLSSDMXM"
            + "BDZZSXWHAMLCZCPJMCNHJYSNSYGCHSKQMZZQDLLKABLWJXSFMOCDXJRRLYQZKJMYBYQLYHETFJZFRFKSRYXFJTWDSXXSYSQJYSLY"
            + "XWJHSNLXYYXHBHAWHHJZXWMYLJCSSLKYDZTXBZSYFDXGXZJKHSXXYBSSXDPYNZWRPTQZCZENYGCXQFJYKJBZMLJCMQQXUOXSLYXX"
            + "LYLLJDZBTYMHPFSTTQQWLHOKYBLZZALZXQLHZWRRQHLSTMYPYXJJXMQSJFNBXYXYJXXYQYLTHYLQYFMLKLJTMLLHSZWKZHLJMLHL"
            + "JKLJSTLQXYLMBHHLNLZXQJHXCFXXLHYHJJGBYZZKBXSCQDJQDSUJZYYHZHHMGSXCSYMXFEBCQWWRBPYYJQTYZCYQYQQZYHMWFFHG"
            + "ZFRJFCDPXNTQYZPDYKHJLFRZXPPXZDBBGZQSTLGDGYLCQMLCHHMFYWLZYXKJLYPQHSYWMQQGQZMLZJNSQXJQSYJYCBEHSXFSZPXZ"
            + "WFLLBCYYJDYTDTHWZSFJMQQYJLMQXXLLDTTKHHYBFPWTYYSQQWNQWLGWDEBZWCMYGCULKJXTMXMYJSXHYBRWFYMWFRXYQMXYSZTZ"
            + "ZTFYKMLDHQDXWYYNLCRYJBLPSXCXYWLSPRRJWXHQYPHTYDNXHHMMYWYTZCSQMTSSCCDALWZTCPQPYJLLQZYJSWXMZZMMYLMXCLMX"
            + "CZMXMZSQTZPPQQBLPGXQZHFLJJHYTJSRXWZXSCCDLXTYJDCQJXSLQYCLZXLZZXMXQRJMHRHZJBHMFLJLMLCLQNLDXZLLLPYPSYJY"
            + "SXCQQDCMQJZZXHNPNXZMEKMXHYKYQLXSXTXJYYHWDCWDZHQYYBGYBCYSCFGPSJNZDYZZJZXRZRQJJYMCANYRJTLDPPYZBSTJKXXZ"
            + "YPFDWFGZZRPYMTNGXZQBYXNBUFNQKRJQZMJEGRZGYCLKXZDSKKNSXKCLJSPJYYZLQQJYBZSSQLLLKJXTBKTYLCCDDBLSPPFYLGYD"
            + "TZJYQGGKQTTFZXBDKTYYHYBBFYTYYBCLPDYTGDHRYRNJSPTCSNYJQHKLLLZSLYDXXWBCJQSPXBPJZJCJDZFFXXBRMLAZHCSNDLBJ"
            + "DSZBLPRZTSWSBXBCLLXXLZDJZSJPYLYXXYFTFFFBHJJXGBYXJPMMMPSSJZJMTLYZJXSWXTYLEDQPJMYGQZJGDJLQJWJQLLSJGJGY"
            + "GMSCLJJXDTYGJQJQJCJZCJGDZZSXQGSJGGCXHQXSNQLZZBXHSGZXCXYLJXYXYYDFQQJHJFXDHCTXJYRXYSQTJXYEFYYSSYYJXNCY"
            + "ZXFXMSYSZXYYSCHSHXZZZGZZZGFJDLTYLNPZGYJYZYYQZPBXQBDZTZCZYXXYHHSQXSHDHGQHJHGYWSZTMZMLHYXGEBTYLZKQWYTJ"
            + "ZRCLEKYSTDBCYKQQSAYXCJXWWGSBHJYZYDHCSJKQCXSWXFLTYNYZPZCCZJQTZWJQDZZZQZLJJXLSBHPYXXPSXSHHEZTXFPTLQYZZ"
            + "XHYTXNCFZYYHXGNXMYWXTZSJPTHHGYMXMXQZXTSBCZYJYXXTYYZYPCQLMMSZMJZZLLZXGXZAAJZYXJMZXWDXZSXZDZXLEYJJZQBH"
            + "ZWZZZQTZPSXZTDSXJJJZNYAZPHXYYSRNQDTHZHYYKYJHDZXZLSWCLYBZYECWCYCRYLCXNHZYDZYDYJDFRJJHTRSQTXYXJRJHOJYN"
            + "XELXSFSFJZGHPZSXZSZDZCQZBYYKLSGSJHCZSHDGQGXYZGXCHXZJWYQWGYHKSSEQZZNDZFKWYSSTCLZSTSYMCDHJXXYWEYXCZAYD"
            + "MPXMDSXYBSQMJMZJMTZQLPJYQZCGQHXJHHLXXHLHDLDJQCLDWBSXFZZYYSCHTYTYYBHECXHYKGJPXHHYZJFXHWHBDZFYZBCAPNPG"
            + "NYDMSXHMMMMAMYNBYJTMPXYYMCTHJBZYFCGTYHWPHFTWZZEZSBZEGPFMTSKFTYCMHFLLHGPZJXZJGZJYXZSBBQSCZZLZCCSTPGXM"
            + "JSFTCCZJZDJXCYBZLFCJSYZFGSZLYBCWZZBYZDZYPSWYJZXZBDSYUXLZZBZFYGCZXBZHZFTPBGZGEJBSTGKDMFHYZZJHZLLZZGJQ"
            + "ZLSFDJSSCBZGPDLFZFZSZYZYZSYGCXSNXXCHCZXTZZLJFZGQSQYXZJQDCCZTQCDXZJYQJQCHXZTDLGSCXZSYQJQTZWLQDQZTQCHQ"
            + "QJZYEZZZPBWKDJFCJPZTYPQYQTTYNLMBDKTJZPQZQZZFPZSBNJLGYJDXJDZZKZGQKXDLPZJTCJDQBXDJQJSTCKNXBXZMSLYJCQMT"
            + "JQWWCJQNJNLLLHJCWQTBZQYDZCZPZZDZYDDCYZZZCCJTTJFZDPRRTZTJDCQTQZDTJNPLZBCLLCTZSXKJZQZPZLBZRBTJDCXFCZDB"
            + "CCJJLTQQPLDCGZDBBZJCQDCJWYNLLZYZCCDWLLXWZLXRXNTQQCZXKQLSGDFQTDDGLRLAJJTKUYMKQLLTZYTDYYCZGJWYXDXFRSKS"
            + "TQTENQMRKQZHHQKDLDAZFKYPBGGPZREBZZYKZZSPEGJXGYKQZZZSLYSYYYZWFQZYLZZLZHWCHKYPQGNPGBLPLRRJYXCCSYYHSFZF"
            + "YBZYYTGZXYLXCZWXXZJZBLFFLGSKHYJZEYJHLPLLLLCZGXDRZELRHGKLZZYHZLYQSZZJZQLJZFLNBHGWLCZCFJYSPYXZLZLXGCCP"
            + "ZBLLCYBBBBUBBCBPCRNNZCZYRBFSRLDCGQYYQXYGMQZWTZYTYJXYFWTEHZZJYWLCCNTZYJJZDEDPZDZTSYQJHDYMBJNYJZLXTSST"
            + "PHNDJXXBYXQTZQDDTJTDYYTGWSCSZQFLSHLGLBCZPHDLYZJYCKWTYTYLBNYTSDSYCCTYSZYYEBHEXHQDTWNYGYCLXTSZYSTQMYGZ"
            + "AZCCSZZDSLZCLZRQXYYELJSBYMXSXZTEMBBLLYYLLYTDQYSHYMRQWKFKBFXNXSBYCHXBWJYHTQBPBSBWDZYLKGZSKYHXQZJXHXJX"
            + "GNLJKZLYYCDXLFYFGHLJGJYBXQLYBXQPQGZTZPLNCYPXDJYQYDYMRBESJYYHKXXSTMXRCZZYWXYQYBMCLLYZHQYZWQXDBXBZWZMS"
            + "LPDMYSKFMZKLZCYQYCZLQXFZZYDQZPZYGYJYZMZXDZFYFYTTQTZHGSPCZMLCCYTZXJCYTJMKSLPZHYSNZLLYTPZCTZZCKTXDHXXT"
            + "QCYFKSMQCCYYAZHTJPCYLZLYJBJXTPNYLJYYNRXSYLMMNXJSMYBCSYSYLZYLXJJQYLDZLPQBFZZBLFNDXQKCZFYWHGQMRDSXYCYT"
            + "XNQQJZYYPFZXDYZFPRXEJDGYQBXRCNFYYQPGHYJDYZXGRHTKYLNWDZNTSMPKLBTHBPYSZBZTJZSZZJTYYXZPHSSZZBZCZPTQFZMY"
            + "FLYPYBBJQXZMXXDJMTSYSKKBJZXHJCKLPSMKYJZCXTMLJYXRZZQSLXXQPYZXMKYXXXJCLJPRMYYGADYSKQLSNDHYZKQXZYZTCGHZ"
            + "TLMLWZYBWSYCTBHJHJFCWZTXWYTKZLXQSHLYJZJXTMPLPYCGLTBZZTLZJCYJGDTCLKLPLLQPJMZPAPXYZLKKTKDZCZZBNZDYDYQZ"
            + "JYJGMCTXLTGXSZLMLHBGLKFWNWZHDXUHLFMKYSLGXDTWWFRJEJZTZHYDXYKSHWFZCQSHKTMQQHTZHYMJDJSKHXZJZBZZXYMPAGQM"
            + "STPXLSKLZYNWRTSQLSZBPSPSGZWYHTLKSSSWHZZLYYTNXJGMJSZSUFWNLSOZTXGXLSAMMLBWLDSZYLAKQCQCTMYCFJBSLXCLZZCL"
            + "XXKSBZQCLHJPSQPLSXXCKSLNHPSFQQYTXYJZLQLDXZQJZDYYDJNZPTUZDSKJFSLJHYLZSQZLBTXYDGTQFDBYAZXDZHZJNHHQBYKN"
            + "XJJQCZMLLJZKSPLDYCLBBLXKLELXJLBQYCXJXGCNLCQPLZLZYJTZLJGYZDZPLTQCSXFDMNYCXGBTJDCZNBGBQYQJWGKFHTNPYQZQ"
            + "GBKPBBYZMTJDYTBLSQMPSXTBNPDXKLEMYYCJYNZCTLDYKZZXDDXHQSHDGMZSJYCCTAYRZLPYLTLKXSLZCGGEXCLFXLKJRTLQJAQZ"
            + "NCMBYDKKCXGLCZJZXJHPTDJJMZQYKQSECQZDSHHADMLZFMMZBGNTJNNLGBYJBRBTMLBYJDZXLCJLPLDLPCQDHLXZLYCBLCXZZJAD"
            + "JLNZMMSSSMYBHBSQKBHRSXXJMXSDZNZPXLGBRHWGGFCXGMSKLLTSJYYCQLTSKYWYYHYWXBXQYWPYWYKQLSQPTNTKHQCWDQKTWPXX"
            + "HCPTHTWUMSSYHBWCRWXHJMKMZNGWTMLKFGHKJYLSYYCXWHYECLQHKQHTTQKHFZLDXQWYZYYDESBPKYRZPJFYYZJCEQDZZDLATZBB"
            + "FJLLCXDLMJSSXEGYGSJQXCWBXSSZPDYZCXDNYXPPZYDLYJCZPLTXLSXYZYRXCYYYDYLWWNZSAHJSYQYHGYWWAXTJZDAXYSRLTDPS"
            + "SYYFNEJDXYZHLXLLLZQZSJNYQYQQXYJGHZGZCYJCHZLYCDSHWSHJZYJXCLLNXZJJYYXNFXMWFPYLCYLLABWDDHWDXJMCXZTZPMLQ"
            + "ZHSFHZYNZTLLDYWLSLXHYMMYLMBWWKYXYADTXYLLDJPYBPWUXJMWMLLSAFDLLYFLBHHHBQQLTZJCQJLDJTFFKMMMBYTHYGDCQRDD"
            + "WRQJXNBYSNWZDBYYTBJHPYBYTTJXAAHGQDQTMYSTQXKBTZPKJLZRBEQQSSMJJBDJOTGTBXPGBKTLHQXJJJCTHXQDWJLWRFWQGWSH"
            + "CKRYSWGFTGYGBXSDWDWRFHWYTJJXXXJYZYSLPYYYPAYXHYDQKXSHXYXGSKQHYWFDDDPPLCJLQQEEWXKSYYKDYPLTJTHKJLTCYYHH"
            + "JTTPLTZZCDLTHQKZXQYSTEEYWYYZYXXYYSTTJKLLPZMCYHQGXYHSRMBXPLLNQYDQHXSXXWGDQBSHYLLPJJJTHYJKYPPTHYYKTYEZ"
            + "YENMDSHLCRPQFDGFXZPSFTLJXXJBSWYYSKSFLXLPPLBBBLBSFXFYZBSJSSYLPBBFFFFSSCJDSTZSXZRYYSYFFSYZYZBJTBCTSBSD"
            + "HRTJJBYTCXYJEYLXCBNEBJDSYXYKGSJZBXBYTFZWGENYHHTHZHHXFWGCSTBGXKLSXYWMTMBYXJSTZSCDYQRCYTWXZFHMYMCXLZNS"
            + "DJTTTXRYCFYJSBSDYERXJLJXBBDEYNJGHXGCKGSCYMBLXJMSZNSKGXFBNBPTHFJAAFXYXFPXMYPQDTZCXZZPXRSYWZDLYBBKTYQP"
            + "QJPZYPZJZNJPZJLZZFYSBTTSLMPTZRTDXQSJEHBZYLZDHLJSQMLHTXTJECXSLZZSPKTLZKQQYFSYGYWPCPQFHQHYTQXZKRSGTTSQ"
            + "CZLPTXCDYYZXSQZSLXLZMYCPCQBZYXHBSXLZDLTCDXTYLZJYYZPZYZLTXJSJXHLPMYTXCQRBLZSSFJZZTNJYTXMYJHLHPPLCYXQJ"
            + "QQKZZSCPZKSWALQSBLCCZJSXGWWWYGYKTJBBZTDKHXHKGTGPBKQYSLPXPJCKBMLLXDZSTBKLGGQKQLSBKKTFXRMDKBFTPZFRTBBR"
            + "FERQGXYJPZSSTLBZTPSZQZSJDHLJQLZBPMSMMSXLQQNHKNBLRDDNXXDHDDJCYYGYLXGZLXSYGMQQGKHBPMXYXLYTQWLWGCPBMQXC"
            + "YZYDRJBHTDJYHQSHTMJSBYPLWHLZFFNYPMHXXHPLTBQPFBJWQDBYGPNZTPFZJGSDDTQSHZEAWZZYLLTYYBWJKXXGHLFKXDJTMSZS"
            + "QYNZGGSWQSPHTLSSKMCLZXYSZQZXNCJDQGZDLFNYKLJCJLLZLMZZNHYDSSHTHZZLZZBBHQZWWYCRZHLYQQJBEYFXXXWHSRXWQHWP"
            + "SLMSSKZTTYGYQQWRSLALHMJTQJSMXQBJJZJXZYZKXBYQXBJXSHZTSFJLXMXZXFGHKZSZGGYLCLSARJYHSLLLMZXELGLXYDJYTLFB"
            + "HBPNLYZFBBHPTGJKWETZHKJJXZXXGLLJLSTGSHJJYQLQZFKCGNNDJSSZFDBCTWWSEQFHQJBSAQTGYPQLBXBMMYWXGSLZHGLZGQYF"
            + "LZBYFZJFRYSFMBYZHQGFWZSYFYJJPHZBYYZFFWODGRLMFTWLBZGYCQXCDJYGZYYYYTYTYDWEGAZYHXJLZYYHLRMGRXXZCLHNELJJ"
            + "TJTPWJYBJJBXJJTJTEEKHWSLJPLPSFYZPQQBDLQJJTYYQLYZKDKSQJYYQZLDQTGJQYZJSUCMRYQTHTEJMFCTYHYPKMHYZWJDQFHY"
            + "YXWSHCTXRLJHQXHCCYYYJLTKTTYTMXGTCJTZAYYOCZLYLBSZYWJYTSJYHBYSHFJLYGJXXTMZYYLTXXYPZLXYJZYZYYPNHMYMDYYL"
            + "BLHLSYYQQLLNJJYMSOYQBZGDLYXYLCQYXTSZEGXHZGLHWBLJHEYXTWQMAKBPQCGYSHHEGQCMWYYWLJYJHYYZLLJJYLHZYHMGSLJL"
            + "JXCJJYCLYCJPCPZJZJMMYLCQLNQLJQJSXYJMLSZLJQLYCMMHCFMMFPQQMFYLQMCFFQMMMMHMZNFHHJGTTHHKHSLNCHHYQDXTMMQD"
            + "CYZYXYQMYQYLTDCYYYZAZZCYMZYDLZFFFMMYCQZWZZMABTBYZTDMNZZGGDFTYPCGQYTTSSFFWFDTZQSSYSTWXJHXYTSXXYLBYQHW"
            + "WKXHZXWZNNZZJZJJQJCCCHYYXBZXZCYZTLLCQXYNJYCYYCYNZZQYYYEWYCZDCJYCCHYJLBTZYYCQWMPWPYMLGKDLDLGKQQBGYCHJ"
            + "XY";

        #endregion

        /// <summary>
        /// 判断指定字符是否为中文字符。
        /// </summary>
        /// <param name="ch">指定字符。</param>
        /// <returns>
        /// 	<c>true</c>如果ch为中文字符; 否则，<c>false</c>。
        /// </returns>
        public static bool IsChineseChar(char ch)
        {
            int code = Convert.ToInt32(ch);
            return (code >= MinChineseCode && code <= MaxChineseCode);
        }

        /// <summary>
        /// 将指定字符串转换为中文拼音的首字母大写。
        /// </summary>
        /// <param name="input">指定字符串。</param>
        /// <returns>各字符的首字母的大写形式拼接而成的字符串。</returns>
        public static string ToChineseSpelling(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            StringBuilder sb = new StringBuilder();
            int index = 0;
            foreach (char ch in input)
            {
                // 若是字母则直接输出，如果是数字等字符怎么办？
                //if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                if (ch < 127)
                    sb.Append(char.ToUpper(ch));
                else
                {
                    index = Convert.ToInt32(ch) - MinChineseCode;
                    if (index >= 0 && index < ChineseCharsCount)
                    {
                        sb.Append(chineseFirstChars[index]);
                    }
                }
            }

            return sb.ToString();
        }

        #endregion

        #region 字符串数组操作

        /// <summary>
        /// 在字符串数组查找目标字符串的位置。
        /// </summary>
        /// <param name="stringArray">字符串数组。</param>
        /// <param name="target">目标字符串。</param>
        /// <returns>如果数组包含目标字符串，返回其索引，否则返回－1。</returns>
        public static int IndexOfString(string[] stringArray, string target)
        {
            if (stringArray == null || stringArray.Length == 0)
            {
                return -1;
            }

            int index = -1;
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (stringArray[i].Equals(target))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public static bool IsAnyNullOrEmpty(string first, params string[] others)
        {
            return first.IsNullOrEmpty() || others.Any(s => s.IsNullOrEmpty());
        }

        public static bool IsAnyNull(string first, params string[] others)
        {
            return first.IsNull() || others.Any(s => s.IsNull());
        }

        #endregion

        #region IList<string>

        public static string ConvertFromList(IList<string> strList)
        {
            return Join(strList, ",");
        }

        /// <summary>
        /// Concatenates a specified separator string between each element of
        /// a specified string list, yielding a single concatenated string.
        /// </summary>
        /// <param name="strList">The string list.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static string Join(IList<string> strList, string separator)
        {
            if (strList == null) return null;

            string result = string.Empty;
            foreach (string s in strList)
            {
                if (string.IsNullOrEmpty(result))
                {
                    result = s;
                }
                else
                {
                    result += separator + s;
                }
            }

            return result;
        }

        #endregion
    }
}
