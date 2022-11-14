using RayTracer.Matrices;
using RayTracer.Tuple;

namespace RayTracer.Primitives;
public class Ray
{
    public Ray(Point origin, Vector direction)
    {
        Origin = origin;
        Direction = direction;
    }

    public Point Origin { get; }
    public Vector Direction { get; }

    public Point Position(double t)
    {
        return Origin + Direction.Normalize() * t;
    }

    public Ray Transform(Matrix4D matrix)
    {
        return new Ray(matrix * Origin, matrix * Direction);
    }
}