using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Algorithm;

namespace Andersc.CodeLib.Tester.Algorithm
{
    [TestFixture]
    public class TestWordSearchPuzzle
    {
        [Test]
        public void TestSolve()
        {
            WordSearchPuzzle puzzle = new WordSearchPuzzle("puzzle.txt", "words.txt");
            puzzle.Solve();
        }
    }
}
