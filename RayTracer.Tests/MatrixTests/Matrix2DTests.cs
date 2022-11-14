using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracer.Matrices;

namespace RayTracer.Tests.MatrixTests
{
    public class Matrix2DTests
    {
        [Fact]
        public void Matrix2D_Determinant()
        {
            Matrix2D m = new Matrix2D(
                1, 5, -3, 2  
            );

            var actual = m.Determinant();
            var expected = 17;

            Assert.Equal(expected, actual);
        }
    }
}
