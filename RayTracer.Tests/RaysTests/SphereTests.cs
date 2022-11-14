using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracer.Matrices;
using RayTracer.Primitives;
using RayTracer.Tuple;

namespace RayTracer.Tests.RaysTests
{
    public class SphereTests
    {
        [Theory]
        [InlineData(0, 0, -5, 2, 4.0, 6.0)]
        [InlineData(0, 1, -5, 2, 5.0, 5.0)]
        [InlineData(0, 2, -5, 0, 0.0, 0.0)]
        [InlineData(0, 0, 0, 2, -1.0, 1.0)]
        [InlineData(0, 0, 5, 2, -6.0, -4.0)]
        public void RaySphereIntesections(double x, double y, double z, int count, double t1, double t2)
        {
            Ray ray = new Ray(new RayTracer.Tuple.Point(x, y, z), new RayTracer.Tuple.Vector(0, 0, 1));
            Sphere sphere = new Sphere();

            var intersections = sphere.Intersects(ray);
            var actual = intersections.Count() == count &&
                         intersections.Count() switch
                         {
                             0     => true,
                             var n => intersections[0] == t1 && intersections[1] == t2
                         };

            Assert.True(actual);
        }

        [Fact]
        public void Transformed_Sphere_Intersections()
        {
            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            Sphere sphere = new Sphere();
            sphere.Transformation = Matrix4D.Translation(5, 0, 0);

            var actual = sphere.Intersects(ray).Count();
            var expected = 0;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1,0,0,1,0,0)]
        [InlineData(0,1,0,0,1,0)]
        [InlineData(0,0,1,0,0,1)]
        public void Sphere_Normal_At_Point(double x, double y, double z, double u, double v, double w)
        {
            IShape s = new Sphere();
            
            var actual = s.NormalAt(new Point(x, y, z));
            var expected = new Vector(u, v, w);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1,0,0)]
        [InlineData(0,1,0)]
        [InlineData(0,0,1)]
        public void Sphere_Normal_At_Point_Is_Normalized(double x, double y, double z)
        {
            IShape s = new Sphere();
            
            var actual = s.NormalAt(new Point(x, y, z));
            var expected = actual.Normalize();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Normal_At_Translated_Sphere()
        {
            IShape s = new Sphere();
            s.Transformation = Matrix4D.Translation(0, 1, 0);
            var n = Math.Sqrt(2);
            var actual = s.NormalAt(new Point(0, 1 + 1 / n, -1 / n));
            var expected = new Vector(0, 1 / n, -1 / n);

            Assert.Equal(expected, actual);
        }

        /*[Fact]
        public void Normal_At_Transformed_Sphere()
        {
            IShape s = new Sphere();
            s.Transformation = Matrix4D.Scaling(1, 0.5, 1) * Matrix4D.RotationZ(Math.PI / 5);
            var n = Math.Sqrt(2);
            var actual = s.NormalAt(new Point(0, n / 2, -n / 2));
            var expected = new Vector(0, 0.97014, -0.24254);
            var e = 1e-5;
            var res = actual.X.AbsDiff(expected.X, e) &&
                actual.Y.AbsDiff(expected.Y, e) &&
                actual.Z.AbsDiff(expected.Z, e);
            
            Assert.Equal(actual, expected);
        }*/
    }
}
