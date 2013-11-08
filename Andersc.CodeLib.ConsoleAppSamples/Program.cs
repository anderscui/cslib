using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Andersc.CodeLib.ConsoleAppSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            TextWriterTraceListener tr1 = new TextWriterTraceListener(System.Console.Out);
            Debug.Listeners.Add(tr1);

            TextWriterTraceListener tr2 = new TextWriterTraceListener(File.AppendText("debugInfo.log"));
            Debug.Listeners.Add(tr2);

            Debug.WriteLine("Debug Info");
            Trace.WriteLine("Trace Info");
            Debug.Flush();

            Console.WriteLine("name? {0}", "anders");
            Console.Read();
        }
    }
}
