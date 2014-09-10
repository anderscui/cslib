using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Stats
{
    public class StatsHelper
    {
        public const bool T = true;
        public const bool F = false;
        public const bool TRUE = true;
        public const bool FALSE = false;

        public static int Len<T>(IEnumerable<T> seq)
        {
            return 0;
        }

        public static int Mean<T>(IEnumerable<T> seq)
        {
            return 0;
        }

        public static int Quantile<T>(IEnumerable<T> seq, IEnumerable<double> probs)
        {
            if (probs.IsNotEmpty())
            {
                // seq(0, 1, 0.25)
                probs = new double[] { 0.00, 0.25, 0.50, 0.75, 1.00 };
            }
            return 0;
        }

        public static int Table<T>(IEnumerable<T> seq, object formula)
        {
            // Group (key, count), may be multiple variables
            return 0;
        }

        public static int XTabs<T>(IEnumerable<T> seq, object formula)
        {
            // Group (key, count), may be multiple variables
            return 0;
        }

        // is.na
        // any
        // all 

        // colSums/rowSums/colMeans/rowMeans

    }
}
