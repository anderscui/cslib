using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Algorithm
{
    public static class StringMatcher
    {
        private static readonly int NotFound = -1;

        // 128 for ASCII chars;
        private static readonly int AsciiShiftTableLength = 128;

        private static readonly char[] CharTable = new[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 
            'h', 'i', 'j', 'k', 'l', 'm', 'n',
            'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z',
            ' ', '\n', ',', '.', '?', '!'
        };

        private static int[] GetShiftTable(string pattern)
        {
            var table = new int[AsciiShiftTableLength];
            table.Init(pattern.Length);

            for (var i = 0; i < pattern.Length - 1; i++)
            {
                table[pattern[i]] = pattern.Length - 1 - i;
            }

            return table;
        }

        public static int[] ShiftTable(string pat)
        {
            return GetShiftTable(pat);
        }

        public static int BruteForce(string text, string pattern)
        {
            if (text == null) { throw new ArgumentNullException("text"); }
            if (pattern == null) { throw new ArgumentNullException("pattern"); }

            int n = text.Length;
            int m = pattern.Length;

            if (m == 0) { return 0; }
            // not needed, only one loop execution.
            //if (n < m) { return NotFound; }

            // both t and p have at least one char.
            for (var i = 0; i <= n - m; i++)
            {
                int j;
                for (j = 0; j < m && text[i + j] == pattern[j]; j++)
                { }

                if (j == m) { return i; }
            }

            return NotFound;
        }

        public static int BruteForce2(string text, string pattern)
        {
            if (text == null) { throw new ArgumentNullException("text"); }
            if (pattern == null) { throw new ArgumentNullException("pattern"); }

            int i, n = text.Length;
            int j, m = pattern.Length;

            if (m == 0) { return 0; }

            for (i = 0, j = 0; i < n && j < m; i++)
            {
                if (text[i] == pattern[j])
                {
                    j++;
                }
                else
                {
                    i -= j;
                    j = 0;
                }
            }
            if (j == m) { return i - m; }

            return NotFound;
        }

        public static int[] BruteForceAll(string text, string pattern)
        {
            var noMatches = new int[] { };

            if (text == null) { throw new ArgumentNullException("text"); }
            if (pattern == null) { throw new ArgumentNullException("pattern"); }

            int n = text.Length;
            int m = pattern.Length;

            if (m == 0) { return new[] { 0 }; }
            if (n < m) { return noMatches; }

            var matches = new List<int>();
            for (var i = 0; i <= n - m; i++)
            {
                int j;
                for (j = 0; j < m && text[i + j] == pattern[j]; j++)
                { }

                if (j == m)
                {
                    matches.Add(i);
                    i += (m - 1);
                }
            }

            return matches.Any()
                       ? matches.ToArray()
                       : noMatches;
        }

        public static int Horspool(string text, string pattern)
        {
            if (text == null) { throw new ArgumentNullException("text"); }
            if (pattern == null) { throw new ArgumentNullException("pattern"); }

            int n = text.Length;
            int m = pattern.Length;

            if (m == 0) { return 0; }

            var table = GetShiftTable(pattern);

            var i = m - 1;
            while (i <= n - 1)
            {
                var k = 0;
                while (k <= m - 1 && text[i - k] == pattern[m - 1 - k])
                {
                    k++;
                }
                if (k == m)
                {
                    return (i - m + 1);
                }
                else
                {
                    i += table[text[i]];
                }
            }

            return NotFound;
        }

        public static int[] GetNexts(string pat)
        {
            var len = pat.Length;
            // index ranges in 1 to len.
            var nexts = new int[len + 1];

            // current length of longest prefix
            int longest = 0;
            nexts[1] = 0;
            // check each of the number of characters matched
            for (int matched = 2; matched < len + 1; matched++)
            {
                while (longest > 0 && (pat[longest] != pat[matched - 1]))
                    longest = nexts[longest];
                if (pat[longest] == pat[matched - 1])
                    longest++;
                nexts[matched] = longest;
            }

            return nexts;
        }

        public static int KMP(string text, string pattern)
        {
            if (text == null) { throw new ArgumentNullException("text"); }
            if (pattern == null) { throw new ArgumentNullException("pattern"); }

            int n = text.Length;
            int m = pattern.Length;

            if (m == 0) { return 0; }

            var nexts = GetNexts(pattern);

            var matched = 0;
            for (int i = 0; i < n; i++)
            {
                while (matched > 0 && pattern[matched] != text[i])
                    matched = nexts[matched];
                if (pattern[matched] == text[i])
                    matched++;
                if (matched == m)
                {
                    return (i - m + 1);
                    //NOCM = nexts[NOCM];
                }
            }

            return NotFound;
        }

        public static int KmpAll(string text, string pattern)
        {
            if (text == null) { throw new ArgumentNullException("text"); }
            if (pattern == null) { throw new ArgumentNullException("pattern"); }

            int n = text.Length;
            int m = pattern.Length;

            if (m == 0) { return 0; }

            var nexts = GetNexts(pattern);

            var matched = 0;
            for (int i = 0; i < n; i++)
            {
                while (matched > 0 && pattern[matched] != text[i])
                    matched = nexts[matched];
                if (pattern[matched] == text[i])
                    matched++;
                if (matched == m)
                {
                    return (i - m + 1);
                    //NOCM = nexts[NOCM];
                }
            }

            return -1;
        }
    }
}
