using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andersc.CodeLib.Stats
{
    public class Seq : IEnumerable<int>
    {
        public int From { get; set; }
        public int To { get; set; }
        public int By { get; set; }

        public Seq(int from, int to, int by)
        {
            From = from;
            To = to;
            By = by;
        }

        public IEnumerator<int> GetEnumerator()
        {
            var value = From;
            while (value <= To)
            {
                yield return value;
                value += By;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
