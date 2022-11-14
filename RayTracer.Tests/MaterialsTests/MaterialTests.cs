using RayTracer.Display;
using RayTracer.Materials;
using RayTracer.Primitives;
using RayTracer.Scene;
using RayTracer.Tuple;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Tests.MaterialsTests
{
    public class MaterialTests
    {
        [Theory]
        [MemberData(nameof(TestDataGenerator))]
        public void Material_Lighting(Vector eye, Vector normal, Point point, Color color, Color expected)
        {
            ILight light = new PointLight(point, color);
            Material material = new Material();
            Point position = new Point(0, 0, 0);

            var actual = material.Lighting(new Sphere(),light, position, eye, normal, false);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Material_Lighting_Shadow()
        {
            var eye = new Vector(0, 0, -1);
            var normal = new Vector(0,0,-1);
            var light = new PointLight(new Point(0,0,-10), new Color(1, 1, 1));
            var material = new Material();
            var inShadow = true;

            var actual = material.Lighting(new Sphere(), light, new Point(0,0,0), eye, normal, inShadow);
            var expected = new Color(0.1, 0.1, 0.1);

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> TestDataGenerator()
        {
            double n = Math.Sqrt(2);
            double r = 0.1 + 0.9 * (n/2);
            double r1 = 0.1 + 0.9 * (n/2) + 0.9;

            yield return new object[] { new Vector(0,0,-1), new Vector(0,0,-1), new Point(0,0,-10), new Color(1,1,1), new Color(1.9,1.9,1.9) };
            yield return new object[] { new Vector(0,n/2,-n/2), new Vector(0,0,-1), new Point(0,0,-10), new Color(1,1,1), new Color(1.0,1.0,1.0) };
            yield return new object[] { new Vector(0,0,-1), new Vector(0,0,-1), new Point(0,10,-10), new Color(1,1,1), new Color(r,r,r) };
            yield return new object[] { new Vector(0,-n/2,-n/2), new Vector(0,0,-1), new Point(0,10,-10), new Color(1,1,1), new Color(r1,r1,r1) };
            yield return new object[] { new Vector(0,0,-1), new Vector(0,0,-1), new Point(0,0,10), new Color(1,1,1), new Color(0.1,0.1,0.1) };
        }
    }
}
