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
    public class Sphere : IShape
    {
        public Sphere() : this (new Point(0.0, 0.0, 0.0), 1, Matrix4D.Identity, new Material())
        {

        }

        public Sphere(Point center, double radius) :this (center, radius, Matrix4D.Identity, new Material())
        {

        }

        public Sphere(Point center, double radius, Matrix4D transformation, Material material)
        {
            Center = center;
            Radius = radius;
            Transformation = transformation;
            Material = material;
        }

        public Point Center { get; }
        public double Radius { get; }
        public Matrix4D Transformation { get; set; }
        public Material Material { get; set; }

        public double[] Intersects(Ray ray)
        {
            ray = ray.Transform(Transformation.Inverse());
            double[] intersects;
            Vector sphere_to_ray = ray.Origin - Center;
            double a = ray.Direction.Dot(ray.Direction);
            double b = 2 * ray.Direction.Dot(sphere_to_ray);
            double c = sphere_to_ray.Dot(sphere_to_ray) - Radius;

            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                intersects = new double[] { };
            }
            else
            {
                double t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                double t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                intersects = new double[] { t1, t2 };
            }
            return intersects;
        }

        public Vector NormalAt(Point p)
        {
            Point point = Transformation.Inverse() * p;
            Vector vector = point - Center;

            Vector normal = Transformation.Inverse().Transpose() * vector;
            normal = new Vector(normal.X, normal.Y, normal.Z, 0.0);

            return normal.Normalize();
        }
    }
}
