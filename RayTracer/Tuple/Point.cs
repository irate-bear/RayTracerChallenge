namespace RayTracer.Tuple
{
    public class Point : Tuple 
    {
        public Point(params double[] values) : base (values)
        { 
        }

        public Point(double x, double y, double z) 
            : base (x, y, z, 1.0)
        {
            
        }

        public static Vector operator +(Point p1, Point p2)
        {
            return new Vector(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

         public static Point operator +(Point point, Vector vector)
        {
            return new Point(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);   
        }

        public static Vector operator -(Point p1, Point p2)
        {
            return new Vector(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        public static Point operator -(Point point, Vector vector)
        {
            return new Point(point.X - vector.X, point.Y - vector.Y, point.Z - vector.Z);   
        }

        public static Point operator -(Point point)
        {
            return new Point(-point.X, -point.Y, -point.Z);   
        }

        public bool Equal(Point p, double e)
        {
            return X.AbsDiff(p.X, e) &&
                Y.AbsDiff(p.Y, e) &&
                Z.AbsDiff(p.Z, e);
        }
    }    
}