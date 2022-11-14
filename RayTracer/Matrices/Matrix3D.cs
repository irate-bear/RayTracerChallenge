using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Matrices
{
    public class Matrix3D : Matrix
    {
        public Matrix3D(double[][] values) : base(values)
        {
        }

        public Matrix3D(int rows, int cols) : base(rows, cols)
        {
        }

        public Matrix3D(params double[] values) : base(3, 3, values)
        {
        }

        public Matrix2D Submatrix(int row, int col)
        {
            double[] values = new double[Rows * Cols];
            int n = 0;
            for (int i = 0; i < Rows; i++)
            {
                if (i != row)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        if (j != col)
                        {
                            values[n] = matrix[i][j];
                            n++;
                        }
                    }
                }
            }

            return new Matrix2D(values);
        }

        public double Minor(int row, int col)
        {
            return Submatrix(row, col).Determinant();
        }

        public double Cofactor(int row, int col)
        {
            return Math.Pow(-1, row + col) * Minor(row, col);
        }

        public double Determinant()
        {
            double det = 0;
            for (int i = 0; i < Cols; i++)
            {
                det += matrix[0][i] * Cofactor(0, i);
            }
            return det;
        }
    }
}
