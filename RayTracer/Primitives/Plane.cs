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
    public class Plane : IShape
    {
        public Plane() : this (new Material(), Matrix4D.Identity)
        {

        }

        public Plane(Material material, Matrix4D transformation)
        {
            Material = material;
            Transformation = transformation;
        }
        public Matrix4D Transformation { get; set; }
        public Material Material { get; set; }

        public double[] Intersects(Ray ray)
        {
            Ray localRay = ray.Transform(Transformation.Inverse());
            
            if (Math.Abs(localRay.Direction.Y) < Extensions.EPSILON)
            {
                return new double[0];
            }

            double t = -localRay.Origin.Y / localRay.Direction.Y;
            return new double[] { t };
        }

        public Vector NormalAt(Point p)
        {
            Point point = Transformation.Inverse() * p;
            Vector vector = new Vector(p.X, p.Y, p.Z);

            Vector normal = Transformation.Inverse().Transpose() * vector;
            normal = new Vector(normal.X, normal.Y, normal.Z, 0.0);

            return new Vector(0, 1, 0) * Transformation;
        }
    }
}
