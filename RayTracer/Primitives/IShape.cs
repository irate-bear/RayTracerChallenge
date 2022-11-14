using RayTracer.Materials;
using RayTracer.Matrices;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Primitives
{
    public interface IShape
    {
        double[] Intersects(Ray ray);
        Matrix4D Transformation { get;  set; }
        Vector NormalAt(Point p);
        Material Material { get; set; }
    }
}
