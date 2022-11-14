using RayTracer.Display;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Primitives
{
    public interface ILight
    {
        Point Position { get; }
        Color Intensity { get; }
    }
}
