using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Matrices
{
    public class Matrix4D : Matrix
    {
        public static Matrix4D Identity = new Matrix4D(
                1.0, 0.0, 0.0, 0.0,
                0.0, 1.0, 0.0, 0.0,
                0.0, 0.0, 1.0, 0.0,
                0.0, 0.0, 0.0, 1.0
            );
        public Matrix4D() : base(4, 4)
        {
        }

        public Matrix4D(params double[] items) : base(4, 4, items)
        {
        }

        public Matrix4D(double[][] items) : base(items)
        {
        }

        public static Matrix4D operator *(Matrix4D lhs, Matrix4D other)
        {            
            Vector[] rows = lhs.GetRows;
            Vector[] cols = other.GetCols;
            double[] res = new double[lhs.Cols * other.Rows];
            int i = 0;
            foreach (var row in rows)
            {
                foreach (var col in cols)
                {
                    res[i] = row.Dot(col);
                    i++;
                }
            }

            return new Matrix4D(res);
        }

        public static bool operator ==(Matrix4D m1, Matrix4D m2)
        {
            return m1.Equals(m2);
        }

        public static bool operator !=(Matrix4D m1, Matrix4D m2)
        {
            return !m1.Equals(m2);
        }

        public static Vector operator *(Vector v, Matrix4D m)
        {
            Vector[] cols = m.GetCols;
            double[] res = new double[m.Cols];
            int i = 0;
            foreach (var col in cols)
            {
                res[i] = v.Dot(col);
                i++;
            }
            return new Vector(res);
        }

        public static Vector operator *(Matrix4D m, Vector v)
        {
            Vector[] rows = m.GetRows;
            double[] res = new double[m.Rows];
            int i = 0;
            foreach (var row in rows)
            {
                res[i] = row.Dot(v);
                i++;
            }
            return new Vector(res);
        }

        public static Point operator *(Point p, Matrix4D m)
        {
            Vector[] cols = m.GetCols;
            double[] res = new double[m.Cols];
            Vector v = new Vector(p.ToVector());
            int i = 0;
            foreach (var col in cols)
            {
                res[i] = v.Dot(col);
                i++;
            }
            return new Point(res);
        }

        public static Point operator *(Matrix4D m, Point p)
        {
            Vector[] rows = m.GetRows;
            double[] res = new double[m.Rows];
            Vector v = new Vector(p.ToVector());
            int i = 0;
            foreach (var row in rows)
            {
                res[i] = row.Dot(v);
                i++;
            }
            return new Point(res);
        }

        public Matrix4D Transpose()
        {
            return new Matrix4D(base.Transpose());
        }

        public Matrix3D Submatrix(int row, int col)
        {
            double[] res = new double[9];
            int n = 0;
            for (int i = 0; i < Rows; i++)
            {
                if (i != row)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        if (j != col)
                        {
                            res[n] = matrix[i][j];
                            n++;
                        } 
                    }
                }
            }
            return new Matrix3D(res);
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

        public Matrix4D Inverse()
        {
            double det = Determinant();
            if (det == 0)
                throw new InvalidOperationException($"The matrix cannot be inverted.");
            double[][] res = new double[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (res[j] is null)
                        res[j] = new double[Cols];
                    res[j][i] = Cofactor(i, j) / det;
                }
            }

            return new Matrix4D(res);
        }

        public static Matrix4D Translation(double x, double y, double z)
        {
            return new Matrix4D(
                1, 0, 0, x,
                0, 1, 0, y,
                0, 0, 1, z,
                0, 0, 0, 1
            );
        }

        public static Matrix4D Scaling(double x, double y, double z)
        {
            return new Matrix4D(
                x, 0, 0, 0,
                0, y, 0, 0,
                0, 0, z, 0,
                0, 0, 0, 1
            );
        }

        public static Matrix4D RotationX(double radians)
        {
            return new Matrix4D(
                1, 0, 0, 0,
                0, Math.Cos(radians), -Math.Sin(radians), 0,
                0, Math.Sin(radians), Math.Cos(radians), 0,
                0, 0, 0, 1
            );
        }

        public static Matrix4D RotationY(double radians)
        {
            return new Matrix4D(
                Math.Cos(radians), 0, Math.Sin(radians), 0,
                0, 1, 0, 0,
                -Math.Sin(radians), 0, Math.Cos(radians), 0,
                0, 0, 0, 1
            );
        }

        public static Matrix4D RotationZ(double radians)
        {
            return new Matrix4D(
                Math.Cos(radians), -Math.Sin(radians), 0, 0,
                Math.Sin(radians), Math.Cos(radians), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            );
        }

        public static Matrix4D Shearing(double ay, double az, double bx, double bz, double cx, double cy)
        {
            return new Matrix4D(
                1, ay, az, 0,
                bx, 1, bz, 0,
                cx, cy, 1, 0,
                0, 0, 0, 1
            );
        }

        public static Matrix4D ViewTransform(Point from, Point to, Vector up)
        {
            Vector forward = (to - from).Normalize();
            Vector upn = up.Normalize();
            Vector left = forward.Cross(upn);
            Vector true_up = left.Cross(forward);
            Matrix4D orientation = new Matrix4D(new double[]
            {
                left.X, left.Y, left.Z, 0,
                true_up.X, true_up.Y, true_up.Z, 0,
                -forward.X, -forward.Y, -forward.Z, 0,
                0, 0, 0, 1
            });

            return orientation * Translation(-from.X, -from.Y, -from.Z);
        }
    }
}
