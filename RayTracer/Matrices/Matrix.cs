using RayTracer.Tuple;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Matrices
{
    public abstract class Matrix : IEnumerable<double>
    {
        internal double[][] matrix;

        public int Rows { get; }
        public int Cols { get; }

        public Vector[] GetRows { get; }
        public Vector[] GetCols { get; }

        public Matrix(int rows, int cols) : this(rows, cols, 0.0)
        {
        }

        public Matrix(int rows, int cols, params double[] values)
        {
            matrix = new double[rows][];
            Rows = rows;
            Cols = cols;
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new double[cols];
                for (int j = 0; j < cols; j++)
                {
                    int index = i * rows + j;
                    if (index < values.Length)
                        this[i, j] = values[index];
                    else
                        break;
                }
            }
            GetCols = ToVectorArray(Transpose());
            GetRows = ToVectorArray();
        }

        public Matrix(double[][] values)
        {
            matrix = values;
            Rows = values.Length;
            Cols = values[0].Length;
            GetCols = ToVectorArray(Transpose());
            GetRows = ToVectorArray();
        }
        
        public IEnumerator<double> GetEnumerator()
        {
            foreach (var val in matrix)
            {
                foreach(var num in val)
                {
                    yield return num;
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public double this[int row, int col]
        {
            get => matrix[row][col];
            set => matrix[row][col] = value;
        }

        public override bool Equals(object? obj)
        {
            bool isEqual = false;

            if (obj is Matrix mat &&
                mat.Rows == Rows &&
                mat.Cols == Cols)
            {
                isEqual = this.Zip(mat, (x, y) => x.AbsDiff(y)).All(x => x);
            }

            return isEqual;
        }

        public bool Equals(Matrix mat, double epsilon)
        {
            bool isEqual = false;

            if (mat.Rows == Rows &&
                mat.Cols == Cols)
            {
                isEqual = this.Zip(mat, (x, y) => x.AbsDiff(y, epsilon)).All(x => x);
            }

            return isEqual;
        }

        internal double[][] Transpose()
        {
            double[][] mat = new double[Rows][];

            for (int i = 0; i < Rows; i++)
            {
                mat[i] =  new double[Cols]; 
                for (int j = 0; j < Cols; j++)
                {
                    mat[i][j] = matrix[j][i];
                }
            }
            
            return mat;
        }

        internal Vector[] ToVectorArray()
        {
            Vector[] vectors = new Vector[Rows];
            for (int i = 0; i < Rows; i++)
            {
                var row = matrix[i];
                if (row is not null)
                    vectors[i] = new Vector(row);
            }
            return vectors;
        }

        private Vector[] ToVectorArray(double[][] array)
        {
            Vector[] vectors = new Vector[Rows];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                var row = array[i];
                if (row is not null)
                    vectors[i] = new Vector(row);
            }
            return vectors;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
   
    }
}
