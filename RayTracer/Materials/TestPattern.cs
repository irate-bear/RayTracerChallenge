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
    public class TestPattern : IPattern
    {
        public TestPattern()
        {
            Transformation = Matrix4D.Identity;
        }
        public Color A => throw new NotImplementedException();

        public Color B => throw new NotImplementedException();

        public Matrix4D Transformation { get; set; }

        public Color ColorAt(Point point)
        {
            return new Color(point.X, point.Y, point.Z);
        }

        public Color PatternOnShape(IShape shape, Point point)
        {
            return ColorAt(point);
        }
    }
}
