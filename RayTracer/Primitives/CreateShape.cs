using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Primitives
{
    public static class CreateShape
    {
        public static IShape CreateGlassShape(IShape shape)
        {
            shape.Material.Transparency = 1.0;
            shape.Material.RefractiveIndex = 1.5;
            return shape;
        }
    }
}
