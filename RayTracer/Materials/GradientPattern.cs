using RayTracer.Display;
using RayTracer.Matrices;
using RayTracer.Primitives;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Materials
{
    public class GradientPattern : IPattern
    {
        public GradientPattern(Color a, Color b) : this (a, b, Matrix4D.Identity)
        {

        }
        public GradientPattern(Color a, Color b, Matrix4D transformation)
        {
            A = a;
            B = b;
            Transformation = transformation;
        }
        public Matrix4D Transformation { get; set; }
        public Color A { get; }
        public Color B { get; }

        public Color ColorAt(Point point)
        {
            Color distance = B - A;
            var fraction = point.X - Math.Floor(point.X);

            return A + distance * fraction;
        }

        public Color PatternOnShape(IShape shape, Point point)
        {
            Point objpt = shape.Transformation.Inverse() * point;
            Point patpt = Transformation.Inverse() * objpt;

            return ColorAt(patpt);
        }
    }
}
