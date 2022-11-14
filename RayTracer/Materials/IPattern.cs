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
    public interface IPattern
    {
        Color A { get; }
        Color B { get; }
        Color ColorAt(Point point);
        Color PatternOnShape(IShape shape, Point point);
        Matrix4D Transformation { get; set; }
    }
}
