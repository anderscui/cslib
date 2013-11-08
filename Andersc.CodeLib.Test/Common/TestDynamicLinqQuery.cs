using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using NUnit.Framework;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestDynamicLinqQuery
    {
        [Test]
        public void TestDynamicQuery()
        {
            string[] companies = { "Consolidated Messenger", "Alpine Ski House", "Southridge Video", "City Power & Light",
                               "Coho Winery", "Wide World Importers", "Graphic Design Institute", "Adventure Works",
                               "Humongous Insurance", "Woodgrove Bank", "Margie's Travel", "Northwind Traders",
                               "Blue Yonder Airlines", "Trey Research", "The Phone Company",
                               "Wingtip Toys", "Lucerne Publishing", "Fourth Coffee" };

            IQueryable<string> querableData = companies.AsQueryable<string>();

            ParameterExpression pe = Expression.Parameter(typeof(string), "company");
            // Presents: company.ToLower() == "coho winery";
            Expression left = Expression.Call(pe, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
            Expression right = Expression.Constant("coho winery");
            Expression e1 = Expression.Equal(left, right);

            // Presents: company.Length > 16;
            left = Expression.Property(pe, typeof(string).GetProperty("Length"));
            right = Expression.Constant(16, typeof(int));
            Expression e2 = Expression.GreaterThan(left, right);

            // e1 || e2
            Expression predicteBody = Expression.OrElse(e1, e2);

            MethodCallExpression whereCall = Expression.Call(
                typeof(Queryable), 
                "Where", 
                new Type[] { querableData.ElementType },
                querableData.Expression, 
                Expression.Lambda<Func<string, bool>>(predicteBody, new ParameterExpression[] { pe }));

            MethodCallExpression orderByCall = Expression.Call(
                typeof(Queryable),
                "OrderBy",
                new Type[] { querableData.ElementType, querableData.ElementType },
                whereCall,
                Expression.Lambda<Func<string, string>>(pe, new ParameterExpression[] { pe }));

            IQueryable<string> results = querableData.Provider.CreateQuery<string>(orderByCall);
            foreach (string company in results)
            {
                Console.WriteLine(company);
            }
        }
    }
}
