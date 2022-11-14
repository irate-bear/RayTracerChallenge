using RayTracer.Display;
using RayTracer.Materials;
using RayTracer.Matrices;
using RayTracer.Primitives;
using RayTracer.Scene;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Tests.SceneTests
{
    public class WorldTests
    {
        [Fact]
        public void World_Ray_Intersections()
        {
            World w = World.DefaultWorld();
            Ray ray = new Ray(new Point(0,0,-5), new Vector(0,0,1));

            var actual = w.Intersect(ray)
                .AsParallel()
                .Select(i => i.T)
                .OrderBy(i => i)
                .ToArray();
            var expected = new double[] { 4, 4.5, 5.5, 6};
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void World_Shade_Hit(Point p1, Point p2, int index, double t, Color c)
        {
            var w = World.DefaultWorld();
            w.Light = new PointLight(p1, new Color(1,1,1));
            var r = new Ray(p2, new Vector(0, 0, 1));
            var shape = w.Shapes[index];
            var i = new Intersection(shape, t);
            var comps = i.GetComputation(r);

            var expected = c;
            var actual = w.ShadeHit(comps, 1);
            var e = 1e-4;
            var res = expected.Red.AbsDiff(actual.Red, e) &&
                expected.Green.AbsDiff(actual.Green, e) &&
                expected.Blue.AbsDiff(actual.Blue, e);

            Assert.True(res);
        }

        [Theory]
        [InlineData(0, 0, 1, 0.38066, 0.47583, 0.2855)]
        [InlineData(0, 1, 0, 0, 0, 0)]
        public void World_ColorAt(double x, double y, double z, double i, double j, double k)
        {
            var w = World.DefaultWorld();
            Ray r = new Ray(new Point(0,0,-5), new Vector(x, y, z));
            
            var expected = new Color(i, j, k);
            var actual = w.ColorAt(r, 1);

            var res = actual.Equal(expected);

            Assert.True(res);
        }

        /*[Fact]
        public void World_Intersection_Behind_Ray()
        {
            var w = World.DefaultWorld();
            var outer = w.Shapes[0];
            outer.Material.Ambient = 1;
            var inner = w.Shapes[1];
            inner.Material.Ambient = 1;
            var ray = new Ray(new Point(0,0,0.75), new Vector(0,0,-1));
            
            var expected = inner.Material.Color;
            var actual = w.ColorAt(ray);
            //var res = expected.Equal(actual);
            Assert.Equal(expected, actual);
        }*/

        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[] 
            { 
                new Point(-10,10,-10), 
                new Point(0,0,-5),
                0,
                4.0, 
                new Color(0.38066, 0.47583, 0.2855) 
            };
            yield return new object[] 
            { 
                new Point(0,0.25,0), 
                new Point(0,0,0),
                1,
                0.5, 
                new Color(0.90498, 0.90498, 0.90498) 
            };
        }

        [Theory]
        [InlineData(0, 10, 0, false)]
        [InlineData(10, -10, 10, true)]
        [InlineData(-20, 20, -20, false)]
        [InlineData(-2, 2, -2, false)]
        public void World_IsShadow_Collinear_Point_Light(int x, int y, int z, bool isShadowed)
        {
            var w = World.DefaultWorld();
            var p = new Point(x, y, z);

            var actual = w.IsShadowed(p);
            var expected = isShadowed;

            Assert.Equal(expected, actual); 
        }

        [Fact]
        public void World_Shade_Hit_In_Shadow()
        {
            var w = World.DefaultWorld();
            w.Light = new PointLight(new Point(0,0,-10), new Color(1,1,1));
            var s1 = new Sphere();
            var s2 = new Sphere();
            s2.Transformation = Matrix4D.Translation(0,0,10);
            w.Shapes = new IShape[] { s1, s2 };
            var r = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
            var i = new Intersection(s2, 4);
            var comps = i.GetComputation(r);

            var expected = new Color(0.1, 0.1, 0.1);
            var actual = w.ShadeHit(comps, 1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void World_Reflected_Color_Non_Reflective_Surface()
        {
            var w = World.DefaultWorld();
            var r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            w.Shapes[1].Material.Ambient = 1;
            var i = new Intersection(w.Shapes[1], 1);
            var c = i.GetComputation(r);

            var expected = new Color(0, 0, 0);
            var actual = w.ReflectedColor(c, 1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void World_Reflected_Color_Reflective_Surface()
        {
            var n = Math.Sqrt(2);
            var w = World.DefaultWorld();
            var m = new Material();
            m.Reflective = 0.5;
            var s = new Plane(m, Matrix4D.Translation(0, -1, 0));
            w.Shapes.Append(s);
            var r = new Ray(new Point(0, 0, -3), new Vector(0, -n/2, n/2));
            var i = new Intersection(s, n);
            var c = i.GetComputation(r);

            var expected = new Color(0.19032, 0.2379, 0.14274);
            var actual = w.ReflectedColor(c, 1);
            var res = expected.Equal(actual);

            Assert.True(res);
        }

        [Fact]
        public void Shade_Hit_Reflective_Material()
        {
            var n = Math.Sqrt(2);
            var w = World.DefaultWorld();
            var m = new Material();
            m.Reflective = 0.5;
            var s = new Plane(m, Matrix4D.Translation(0, -1, 0));
            w.Shapes.Append(s);
            var r = new Ray(new Point(0, 0, -3), new Vector(0, -n/2, n/2));
            var i = new Intersection(s, n);
            var c = i.GetComputation(r);

            var expected = new Color(0.87677, 0.92436, 0.82918);
            var actual = w.ShadeHit(c, 1); 
            var res = expected.Equal(actual);

            Assert.True(res);
        }

        [Fact]
        public void World_Color_At_Recursion_Terminates()
        {
            var w = new World();
            w.Light = new PointLight(new Point(0, 0, 0), new Color(1, 1, 1));
            var m1 = new Material();
            m1.Reflective = 1;
            var m2 = new Material();
            m2.Reflective = 1;
            var lower = new Plane(m1, Matrix4D.Translation(0, -1, 0));
            var upper = new Plane(m2, Matrix4D.Translation(0, 1, 0));
            w.Shapes = new IShape[] { lower, upper };
            var r = new Ray(new Point(0,0,0), new Vector(0,1,0));

            var actual = w.ColorAt(r, 1);
            Assert.True(true);
        }

        [Fact]
        public void World_Refracted_Color()
        {
            var w = World.DefaultWorld();
            var s = w.Shapes[0];
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var xs =
                new Intersections(new Intersection[]
                    {
                        new Intersection(s, 4),
                        new Intersection(s, 6)
                    });
            var c = xs[0].GetComputation(r, xs);
            
            var actual = w.RefractedColor(c, 5);
            var expected = Color.Black;
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void World_Refracted_Color_At_Max_Recusive_Depth()
        {
            var w = World.DefaultWorld();
            var s = w.Shapes[0];
            s.Material.Transparency = 1.0;
            s.Material.RefractiveIndex = 1.5;
            var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            var xs =
                new Intersections(new Intersection[]
                    {
                        new Intersection(s, 4),
                        new Intersection(s, 6)
                    });
            var c = xs[0].GetComputation(r, xs);
            
            var actual = w.RefractedColor(c, 0);
            var expected = Color.Black;
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void World_Refracted_Color_Total_Internal_Reflection()
        {
            var n = Math.Sqrt(2);
            var w = World.DefaultWorld();
            var s = w.Shapes[0];
            s.Material.Transparency = 1.0;
            s.Material.RefractiveIndex = 1.5;
            var r = new Ray(new Point(0, 0, n/2), new Vector(0, 1, 0));
            var xs =
                new Intersections(new Intersection[]
                    {
                        new Intersection(s, -n/2),
                        new Intersection(s, n/2)
                    });
            var c = xs[1].GetComputation(r, xs);
            
            var actual = w.RefractedColor(c, 5);
            var expected = Color.Black;
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Refracted_Color_From_Refracted_Ray()
        {
            var w = World.DefaultWorld();
            var A = w.Shapes[0];
            var B = w.Shapes[1];
            A.Material.Ambient = 1.0;
            A.Material.Pattern = new TestPattern();
            B.Material.Transparency = 1.0;
            B.Material.RefractiveIndex = 1.5;
            var r = new Ray(new Point(0, 0, 0.1), new Vector(0, 1, 0));
            var xs = new Intersections(
                new Intersection[]
                {
                    new Intersection(A, -0.9899),
                    new Intersection(B,-0.4899), 
                    new Intersection(B, 0.4899), 
                    new Intersection(A, 0.9899)
                }        
            );
            var c = xs[2].GetComputation(r, xs);

            var actual = w.RefractedColor(c, 5);
            var expected = new Color(0, 0.99888, 0.0472);
            var res = expected.Equal(actual);

            Assert.True(res);
            //Assert.Equal(expected, actual);
        }

        [Fact]
        public void Shade_Hit_Transparent_Material()
        {
            var n = Math.Sqrt(2);
            var w = World.DefaultWorld();
            var m1 = new Material();
            m1.Transparency = 0.5;
            m1.RefractiveIndex = 1.5;
            var floor = new Plane(m1, Matrix4D.Translation(0, -1, 0));
            var shapes = w.Shapes;
            shapes = shapes.Append(floor).ToArray();
            var m2 = new Material();
            m2.Color = new Color(1, 0, 0);
            m2.Ambient = 0.5;
            var ball = new Sphere();
            ball.Material = m2;
            ball.Transformation = Matrix4D.Translation(0, -3.5, -0.5);
            shapes = shapes.Append(ball).ToArray();
            w.Shapes = shapes;
            var r = new Ray(new Point(0, 0, -3), new Vector(0, -n / 2, n / 2));
            var xs = new Intersections(new Intersection[] { new Intersection(floor, n) });
            var c = xs[0].GetComputation(r, xs);

            var expected = new Color(0.93642, 0.68642, 0.68642);
            var actual = w.ShadeHit(c, 5);
            var res = expected.Equal(actual);

            Assert.True(res);
            //Assert.Equal(expected, actual);
        }
    }
}
