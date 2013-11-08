using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Andersc.CodeLib.Common.Collections
{
    public abstract class Range<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private readonly T start;
        private readonly T end;

        public Range(T start, T end)
        {
            if (start.CompareTo(end) > 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.start = start;
            this.end = end;
        }

        public T Start
        {
            get { return start; }
        }

        public T End
        {
            get { return end; }
        }

        public bool Contains(T value)
        {
            return value.CompareTo(start) >= 0 && value.CompareTo(end) <= 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            T value = start;
            while (value.CompareTo(end) < 0)
            {
                yield return value;
                value = GetNextValue(value);
            }

            if (value.CompareTo(end) == 0)
            {
                yield return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected abstract T GetNextValue(T value);
    }

    public class DateTimeRange : Range<DateTime>
    {
        private readonly TimeSpan step;

        public DateTimeRange(DateTime start, DateTime end)
            : this(start, end, TimeSpan.FromDays(1)) { }

        public DateTimeRange(DateTime start, DateTime end, TimeSpan step)
            : base(start, end)
        {
            this.step = step;
        }

        protected override DateTime GetNextValue(DateTime current)
        {
            return current + step;
        }
    }

    public class Int32Range : Range<int>
    {
        private readonly int step;

        public Int32Range(int start, int end)
            : this(start, end, 1) { }

        public Int32Range(int start, int end, int step)
            : base(start, end)
        {
            this.step = step;
        }

        protected override int GetNextValue(int current)
        {
            return current + step;
        }
    }
}
