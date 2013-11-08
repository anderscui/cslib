using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Maths
{
    // TODO: Unit Test Cases
    public abstract class DenseMatrix : Matrix
    {
        protected int numberOfRows;
        protected int numberOfColumns;
        protected double[,] array;

        public DenseMatrix(int numberOfRows, int numberOfColumns)
        {
            this.numberOfRows = numberOfRows;
            this.numberOfColumns = numberOfColumns;
            array = new double[numberOfRows, numberOfColumns];
        }

        public override int Rows
        {
            get { return numberOfRows; }
        }

        public override int Columns
        {
            get { return numberOfColumns; }
        }        
    }
}
