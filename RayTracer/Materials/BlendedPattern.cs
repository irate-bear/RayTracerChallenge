using RayTracer.Display;
using RayTracer.Matrices;
using RayTracer.Primitives;
using RayTracer.Tuple;

namespace RayTracer.Materials
{
    public class BlendedPattern : IPattern
    {
        public BlendedPattern(IPattern a, IPattern b) : this(a, b, Matrix4D.Identity)
        {

        }
        public BlendedPattern(IPattern a, IPattern b, Matrix4D transformation)
        {
            A = a;
            B = b;
            Transformation = transformation;
        }
        public Matrix4D Transformation { get; set; }
        public IPattern A { get; }
        public IPattern B { get; }

        Color IPattern.A => throw new NotImplementedException();

        Color IPattern.B => throw new NotImplementedException();

        public Color ColorAt(Point point)
        {
            Color c1 = A.ColorAt(point);
            Color c2 = B.ColorAt(point);
            return (c1 + c2) * 0.5;
        }

        public Color PatternOnShape(IShape shape, Point point)
        {
            Point objpt = shape.Transformation.Inverse() * point;
            Point patpt = Transformation.Inverse() * objpt;

            return ColorAt(patpt);
    }
    }
}
