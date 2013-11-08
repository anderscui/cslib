using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using Velocit.RegularExpressions;

namespace Andersc.CodeLib.Common
{
    public class HtmlHelper
    {
        /// <summary>
        /// Strips HTML tags from the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string RemoveHtml(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                HtmlTagRegex regex = new HtmlTagRegex();
                return regex.Replace(text, string.Empty);
            }
            return text;
        }

        /// <summary>
        /// Strips HTML comments from the specified text
        /// </summary>
        /// <param name="text">The text</param>
        /// <returns>The text without comments</returns>
        public static string RemoveHtmlComments(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                Regex commentStripper = new Regex("(?:&lt;!--)(?:(\\w|[ ]|\\W)*)(?:--&gt;)|(?:<!--)(?:(\\w|[ ]|\\W)*)(?:-->)");
                return commentStripper.Replace(text, string.Empty);
            }
            return text;
        }

        /// <summary>
        /// Strips HTML tags and comments from the specified text
        /// </summary>
        /// <param name="text">The text</param>
        /// <returns>The text without tags and comments</returns>
        public static string RemoveHtmlAndComments(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                text = RemoveHtmlComments(text);
                text = RemoveHtml(text);
            }
            return text;
        }
    }
}
