using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

using NUnit.Framework;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Tester.Common
{
    public delegate bool NumberTester(int i);

    [TestFixture]
    public class TestExpressionTree
    {
        [Test]
        public void TestCase()
        {
        }

        public static void PrintMatchingNumbers(int from, int to, NumberTester filter)
        {
            for (int i = from; i <= to; i++)
            {
                if (filter(i))
                {
                    Console.WriteLine(i);
                }
            }
        }

        [Test]
        public void TestBasicUsage()
        {
            Func<int, int> fun = x => x + 1;
            var addition = fun(2);
            Assert.AreEqual(3, addition);

            Expression<Func<int, int>> exp = x => x + 1;
            var originalDel = exp.Compile();
            Assert.AreEqual(3, originalDel.Invoke(2));
        }

        [Test]
        public void TestDelegate()
        {
            for (int d = 1; d < 10; d++)
            {
                Console.WriteLine("Numbers divisible by {0}", d);
                PrintMatchingNumbers(1, 10,
                    delegate(int i)
                    {
                        return (i % d) == 0;
                    });
            }
        }

        [Test]
        public void TestBinaryExpression()
        {
            ConstantExpression exp1 = Expression.Constant(1);
            ConstantExpression exp2 = Expression.Constant(2);
            BinaryExpression exp12 = Expression.Add(exp1, exp2);

            ConstantExpression exp3 = Expression.Constant(3);
            BinaryExpression exp123 = Expression.Add(exp12, exp3);

            Console.WriteLine(exp123.Type);
        }

        [Test]
        public void TestLambdaExpression()
        {
            ParameterExpression expA = Expression.Parameter(typeof(double), "a");

            // Lambda body;
            MethodCallExpression expCall = Expression.Call(null,
                typeof(Math).GetMethod("Sin", BindingFlags.Static | BindingFlags.Public),
                expA);

            // Lambda signature;
            LambdaExpression lambda = Expression.Lambda(expCall, expA);

            // Expression Tree Literals
            Expression<Func<double, double>> lambda2 = a => Math.Sin(a);
        }

        [Test]
        public void TestExpTreeBasic()
        {
            Expression<Func<int, bool>> isOdd = i => (i & 1) == 1;
            
            // TODO: error, check this.
            //ObjectDumper.Write(isOdd, 1);

            // Cannot compile when using statement body.
            //Expression<Func<object, object>> identity = o => { return o; };

        }
    }
}
