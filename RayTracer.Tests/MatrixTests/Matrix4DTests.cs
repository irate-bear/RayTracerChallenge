using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracer.Matrices;
using RayTracer.Tuple;

namespace RayTracer.Tests.MatrixTests
{
    public class Matrix4DTests
    {
        [Fact]
        public void Create_4d_Matrix()
        {
            Matrix mat = new Matrix4D(
                1, 2, 3, 4,
                5.5, 6.5, 7.5, 8.5,
                9, 10, 11, 12,
                13.5, 14.5, 15.5, 16.5 
            );

            var actual = mat[0, 0].AbsDiff(1) &&
                mat[0, 1].AbsDiff(2) &&
                mat[0, 2].AbsDiff(3) &&
                mat[0, 3].AbsDiff(4) &&
                mat[1, 0].AbsDiff(5.5) &&
                mat[1, 1].AbsDiff(6.5) &&
                mat[1, 2].AbsDiff(7.5) &&
                mat[1, 3].AbsDiff(8.5) &&
                mat[2, 0].AbsDiff(9) &&
                mat[2, 1].AbsDiff(10) &&
                mat[2, 2].AbsDiff(11) &&
                mat[2, 3].AbsDiff(12) &&
                mat[3, 0].AbsDiff(13.5) &&
                mat[3, 1].AbsDiff(14.5) &&
                mat[3, 2].AbsDiff(15.5) &&
                mat[3, 3].AbsDiff(16.5);

            Assert.True(actual);
        }

        [Fact]
        public void Matrx4D_Are_Equal()
        {
            Matrix4D m1 = new Matrix4D(
                1.0 , 2 , 3 , 4 ,
                5 , 6 , 7 , 8 ,
                9 , 8 , 7 , 6 ,
                5 , 4 , 3 , 2);

            Matrix4D m2 = new Matrix4D(
                1 , 2 , 3 , 4 ,
                5 , 6 , 7 , 8 ,
                9 , 8 , 7.0 , 6 ,
                5 , 4 , 3 , 2);

            bool actual = m1.Equals(m2);

            Assert.True(actual);
        }

        [Fact]
        public void Matrx4D_Are_Not_Equal()
        {
            Matrix4D m1 = new Matrix4D(
                1 , 2 , 3 , 4 ,
                5 , 6 , 7 , 8 ,
                9 , 8 , 7 , 6 ,
                5 , 4 , 3 , 2.1);

            Matrix4D m2 = new Matrix4D(
                1 , 2 , 3 , 4 ,
                5 , 6 , 7 , 8 ,
                9 , 8 , 7 , 6 ,
                5 , 4 , 3 , 2);

            bool actual = m1.Equals(m2);

            Assert.False(actual);
        }

