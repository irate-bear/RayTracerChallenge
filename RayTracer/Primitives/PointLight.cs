using RayTracer.Display;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Primitives
{
    public class PointLight : ILight
    {
        public PointLight()
        {
        }

        public PointLight(Point position, Color color)
        {
            Position = position;
            Intensity = color;
        }

        public Point Position { get; }
        public Color Intensity { get; }
    }
}
