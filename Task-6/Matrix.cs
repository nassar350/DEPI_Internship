using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Task_6
{
    internal class Matrix
    {
        public int row { get; }
        public int column { get; }
        int[,] matrix;
        public Matrix(int x, int y)
        {
            row = x;
            column = y;
            matrix = new int[x, y];
        }

        public int this[int x,int y]
        {
            get 
            {
                return matrix[x,y];
            }
            set
            {
                matrix[x,y] = value;
            }
        }
    }
}
