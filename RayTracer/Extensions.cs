namespace RayTracer
{
    public static class Extensions
    {
        public static readonly double EPSILON = 1e-7;
        public static bool AbsDiff(this double a, double b)
        {
            return Math.Abs(a - b) <= EPSILON;    
        }

        public static bool AbsDiff(this double a, double b, double epsilon)
        {
            return Math.Abs(a - b) <= epsilon;    
        }

        public static int Round(this double a)
        {
            return (int)Math.Round(a);
        }
    }
}
