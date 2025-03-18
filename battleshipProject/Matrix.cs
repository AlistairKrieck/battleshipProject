using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleshipProject
{
    public class Matrix
    {
        int rows, columns;
        int[,] matrix;

        public Matrix(int _rows, int _columns)
        {
            rows = _rows;
            columns = _columns;
            matrix = new int[rows, columns];
        }

        public int GetValue(int m, int n)
        {
            int outP = matrix[m, n];
            return outP;
        }

        public void SetValue(int m, int n, int val)
        {
            matrix[m, n] = val;
        }

        public void SetRow(int row, int[] data)
        {
            for (int i = 0; i < data.Count(); i++)
            {
                matrix.SetValue(row, i, data[i]);
            }
        }

        public void SetColumn(int col, int[] data)
        {
            for (int i = 0; i < data.Count(); i++)
            {
                matrix.SetValue(i, col, data[i]);
            }
        }

        public void Fill(List<int[]> data)
        {
            for(int i = 0; i < rows; i++)
            {
                SetRow(i, data[i]);
            }
        }

    }
}
