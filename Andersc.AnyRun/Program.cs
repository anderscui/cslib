using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Andersc.AnyRun
{
    static class Program
    {
        // use a new unique name (guid).
        static Mutex mutex = new Mutex(true, "{F1B3F9FC-7CD7-42B1-A221-6C7B5CDB6951}");

        //public static readonly int WM_SHOWME = 
        //    NativeMethods.RegisterWindowMessage("WM_SHOW_ANYRUN|F1B3F9FC-7CD7-42B1-A221-6C7B5CDB6951");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
                mutex.ReleaseMutex();
            }
            else
            {
                //NativeMethods.PostMessage(
                //    (IntPtr)NativeMethods.HWND_BROADCAST,
                //    WM_SHOWME,
                //    IntPtr.Zero,
                //    IntPtr.Zero);
            }
        }
    }
}
