using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using Andersc.CodeLib.WinFormsSamples.Components;
using Andersc.CodeLib.WinFormsSamples.Multithread;

namespace Andersc.CodeLib.WinFormsSamples
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isFirstInst = false;
            // this safe name contain info about current user and current process name.
            string safeName = Application.UserAppDataPath.Replace(@"\", "_");
            //string safeName = Process.GetCurrentProcess().ProcessName;
            //MessageBox.Show(safeName);
            Mutex mutex = new Mutex(true, safeName, out isFirstInst);

            //if (!isFirstInst)
            //{
            //    Mbox.ShowWarning("Another instance is running.");
            //    return;
            //}

            //if (!IsFirstInstance())
            //{
            //    Mbox.ShowWarning("Another instance is running.");
            //    return;
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // TODO: Use command line arguments;
            //CultureInfo usCulture = new CultureInfo("en-US");
            //CultureInfo cnCulture = new CultureInfo("zh-CN");

            //Thread.CurrentThread.CurrentUICulture = usCulture;

            //Application.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];

            #region Event Handlers

            //Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            //Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            #endregion

            Application.Run(new CoolBackgroundWorker());
        }

        // It seems that a method doesnot work, should place these lines to Main method.
        private static bool IsFirstInstance()
        {
            bool isFirstInst = false;
            //string safeName = Application.UserAppDataPath.Replace(@"\", "_");
            string safeName = Process.GetCurrentProcess().ProcessName;
            Mutex mutex = new Mutex(true, safeName, out isFirstInst);

            return isFirstInst;
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string message = "An error occured: " + Environment.NewLine + Environment.NewLine
                + e.Exception.Message + Environment.NewLine
                + e.Exception.StackTrace;

            Mbox.ShowError(message);
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            //MessageBox.Show("Test");
        }
    }
}
