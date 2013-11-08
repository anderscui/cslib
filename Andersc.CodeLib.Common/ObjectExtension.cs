using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Andersc.CodeLib.Common
{
    public static class ObjectExtension
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static bool In<T>(this T t, IEnumerable<T> c)
        {
            return c.Any(i => i.Equals(t));
        }

        public static bool InRange<T>(this IComparable<T> t, T min, T max)
        {
            return t.CompareTo(min) >= 0
                   && t.CompareTo(max) <= 0;
        }

        public static T Clone<T>(this T t)
        {
            return (T)CloneObject(t);
        }

        private static object CloneObject(object obj)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter(null,
                     new StreamingContext(StreamingContextStates.Clone));
                binaryFormatter.Serialize(memStream, obj);
                memStream.Seek(0, SeekOrigin.Begin);
                return binaryFormatter.Deserialize(memStream);
            }
        }

        public static bool DoesNotHaveValue<T>(this Nullable<T> nullable) where T : struct
        {
            return !nullable.HasValue;
        }
    }
}
