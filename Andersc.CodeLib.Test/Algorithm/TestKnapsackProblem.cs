/*
 * Created by: Anders Cui
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Andersc.CodeLib.Tester.Algorithm
{
    // TODO: Multiple impl, find better one.
    [TestFixture]
    public class TestKnapsackProblem
    {
        [Test]
        public void TestDynamicPro()
        {
            int capacity = 16;
            int[] sizes = { 3, 4, 7, 8, 9 };
            int[] values = { 4, 5, 10, 11, 13 };
            int[] totalValue = new int[capacity];
            int[] best = new int[capacity];
            int n = values.Length;

            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < capacity; i++)
                {
                    if (i >= sizes[j])
                    {
                        if (totalValue[i] < (totalValue[i - sizes[j]] + values[j]))
                        {
                            totalValue[i] = totalValue[i - sizes[j]] + values[j];
                            best[i] = j;
                        }
                    }
                }
            }

            Console.WriteLine("The maximum value is: " + totalValue[capacity - 1]);
        }
    }
}
