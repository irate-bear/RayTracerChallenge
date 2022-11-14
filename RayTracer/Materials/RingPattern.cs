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
    public class RingPattern : IPattern
    {
        public RingPattern(Color a, Color b) : this (a, b, Matrix4D.Identity)
        {

        }
        public RingPattern(Color a, Color b, Matrix4D transformation)
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
            var v = Math.Floor(Math.Sqrt(Math.Pow(point.X, 2) + Math.Pow(point.Z, 2)));
            return v % 2 == 0 ? A : B;
        }

        public Color PatternOnShape(IShape shape, Point point)
        {
            Point objpt = shape.Transformation.Inverse() * point;
            Point patpt = Transformation.Inverse() * objpt;

            return ColorAt(patpt);
        }
    }
}
