using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Algorithm
{
    public class Queens
    {
        private int[] s = null;
        private int count = 0;
        private int suc;

        public Queens(int count)
        {
            this.count = count;
            s = new int[count];
            suc = 0;

            for (int i = 0; i < count; i++)
            {
                s[i] = -1;
            }
        }

        public void Solve(int n)
        {
            if (n == count)
            {
                suc++;
                Print();
                return;
            }

            for (int i = 0; i < count; i++)
            {
                s[n] = i;
                if (IsAvailable(n))
                {
                    Solve(n + 1);
                }
            }
        }

        private void Print()
        {
            Console.WriteLine("Solution {0}: ", suc);
            foreach (int i in s)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        private bool IsAvailable(int n)
        {
            for (int i = 0; i < n; i++)
            {
                if (s[i] == s[n])
                {
                    return false;
                }
                if (Math.Abs(s[n] - s[i]) == n - i)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
