using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Maths
{
    // TODO: Unit Test Cases
    public struct Complex
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public Complex(double real, double imaginary)
            : this()
        {
            this.Real = real;
            this.Imaginary = imaginary;
        }

        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }

        public static Complex operator -(Complex c1, Complex c2)
        {
            return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        }

        public static Complex operator *(Complex c1, Complex c2)
        {
            return new Complex()
            {
                Real = c1.Real * c2.Real - c1.Imaginary * c2.Imaginary,
                Imaginary = c1.Real * c2.Imaginary - c1.Imaginary * c2.Real
            };
        }

        public static Complex operator /(Complex c1, Complex c2)
        {
            return new Complex
            {
                Real = -c1.Real * c2.Real + c1.Imaginary * c2.Imaginary,
                Imaginary = -c1.Real * c2.Imaginary + c1.Imaginary * c2.Real
            };
        }

        public static implicit operator Complex(string value)
        {
            value = value.TrimEnd('i');
            string[] digits = value.Split('+', '-');

            return new Complex()
            {
                Real = Convert.ToDouble(digits[0]),
                Imaginary = Convert.ToDouble(digits[1])
            };
        }

        // TODO: More init methods?
        //public static implicit operator string(Complex value)
        //{
        //    return string.Format("{0} + {1}i", value.Real, value.Imaginary);
        //}

        public static explicit operator Complex(double real)
        {
            return new Complex(real, 0);
        }
    }
}
