using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.WinFormsSamples.Components
{
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public T OldValue { get; set; }
        public T NewValue { get; set; }

        public ValueChangedEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
