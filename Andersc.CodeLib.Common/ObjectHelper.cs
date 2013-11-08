using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common
{
    public static  class ObjectHelper
    {
        public static string GetMethodName()
        {
            StackFrame[] frames = new StackTrace().GetFrames();
            return frames[1].GetMethod().Name;
        } 
    }
}
