/*
 * Created by: Anders Cui
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Algorithm;

namespace Andersc.CodeLib.Tester.Algorithm
{
    [TestFixture]
    public class TestCoinChangeProblem
    {
        // TODO: Multiple impl, find better one.
        [Test]
        public void TestMakeChange()
        {
            int[] coins = { 1, 5, 10, 25 };
            Assert.That(CoinChangeProblem.MakeChange(coins, 5), Is.EqualTo(1));
            Assert.That(CoinChangeProblem.MakeChange(coins, 6), Is.EqualTo(2));
            Assert.That(CoinChangeProblem.MakeChange(coins, 21), Is.EqualTo(3));
        }

        [Test]
        public void TestMakeChange2()
        {
            int[] coins = { 1, 5, 10, 25 };
            Assert.That(CoinChangeProblem.MakeChange2(coins, 5), Is.EqualTo(1));
            Assert.That(CoinChangeProblem.MakeChange2(coins, 6), Is.EqualTo(2));
            Assert.That(CoinChangeProblem.MakeChange2(coins, 63), Is.EqualTo(6));
        }

        [Test]
        public void TestMakeChange3()
        {
            int[] coins = { 1, 5, 10, 21, 25 };
            Assert.That(CoinChangeProblem.MakeChange3(coins, 5), Is.EqualTo(1));
            Assert.That(CoinChangeProblem.MakeChange3(coins, 6), Is.EqualTo(2));
            Assert.That(CoinChangeProblem.MakeChange3(coins, 63), Is.EqualTo(3));
        }
    }
}
