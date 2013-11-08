using System;
using System.Runtime.InteropServices;

namespace Andersc.AnyRun
{
    internal class NativeMethods
    {
        public const int HWND_BROADCAST = 0xffff;
        //public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOW_ANYRUN");

        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);

        public static int RegisterWindowMessage(string format, params object[] args)
        {
            string message = String.Format(format, args);
            return RegisterWindowMessage(message);
        }
    }
}