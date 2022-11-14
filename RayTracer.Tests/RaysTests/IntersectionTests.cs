using RayTracer.Matrices;
using RayTracer.Primitives;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Tests.RaysTests
{
    public class IntersectionTests
    {
        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, -1, 1)]
        [InlineData(0, -2, -1)]
        public void Get_Valid_Intersections(int hit, double a, double b)
        {
            Sphere sphere = new Sphere();
            var i1 = new Intersection(sphere, a);
            var i2 = new Intersection(sphere, b);
            var xs = new Intersections(i1, i2);

            var actual = xs.Hit();
            var expected = hit switch
            {
                0 => null,
                var n => n == 1 ? i1 : i2
            };

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Intersection_Get_Computations()
        {
            var sphere = new Sphere();
            var ray = new Ray(new RayTracer.Tuple.Point(0,0,-5), new RayTracer.Tuple.Vector(0,0,1));

            var intersection = new Intersection(sphere, 4);
            var actual = intersection.GetComputation(ray);
            var expected = 
                new Computation(
                        intersection.T, 
                        true, 
                        intersection.Shape, 
                        new RayTracer.Tuple.Point(0,0,-1), 
                        new RayTracer.Tuple.Vector(0,0,-1),
                        new RayTracer.Tuple.Vector(0,0,-1)
                );

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Intersection_Get_Computations_Inside()
        {
            var sphere = new Sphere();
            var ray = new Ray(new RayTracer.Tuple.Point(0,0,0), new RayTracer.Tuple.Vector(0,0,1));

            var intersection = new Intersection(sphere, 1);
            var actual = intersection.GetComputation(ray);
            var expected = 
                new Computation(
                        intersection.T, 
                        true, 
                        intersection.Shape, 
                        new RayTracer.Tuple.Point(0,0,1), 
                        new RayTracer.Tuple.Vector(0,0,-1),
                        new RayTracer.Tuple.Vector(0,0,-1)
                );

            Assert.Equal(expected.IsInside, actual.IsInside);
        }

        [Fact]
        public void Intersection_Get_Compiutations_Reflected()
        {
            var n = Math.Sqrt(2);
            var shape = new Plane();
            var ray = new Ray(new Point(0, 1, -1), new Vector(0, -n / 2, n / 2));
            var intersect = new Intersection(shape, n);
            var comp = intersect.GetComputation(ray);

            var expected = new Vector(0, n / 2, n / 2);
            var actual = comp.Reflect;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 1.0, 1.5)]
        [InlineData(1, 1.5, 2.0)]
        [InlineData(2, 2.0, 2.5)]
        [InlineData(3, 2.5, 2.5)]
        [InlineData(4, 2.5, 1.5)]
        [InlineData(5, 1.5, 1.0)]
        public void Intersection_Get_Computation_Refractive_Indices(int index, double n1, double n2)
        {
            var A = CreateShape.CreateGlassShape(new Sphere());
            A.Transformation = Matrix4D.Scaling(2, 2, 2);
            A.Material.RefractiveIndex = 1.5;
            var B = CreateShape.CreateGlassShape(new Sphere());
            B.Transformation = Matrix4D.Translation(0, 0, -0.25);
            B.Material.RefractiveIndex = 2.0;
            var C = CreateShape.CreateGlassShape(new Sphere());
            C.Transformation = Matrix4D.Translation(0, 0, 0.25);
            C.Material.RefractiveIndex = 2.5;
            var r = new Ray(new Point(0, 0, -4), new Vector(0, 0, 1));
            var intersects = 
                new Intersections(
                    new Intersection[] 
                    { 
                        new Intersection(A, 2),
                        new Intersection(B, 2.75),
                        new Intersection(C, 3.25),
                        new Intersection(B, 4.75),
                        new Intersection(C, 5.25),
                        new Intersection(A, 6),
                    });
            var comp = intersects[index].GetComputation(r, intersects);
            var m1 = comp.N1 == n1;
            var m2 = comp.N2 == n2;

            var res = m1 == m2;

            //Assert.True(res);
            Assert.Equal(n1, comp.N1);
            //Assert.Equal(n2, comp.N2);
        }

        [Fact]
        public void Intersections_UnderPoint()
        {
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var s = CreateShape.CreateGlassShape(new Sphere());
            s.Transformation = Matrix4D.Translation(0, 0, 1);
            var i = new Intersection(s, 5);
            var xs = new Intersections(new Intersection[] { i });
            var comp = i.GetComputation(r, xs);

            var res = comp.UnderPoint.Z > Extensions.EPSILON / 2;
            var res2 = comp.Point.Z < comp.UnderPoint.Z;

            Assert.True(res);
        }
    }
}
