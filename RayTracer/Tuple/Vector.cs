using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Tuple
{
    public class Vector : Tuple
    {
        public Vector() : base ()
        {
        }


        public Vector(params double[] values) : base(values)
        {
        }

        public Vector(double x, double y, double z) : base(x, y, z, 0.0)
        {
        }

        public Vector(double x, double y, double z, double w) : base(x, y, z, w)
        {
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }            

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }            

        public static Vector operator -(Vector v1)
        {
            return new Vector(-v1.X, -v1.Y, -v1.Z, -v1.W);
        }
        
        public static Vector operator *(double a, Vector v)
        {
            return new Vector(a * v.X, a * v.Y, a * v.Z, a * v.W);
        }

        public static Vector operator *(Vector v, double a)
        {
            return new Vector(a * v.X, a * v.Y, a * v.Z, a * v.W);
        }

        public static Vector operator /(Vector v, double a)
        {
            var b = 1 / a;
            return new Vector(a * v.X, a * v.Y, a * v.Z, a * v.W);
        }

        public double MagnitudeSqrd()
        {
            return X * X + Y * Y + Z * Z + W * W;
        }

        public double Magnitude()
        {
            return Math.Sqrt(MagnitudeSqrd());
        }

        public Vector Normalize()
        {
            var mag = 1 / Magnitude();
            return new Vector(mag * X, mag * Y, mag * Z, mag * W);
        }

        public double Dot(Vector v)
        {
            return v.X * X + v.Y * Y + v.Z * Z + v.W * W;
        } 

        public Vector Cross(Vector v)
        {
            return new Vector(
                Y * v.Z - Z * v.Y,
                Z * v.X - X * v.Z,
                X * v.Y - Y * v.X
            );
        }

        public Vector Reflect(Vector normal)
        {
            return this - normal * 2 * Dot(normal);
        }

        public bool Equal(Vector v, double e)
        {
            return X.AbsDiff(v.X, e) &&
                Y.AbsDiff(v.Y, e) &&
                Z.AbsDiff(v.Z, e);
        }
    }
}
