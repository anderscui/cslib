using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Tester.Helpers
{
    /// <summary>
    /// Used to test cases specified to value types.
    /// </summary>
    internal struct MyStruct
    {
        private string msg;
        public int ID;
        public DateTime Birthday;

        public string GetMsg()
        {
            return msg;
        }

        public void SetMsg(string value)
        {
            msg = value;
        }
    }
}
