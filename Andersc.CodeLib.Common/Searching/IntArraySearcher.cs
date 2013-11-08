using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Searching
{
    public class IntArraySearcher
    {
        private static readonly int NotFound = -1;

        /// <summary>
        /// Binaries the find by loop.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="key">The key.</param>
        /// <returns>If the key is found, return its index; otherwise, -1.</returns>
        public int BinaryFindByLoop(int[] array, int key)
        {
            if (array.IsNull()) { throw new ArgumentNullException("array"); }
            if (array.IsEmpty()) { return NotFound; }

            int lowerBound = 0;
            int upperBound = array.Length - 1;
            int currentIndex;

            while (true)
            {
                currentIndex = (lowerBound + upperBound) / 2;

                if (array[currentIndex] == key)
                {
                    // Find it.
                    return currentIndex;
                }
                else if (lowerBound > upperBound)
                {
                    // Not found.
                    return NotFound;
                }
                else if (array[currentIndex] < key)
                {
                    // Find to right.
                    lowerBound = currentIndex + 1;
                }
                else
                {
                    // Find to left.
                    upperBound = currentIndex - 1;
                }
            }
        }
    }
}