        [Fact]
        public void Multiply_Two_Matrix4D()
        {
            Matrix4D m1 = new Matrix4D(
                1 , 2 , 3 , 4 ,
                5 , 6 , 7 , 8 ,
                9 , 8 , 7 , 6 ,
                5 , 4 , 3 , 2
            );

            Matrix4D m2 = new Matrix4D(
                -2, 1, 2, 3,
                3, 2, 1, -1,
                4, 3, 6, 5,
                1, 2, 7, 8
            );

            Matrix4D expected = new Matrix4D(
                20, 22 , 50 , 48 ,
                44, 54 , 114 , 108 ,
                40, 58 , 110 , 102 ,
                16, 26 , 46 , 42   
            );

            Matrix4D actual = m1 * m2;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Multiply_Matrix4D_With_Vector()
        {
            Matrix4D m = new Matrix4D(
                1 , 2 , 3 , 4 ,
                2 , 4 , 4 , 2 ,
                8 , 6 , 4 , 1 ,
                0 , 0 , 0 , 1 
            );

            Vector v = new Vector(1, 2, 3, 1);

            var actual = m * v;
            var expected = new Vector(18, 24, 33, 1);
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix4D_Left_Identity()
        {
            Matrix4D m = new Matrix4D(
                0 , 1 , 2 , 4 ,
                1 , 2 , 4 , 8 ,
                2 , 4 , 8 , 16 ,
                4 , 8 , 16 , 32    
            );

            Matrix4D actual = Matrix4D.Identity * m;
            Matrix4D expected = m;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix4D_Right_Identity()
        {
            Matrix4D m = new Matrix4D(
                0 , 1 , 2 , 4 ,
                1 , 2 , 4 , 8 ,
                2 , 4 , 8 , 16 ,
                4 , 8 , 16 , 32    
            );

            Matrix4D actual = m * Matrix4D.Identity;
            Matrix4D expected = m;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix4D_Transpose()
        {
            Matrix4D m = new Matrix4D(
                0 , 9 , 3 , 0 ,
                9 , 8 , 0 , 8 ,
                1 , 8 , 5 , 3 ,
                0 , 0 , 5 , 8    
            );

            var actual = m.Transpose();
            var expected = new Matrix4D(
                0, 9, 1, 0,
                9, 8, 8, 0,
                3, 0, 5, 5,
                0, 8, 3, 8
            );

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix4D_Identity_Transpose()
        {
             Matrix4D m = Matrix4D.Identity;

            var actual = m.Transpose();
            var expected = m;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix4D_Submatrix()
        {
            var m = new Matrix4D(
                -6, 1, 1, 6,
                -8, 5, 8, 6,
                -1, 0, 8, 2,
                -7, 1, -1, 1
            );

            var expected = new Matrix3D(
                -6, 1, 6,
                -8, 8, 6,
                -7, -1, 1
            );
            var actual = m.Submatrix(2, 1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix4D_Determinant()
        {
            var m = new Matrix4D(
                -2 , -8 , 3 , 5 ,
                -3 , 1 , 7 , 3 ,
                1 , 2 , -9 , 6 ,
                -6 , 7 , 7 , -9
            );

            var actual = m.Determinant();
            var expected = -4071;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix4D_Inverse()
        {
            Matrix4D m = new Matrix4D(
                -5 , 2 , 6 , -8 ,
                1 , -5 , 1 , 8 ,
                7 , 7 , -6 , -7 ,
                1 , -3 , 7 , 4    
            );

            var actual = m.Inverse();
            var expected = new Matrix4D(
                0.21805 , 0.45113 , 0.24060 , -0.04511 ,
                -0.80827 , -1.45677 , -0.44361 , 0.52068 ,
                -0.07895 , -0.22368 , -0.05263 , 0.19737 ,
                -0.52256 , -0.81391 , -0.30075 , 0.30639
            );
            var res = actual.Equals(expected, 1e-5);

            Assert.True(res);
        }

        [Fact]
        public void Matrix4D_Product_Multiplied_By_Inverse()
        {
            var m1 = new Matrix4D(
                3 , -9 , 7 , 3 ,
                3 , -8 , 2 , -9 ,
                -4 , 4 , 4 , 1 ,
                -6 , 5 , -1 , 1
            );
            var m2 = new Matrix4D(
                8 , 2 , 2 , 2 ,
                3 , -1 , 7 , 0 ,
                7 , 0 , 5 , 4 ,
                6 , -2 , 0 , 5
            );

            var expected = m1;
            var actual = (m1 * m2) * m2.Inverse();

            Assert.True(expected == actual);
        }

        [Fact]
        public void Matrix4D_Translation()
        {
            Matrix4D m = Matrix4D.Translation(5, -3, 2);
            Point p = new Point(-3, 4, 5);

            var actual = m * p;
            var expected = new Point(2, 1, 7);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix4D_Inverse_Translation()
        {
            Matrix4D m = Matrix4D.Translation(5, -3, 2).Inverse();
            Point p = new Point(-3, 4, 5);

            var actual = m * p;
            var expected = new Point(-8, 7, 3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix4D_Translation_Vector()
        {
            Matrix4D m = Matrix4D.Translation(5, -3, 2);
            Vector v = new Vector(-3, 4, 5);

            var actual = m * v;
            var expected = v;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix4D_Scaling()
        {
            Matrix4D m = Matrix4D.Scaling(2, 3, 4);
            Vector v = new Vector(-4, 6, 8);

            var actual = m.Inverse() * v;
            var expected = new Vector(-2, 2, 2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix4D_RotationX()
        {
            Matrix4D m = Matrix4D.RotationX(Math.PI / 4);
            Point p = new Point(0, 1, 0);

            var actual = m.Inverse() * p;
            var expected = new Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Matrix4D_RotationY()
        {
            Matrix4D m = Matrix4D.RotationY(Math.PI / 2);
            Point p = new Point(0, 0, 1);

            var actual = m * p;
            var expected = new Point(1, 0, 0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix4D_RotationZ()
        {
            Matrix4D m = Matrix4D.RotationZ(Math.PI / 2);
            Point p = new Point(0, 1, 0);

            var actual = m * p;
            var expected = new Point(-1, 0, 0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix4D_Shearing()
        {
            Matrix4D m = Matrix4D.Shearing(1, 0, 0, 0, 0, 0);
            Point p = new Point(2, 3, 4);

            var actual = m * p;
            var expected = new Point(5, 3, 4);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix4D_Composing_Transformations()
        {
            Matrix4D rx = Matrix4D.RotationX(Math.PI / 2);
            Matrix4D s = Matrix4D.Scaling(5, 5, 5);
            Matrix4D t = Matrix4D.Translation(10, 5, 7);
            Point p = new Point(1, 0, 1);

            var p1 = rx * p;
            var p2 = s * p1;
            var expected = t * p2;
            var actual = t * s * rx * p;

            Assert.Equal(expected, actual);
        }
    }
}
