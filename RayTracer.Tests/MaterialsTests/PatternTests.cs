using RayTracer.Display;
using RayTracer.Materials;
using RayTracer.Matrices;
using RayTracer.Primitives;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Tests.MaterialsTests
{
    public class PatternTests
    {
        static Color black = Color.Black;
        static Color white = Color.White;

        [Theory]
        [InlineData(0,0,0,1)]
        [InlineData(0,0,1,1)]
        [InlineData(0,0,2,1)]
        [InlineData(0,1,0,1)]
        [InlineData(0,2,0,1)]
        [InlineData(0.9,0,0,1)]
        [InlineData(1,0,0,0)]
        [InlineData(-0.1,0,0,0)]
        [InlineData(-1,0,0,0)]
        [InlineData(-1.1,0,0,1)]
        public void Striped_Pattern_Color_At(double x, double y, double z, int color)
        {
            var pattern = new StripePattern(white, black);

            var actual = pattern.ColorAt(new RayTracer.Tuple.Point(x, y, z));
            var expected = color == 0 ? black : white;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0.9,1)]
        [InlineData(1.1,0)]
        public void Stripe_Pattern_With_Lighting(double x, int color)
        {
            var material = new Material();
            material.Ambient = 1;
            material.Diffuse = 0;
            material.Specular = 0;
            material.Pattern = new StripePattern(white, black);
            var eye = new Vector(0, 0, -1);
            var normal = new Vector(0, 0, -1);
            var light = new PointLight(new Point(0, 0, -10), white);

            var expected = color == 0 ? black : white;
            var actual = material.Lighting(new Sphere(), light, new Point(x, 0, 0), eye, normal, false);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Stripe_Pattern_Transform(Matrix4D shapeTransform, Matrix4D patternTransform, Point point, Color color)
        {
            var shape = new Sphere();
            shape.Transformation = shapeTransform;
            var pattern = new StripePattern(white, black);
            pattern.Transformation = patternTransform;

            var expected = color;
            var actual = pattern.PatternOnShape(shape, point);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0,0,0,1,1,1)]
        [InlineData(0.25,0,0,0.75,0.75,0.75)]
        [InlineData(0.5,0,0,0.5,0.5,0.5)]
        [InlineData(0.75,0,0,0.25,0.25,0.25)]
        public void Gradient_Pattern_Color_At(double x, double y, double z, double u, double v, double w)
        {
            var gradient = new GradientPattern(white, black);

            var expected = new Color(u, v, w);
            var actual = gradient.ColorAt(new Point(x, y, z));
        }

        [Theory]
        [InlineData(0,0,0,0)]
        [InlineData(1,0,0,1)]
        [InlineData(0,0,1,1)]
        [InlineData(0.708,Math.PI/2,Math.PI/2,1)]
        public void Ring_Pattern_Color_At(double x, double y, double z, int color)
        {
            var pattern = new RingPattern(white, black);

            var expected = color == 0 ? white : black;
            var actual = pattern.ColorAt(new Point(x, y, z));

            Assert.Equal(expected, actual);
        }
    
        [Theory]
        [InlineData(0,0,0,0)]
        [InlineData(0.99,0,0,0)]
        [InlineData(1.01,0,0,1)]
        [InlineData(0,0.99,0,0)]
        [InlineData(0,1.01,0,1)]
        [InlineData(0,0,0.99,0)]
        [InlineData(0,0,1.01,1)]
        public void Checkered_Pattern_Color_At(double x, double y, double z, int color)
        {
            var pattern = new CheckeredPattern(white, black);

            var expected = color == 0 ? white : black;
            var actual = pattern.ColorAt(new Point(x, y, z));

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> GetData()
        {
            yield return new object[] 
            { 
                Matrix4D.Scaling(2, 2, 2), 
                Matrix4D.Identity, 
                new Point(1.5, 0, 0), 
                Color.White 
            };
            yield return new object[] 
            { 
                Matrix4D.Identity,
                Matrix4D.Scaling(2, 2, 2), 
                new Point(1.5, 0, 0), 
                Color.White 
            };
            yield return new object[] 
            { 
                Matrix4D.Scaling(2, 2, 2), 
                Matrix4D.Translation(0.5, 0, 0),
                new Point(2.5, 0, 0),
                Color.White
            };
        }
    }
}
