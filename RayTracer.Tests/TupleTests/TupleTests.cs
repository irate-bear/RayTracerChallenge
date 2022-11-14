using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Tests.TupleTests
{
    public class TupleTests
    {
        [Fact]
        public void Reflect_Vector()
        {
            Vector v = new Vector(1, -1, 0);
            Vector normal = new Vector(0, 1, 0);

            var actual = v.Reflect(normal);
            var expected = new Vector(1, 1, 0);

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Reflect_Slanted_Vector()
        {
            var n = Math.Sqrt(2);
            Vector normal = new Vector(n / 2, n / 2, 0);
            Vector v = new Vector(0, -1, 0);

            var actual = v.Reflect(normal);
            var expected = new Vector(1, 0, 0);

            Assert.Equal(expected, actual);
        }
    }
}
