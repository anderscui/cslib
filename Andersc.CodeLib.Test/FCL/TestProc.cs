using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.Win32;

using NUnit.Framework;

namespace Andersc.CodeLib.Tester.FCL
{
    [TestFixture]
    public class TestProc
    {
        [TestCase]
        public void OpenNewProc()
        {
            var proc = new Process();
            proc.StartInfo.FileName = "Notepad.exe";
            proc.StartInfo.Arguments = @"D:\andersc\temp.txt";
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            proc.Start();
            //proc.WaitForExit();

            Thread.Sleep(1000 * 3);
            proc.Kill();
            Console.WriteLine();
        }

        [TestCase]
        public void OpenBrowserWithUrl()
        {
            Process.Start("http://www.cnblogs.com");
        }

        [TestCase]
        public void OpenFolder()
        {
            Process.Start("explorer.exe", @"/e,/root,c:\windows");
        }

        [TestCase]
        public void OpenFolderSelectFile()
        {
            Process.Start("explorer.exe", @"/select,c:\windows\system32\calc.exe");
        }

        [TestCase]
        public void OpenFile()
        {
            Process.Start(@"D:\skydrive\res\books\The Scheme Programming Language,Fourth Edition.pdf");
        }
    }
}
