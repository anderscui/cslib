using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Extension
{
    public static class DictionaryExtension
    {
        public class DicPair
        {
            internal object Key { get; set; }
            internal object Value { get; set; }

            public DicPair(object key, object value)
            {
                Key = key;
                Value = value;
            }
        }

        public static string ToCustomString(this Hashtable dic)
        {
            List<DicPair> pairs = new List<DicPair>();
            foreach (var item in dic.Keys)
            {
                pairs.Add(new DicPair(item, dic[item]));
            }

            var pairStrings = from pair in pairs
                select string.Format("({0}, {1})", pair.Key, pair.Value);
            
            return string.Join(", ", pairStrings.ToArray());
        }

        public static string ToCustomString<TKey, TValue>(this IDictionary<TKey, TValue> dic)
        {
            var pairs = from key in dic.Keys
                        select string.Format("({0}, {1})", key, dic[key]);

            return string.Join(", ", pairs.ToArray());
        }
    }
}
