using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using System.Linq.Expressions;

namespace Andersc.CodeLib.Tester.FCL
{
    [TestFixture]
    public class TestExpressions
    {
        [TestCase]
        public void TestExpConstruct()
        {
            ParameterExpression p = Expression.Parameter(typeof(string), "s");
            MemberExpression strLen = Expression.Property(p, "Length");
            ConstantExpression five = Expression.Constant(5);
            BinaryExpression comp = Expression.LessThan(strLen, five);

            Expression<Func<string, bool>> lamb =
                Expression.Lambda<Func<string, bool>>(comp, p);

            var runnable = lamb.Compile();
            Console.WriteLine(runnable("kangaroo"));
            Console.WriteLine(runnable("dog"));
        }
    }
}
