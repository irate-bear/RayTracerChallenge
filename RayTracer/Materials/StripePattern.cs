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
    public class StripePattern : IPattern
    {
        public StripePattern(Color a, Color b) : this (a, b, Matrix4D.Identity)
        {
        }

        public StripePattern(Color a, Color b, Matrix4D transformation)
        {
            A = a;
            B = b;
            Transformation = transformation;
        }

        public Color A { get; }
        public Color B { get; }
        public Matrix4D Transformation { get; set; }

        public Color ColorAt(Point point)
        {
            return ((int)Math.Floor(point.X) % 2 == 0) ? A : B; 
        }

        public Color PatternOnShape(IShape shape, Point point)
        {
            Point objpt = shape.Transformation.Inverse() * point;
            Point patpt = Transformation.Inverse() * objpt;

            return ColorAt(patpt);
        }
    }
}
