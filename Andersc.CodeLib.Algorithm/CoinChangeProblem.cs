using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.Algorithm
{
    public static class CoinChangeProblem
    {
        // Returns minimum number of coins to make change.
        // Simple recursive algorithm that is very inefficient.
        // Just for a change of 63, it will take a long time!
        // Its inefficiency is like the Fibonacci sequence.
        public static int MakeChange(int[] coins, int change)
        {
            int miniCoins = change;

            if (coins.Any(i => i == change)) { return 1; }

            for (int i = 1; i <= change / 2; i++)
            {
                int thisCoins = MakeChange(coins, i)
                    + MakeChange(coins, change - i);

                if (thisCoins < miniCoins)
                {
                    miniCoins = thisCoins;
                }
            }

            return miniCoins;
        }

        // A little better, but still inefficient, for 63, it takes more than 10 seconds.
        public static int MakeChange2(int[] coins, int change)
        {
            int miniCoins = change;

            if (coins.Any(i => i == change)) { return 1; }

            for (int i = 0; i < coins.Length; i++)
            {
                if (change < coins[i])
                {
                    continue;
                }

                int thisCoins = 1 + MakeChange2(coins, change - coins[i]);
                if (thisCoins < miniCoins)
                {
                    miniCoins = thisCoins;
                }
            }

            return miniCoins;
        }

        // Uses dynamic programming(动态规划).
        // Avoid many duplicate computing.
        public static int MakeChange3(int[] coins, int change)
        {
            int differentCoins = coins.Length;
            int[] coinsUsed = new int[change + 1];
            int[] lastCoin = new int[change + 1];

            coinsUsed[0] = 0;
            lastCoin[0] = 1;

            for (int cents = 1; cents <= change; cents++)
            {
                int minCoins = cents;
                int newCoin = 1;

                for (int i = 0; i < differentCoins; i++)
                {
                    if(CoinIsNotUsable(cents, coins[i])) { continue; }

                    if (coinsUsed[cents - coins[i]] + 1 < minCoins)
                    {
                        minCoins = coinsUsed[cents - coins[i]] + 1;
                        newCoin = coins[i];
                    }
                }

                coinsUsed[cents] = minCoins;
                lastCoin[cents] = newCoin;
            }

            return coinsUsed[change];
        }

        private static bool CoinIsNotUsable(int cents, int coinValue)
        {
            return (coinValue > cents);
        }
    }
}
