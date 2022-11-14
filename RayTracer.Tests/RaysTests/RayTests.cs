using RayTracer.Matrices;
using RayTracer.Primitives;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Tests.RaysTests
{
    public class RayTests
    {
        [Theory]
        [InlineData(0,2)]
        [InlineData(1,3)]
        [InlineData(-1,1)]
        [InlineData(2.5,4.5)]
        public void Ray_Position(double t, double x)
        {
            Point p = new Point(2, 3, 4);
            Vector v = new Vector(1, 0, 0);
            Ray ray = new Ray(p, v);

            var actual = ray.Position(t);
            var expected = new Point(x, 3, 4);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Ray_Transform_Translation()
        {
            Ray ray = new Ray(new Point(1, 2, 3), new Vector(0, 1, 0));
            Matrix4D matrix = Matrix4D.Translation(3, 4, 5);

            var actual = ray.Transform(matrix);
            var expected = new Ray(new Point(4, 6, 8), new Vector(0, 1, 0));
            var res = actual.Origin.Equals(expected.Origin) &&
               actual.Direction.Equals(expected.Direction);

            Assert.True(res);
        }
        
        [Fact]
        public void Ray_Transform_Scale()
        {
            Ray ray = new Ray(new Point(1, 2, 3), new Vector(0, 1, 0));
            Matrix4D matrix = Matrix4D.Scaling(2, 3, 4);

            var actual = ray.Transform(matrix);
            var expected = new Ray(new Point(2, 6, 12), new Vector(0, 3, 0));
            var res = actual.Origin.Equals(expected.Origin) &&
               actual.Direction.Equals(expected.Direction);

            Assert.True(res);
        }

    }
}
