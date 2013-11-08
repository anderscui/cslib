using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.FCL
{
    [TestFixture]
    public class TestFileIO
    {
        [Test]
        public void ReadBinaryData()
        {
            var fileName = "21dict.dat";
            var f = File.OpenRead(fileName);
            byte[] array = new byte[10];
            f.Read(array, 0, 10);

            array.PrintToConsole();
        }
    }
}
