using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.AnyRun.Parsers
{
    public abstract class AbstractParseResult<T> : IParseResult
    {
        public T Data { get; set; }
        public abstract bool Executable { get; set; }

        public virtual string DisplayInfo { get; set; }

        public virtual void Do()
        {
            if (Executable)
            {
                Action();
            }
        }

        protected abstract void Action();
    }
}
