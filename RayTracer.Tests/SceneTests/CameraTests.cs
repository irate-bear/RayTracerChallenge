using RayTracer.Display;
using RayTracer.Matrices;
using RayTracer.Scene;
using RayTracer.Tests.ExtensionTests;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Tests.SceneTests
{
    public class CameraTests
    {
        [Theory]
        [InlineData(200, 125)]
        [InlineData(125, 200)]
        public void Camera_Pixel_Size(double h, double w)
        {
            var c = new Camera(h, w, Math.PI / 2);

            var expected = 0.01; 
            var actual = c.PixelSize;
            var res = expected.AbsDiff(actual);

            Assert.True(res); 
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Camera_Ray_For_Pixel(Matrix4D transform, double x, double y, Point p, Vector v)
        {
            var pi = Math.PI;
            var c = new Camera(201, 101, pi / 2, transform);
            var r = c.RayForPixel(x, y);

            var e = 1e-5;
            var res = r.Origin.Equal(p, e) &&
                r.Direction.Equal(v, e);

            //Assert.True(res);
            //Assert.Equal(r.Direction, v);
            Assert.Equal(r.Origin, p);
        }

        [Fact]
        public void Camera_Render()
        {
            var w = World.DefaultWorld();
            Point from = new(0, 0, -5);
            Point to = new(0, 0, 0);
            Vector up = new(0, 1, 0);
            var c = new Camera(11, 11, Math.PI / 2, Matrix4D.ViewTransform(from, to, up));
            var image = c.Render(w);

            var expected = new Color(0.38066, 0.47583, 0.2855);
            var actual = image[5, 5];
            var res = expected.Equal(actual);

            Assert.True(res);
        }

        public static IEnumerable<object[]> GetTestData()
        {
            var n = Math.Sqrt(2);
            yield return new object[] 
            { 
                Matrix4D.Identity,
                100, 50,
                new Point(0,0,0), 
                new Vector(0,0,-1) 
            };
            yield return new object[] 
            { 
                Matrix4D.RotationY(Math.PI / 4) * Matrix4D.Translation(0, -2, 5), 
                100, 50,
                new Point(0, 2, -5), 
                new Vector(n / 2, 0, -n / 2) 
            };
           /*
            yield return new object[] 
            { 
                Matrix4D.Identity, 
                0, 0,
                new Point(0, 0, 0), 
                new Vector(0.66519, 0.33259, -0.66851) 
            };
           */
        }
    }
}
