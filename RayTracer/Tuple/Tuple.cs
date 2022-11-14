namespace RayTracer.Tuple
{
    public abstract class Tuple
    {
        public Tuple() : this (0.0, 0.0, 0.0, 0.0)
        {

        }

        public Tuple(params double[] values)
        {
            X = values.ElementAtOrDefault(0);
            Y = values.ElementAtOrDefault(1);
            Z = values.ElementAtOrDefault(2);
            W = values.ElementAtOrDefault(3);
        }

        public Tuple(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public double W { get; }

        public override bool Equals(object? obj)
        {
            bool res = false;
            if (obj is Tuple)
            {
                var tuple = (Tuple)obj;
                res = tuple.X.AbsDiff(X) &&
                      tuple.Y.AbsDiff(Y) &&
                      tuple.Z.AbsDiff(Z) &&
                      tuple.W.AbsDiff(W);
            }
            return res;
        }

        public static bool operator ==(Tuple t1, Tuple t2)
        {
            return t1.Equals(t2);
        }
        public static bool operator !=(Tuple t1, Tuple t2)
        {
            return !t1.Equals(t2);
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public double[] ToVector()
        {
            return new double[] { X, Y, Z, W };
        }
    }
}