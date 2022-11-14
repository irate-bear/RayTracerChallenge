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
    public class BlendedCheckered : IPattern
    {
        public BlendedCheckered(Color a, Color b, Matrix4D transformation)
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
            throw new NotImplementedException();
        }

        public Color PatternOnShape(IShape shape, Point point)
        {
            throw new NotImplementedException();
        }
    }
}
