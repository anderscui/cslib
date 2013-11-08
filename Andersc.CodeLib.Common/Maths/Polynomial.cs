using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Maths
{
    // TODO: Unit Test Cases and refactoring.
    public class Polynomial
    {
        private static readonly int DEFAULT_SIZE = 16;

        private int maxDegree;
        private int[] coeffArray;
        private int highPower;

        #region Properties

        public int MaxDegree
        {
            get { return maxDegree; }
        }

        public int HighPower
        {
            get { return highPower; }
        }

        public int this[int power]
        {
            get
            {
                return coeffArray[power];
            }
            set
            {
                coeffArray[power] = value;
                if (value != 0 && power > highPower)
                {
                    highPower = power;
                }

                if (value == 0 && power == highPower)
                {
                    ResetHighPower();
                }
            }
        }

        public static Polynomial Zero
        {
            get
            {
                return new Polynomial(1);
            }
        }

        #endregion

        #region Constructor

        public Polynomial() : this(DEFAULT_SIZE)
        { }

        public Polynomial(int maxDegree)
        {
            this.maxDegree = maxDegree;
            InitArray();
            highPower = 0;
        }

        #endregion

        private void InitArray()
        {
            coeffArray = new int[maxDegree + 1];
            for (int i = 0; i <= maxDegree; i++)
            {
                coeffArray[i] = 0;
            }
        }

        // TODO: operator override + / - / * / == / != / Equals()
        public Polynomial Add(Polynomial poly)
        {
            int max = Math.Max(maxDegree, poly.MaxDegree);
            Polynomial p = new Polynomial(max);

            for (int i = 0; i <= highPower; i++)
            {
                p.AddValue(i, coeffArray[i]);
            }

            for (int j = 0; j <= poly.HighPower; j++)
            {
                p.AddValue(j, poly.coeffArray[j]);
            }

            return p;
        }

        public Polynomial Subtract(Polynomial poly)
        {
            return Add(poly.Multiply(-1));
        }

        public Polynomial Multiply(Polynomial poly)
        {
            Polynomial p = new Polynomial(maxDegree + poly.MaxDegree);

            for (int i = 0; i <= highPower; i++)
            {
                for (int j = 0; j <= poly.HighPower; j++)
                {
                    p.AddValue(i + j, coeffArray[i] * poly.coeffArray[j]);
                }
            }

            return p;
        }

        public Polynomial Multiply(int multiple)
        {
            Polynomial p = new Polynomial(maxDegree);

            for (int i = 0; i <= highPower; i++)
            {
                p[i] = coeffArray[i] * multiple;
            }

            return p;
        }

        public void AddValue(int power, int coeff)
        {
            this[power] = coeffArray[power] + coeff;
        }

        private void ResetHighPower()
        {
            int power = 0;
            for (int i = maxDegree; i >= 0; i--)
            {
                if (coeffArray[i] != 0)
                {
                    power = i;
                    break;
                }
            }

            highPower = power;
        }

        public int CalculateValue(int value)
        {
            if (HighPower == 0)
            {
                return coeffArray[0];
            }

            return CalcByHornerRule(value);
        }

        private int CalcSimply(int value)
        {
            int sum = 0;
            for (int i = 0; i < coeffArray.Length; i++)
            {
                sum += Convert.ToInt32(coeffArray[i] * Math.Pow(value, i));
            }

            return sum;
        }

        private int CalcByHornerRule(int value)
        {
            int temp = coeffArray[HighPower];
            for (int i = HighPower; i >= 1; i--)
            {
                temp = temp * value + coeffArray[i - 1];
            }

            return temp;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = highPower; i >= 0; i--)
            {
                if (coeffArray[i] != 0)
                {
                    if (i > 0)
                    {
                        sb.Append(string.Format("({0}X^{1}) + ", coeffArray[i], i));
                    }
                    else
                    {
                        sb.Append(string.Format("({0}) + ", coeffArray[i]));
                    }
                }
            }

            string result = sb.ToString();
            if (result.Length == 0)
            {
                result = "0";
            }
            else
            {
                result = result.Substring(0, result.Length - 3);
            }

            return result;
        }
    }
}
