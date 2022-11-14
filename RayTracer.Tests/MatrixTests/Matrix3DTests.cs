using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracer.Matrices;

namespace RayTracer.Tests.MatrixTests
{
    public class Matrix3DTests
    {
        [Fact]
        public void Matrix3D_Submatrix()
        {
            var m = new Matrix3D(
                1 , 5 , 0 ,
                -3 , 2 , 7 ,
                0 , 6 , -3    
            );

            var expected = new Matrix2D(
                -3, 2,
                0,  6
            );
            var actual = m.Submatrix(0,2);
        }

        [Fact]
        public void Matrix3D_Minor()
        {
            Matrix3D m = new Matrix3D(
                3 , 5 , 0 ,
                2 , -1 , -7 ,
                6 , -1 , 5
            );

            var expected = 25;
            var actual = m.Minor(1, 0);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 0, -12)]
        [InlineData(1, 0, 25)]
        public void Matrix3D_Cofactor(int row, int col, double res)
        {
            Matrix3D m = new Matrix3D(
                3 , 5 , 0 ,
                2 , -1 , -7 ,
                6 , -1 , 5
            );

            var expected = res;
            var actual = m.Minor(row, col);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Matrix3D_Determinant()
        {
            var m = new Matrix3D(
                1 , 2 , 6 ,
                -5 , 8 , -4 ,
                2 , 6 , 4
            );

            var expected = -196;
            var actual = m.Determinant();

            Assert.Equal(expected , actual);
        }
    }
}
