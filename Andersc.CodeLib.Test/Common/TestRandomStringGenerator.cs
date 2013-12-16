using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Andersc.CodeLib.Common;
using NUnit.Framework;

using Andersc.CodeLib.Algorithm.Problems;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestRandomStringGenerator
    {
        [Test]
        public void TestStringOfIndexWithAutoPadding()
        {
            var gen = new RandomStringGenerator();
            Assert.That(gen.StringOfIndex(0), Is.EqualTo("AAAAAA"));
            Assert.That(gen.StringOfIndex(100), Is.EqualTo("AAAADW"));
            Assert.That(gen.StringOfIndex(109), Is.EqualTo("AAAAEF"));
            Assert.That(gen.StringOfIndex(54106), Is.EqualTo("AADCBA"));
            Assert.That(gen.StringOfIndex(119438), Is.EqualTo("AAGURU"));
            Assert.That(gen.StringOfIndex(32111809), Is.EqualTo("CSHARP"));
            Assert.That(gen.StringOfIndex(134529718), Is.EqualTo("LIKELY"));
            Assert.That(gen.StringOfIndex(285388380), Is.EqualTo("YANKEE"));
        }

        [Test]
        public void TestStringOfIndexWithoutAutoPadding()
        {
            var gen = new RandomStringGenerator(autoPadding:false);
            Assert.That(gen.StringOfIndex(0), Is.EqualTo(""));
            Assert.That(gen.StringOfIndex(100), Is.EqualTo("DW"));
            Assert.That(gen.StringOfIndex(109), Is.EqualTo("EF"));
            Assert.That(gen.StringOfIndex(54106), Is.EqualTo("DCBA"));
            Assert.That(gen.StringOfIndex(119438), Is.EqualTo("GURU"));
            Assert.That(gen.StringOfIndex(32111809), Is.EqualTo("CSHARP"));
            Assert.That(gen.StringOfIndex(134529718), Is.EqualTo("LIKELY"));
            Assert.That(gen.StringOfIndex(285388380), Is.EqualTo("YANKEE"));
        }

        [Test]
        public void TestIndexOfString()
        {
            var gen = new RandomStringGenerator();
            Assert.That(gen.IndexOfString("AAAA"), Is.EqualTo(0));
            Assert.That(gen.IndexOfString("AAAADW"), Is.EqualTo(100));
            Assert.That(gen.IndexOfString("DW"), Is.EqualTo(100));
            Assert.That(gen.IndexOfString("EF"), Is.EqualTo(109));
            Assert.That(gen.IndexOfString("DCBA"), Is.EqualTo(54106));
            Assert.That(gen.IndexOfString("GURU"), Is.EqualTo(119438));
            Assert.That(gen.IndexOfString("CSHARP"), Is.EqualTo(32111809));
            Assert.That(gen.IndexOfString("LIKELY"), Is.EqualTo(134529718));
            Assert.That(gen.IndexOfString("YANKEE"), Is.EqualTo(285388380));
        }

        [Test]
        public void TestGetNextString()
        {
            var indexStrs = new Dictionary<long, string>();
            
            var n = 100000;
            var gen = new RandomStringGenerator();
            for (int i = 0; i < n; i++)
            {
                var id = gen.NextIndex();
                indexStrs[id] = gen.StringOfIndex(id);
            }
            Console.WriteLine("{0} - {1}", n, indexStrs.Count);
        }

        private Dictionary<long, string> GetStringHistory()
        {
            var hist = new Dictionary<long, string>();
            
            var file_name = "id_logs.txt";
            if (File.Exists(file_name))
            {
                var lines = File.ReadAllLines(file_name);
                foreach (var line in lines)
                {
                    if (line.IsNotNullOrEmpty())
                    {
                        var parts = line.Split(':');
                        hist[Convert.ToInt64(parts[0])] = parts[1];
                    }
                }
            }

            return hist;
        }

        [Test]
        public void TestGetNextStringWithHistory()
        {
            var indexStrs = GetStringHistory();
            Console.WriteLine("{0} historical items found.", indexStrs.Count);

            var needed = 10000;
            var attempts = 0;
            var generated = 0;
            var gen = new RandomStringGenerator();
            while (generated < needed)
            {
                attempts++;

                var id = gen.NextIndex();
                if (indexStrs.ContainsKey(id))
                {
                    continue;
                }
                indexStrs[id] = gen.StringOfIndex(id);
                generated++;
            }
            
            Console.WriteLine("generated {0} ids, attempted {1} times", generated, attempts);

            var file_name = "id_logs.txt";
            if (File.Exists(file_name))
            {
                File.Delete(file_name);
            }

            var data = new StringBuilder();
            foreach (var key in indexStrs.Keys)
            {
                data.AppendFormat("{0}:{1}", key, indexStrs[key]);
                data.AppendLine();
            }
            File.WriteAllText(file_name, data.ToString());
        }
    }
}
