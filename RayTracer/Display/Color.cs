namespace RayTracer.Display
{
    public class Color
    {
        public static Color Black = new Color(0.0, 0.0, 0.0);
        public static Color White = new Color(1.0, 1.0, 1.0);
        public Color() : this(0.0, 0.0, 0.0) { }
        public Color(double red, double green, double blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public double Red { get; }
        public double Green { get; }
        public double Blue { get; }

        public int[] To256Bit()
        {
            double green = Math.Clamp(Green, 0, 1);
            double blue = Math.Clamp(Blue, 0, 1);
            double red = Math.Clamp(Red, 0, 1);
            int norm = 255;
            return new int[] { (red * norm).Round(), (green * norm).Round(), (blue * norm).Round() };
        }

        public static Color operator +(Color c1, Color c2)
        {
            return new Color(c1.Red + c2.Red, c1.Green + c2.Green, c1.Blue + c2.Blue);
        }

        public static Color operator -(Color c1, Color c2)
        {
            return new Color(c1.Red - c2.Red, c1.Green - c2.Green, c1.Blue - c2.Blue);
        }

        public static Color operator *(Color c1, Color c2)
        {
            return new Color(c1.Red * c2.Red, c1.Green * c2.Green, c1.Blue * c2.Blue);
        }

        public static Color operator *(double a, Color c2)
        {
            return new Color(a * c2.Red, a * c2.Green, a * c2.Blue);
        }

        public static Color operator *(Color c1, double a)
        {
            return new Color(c1.Red * a, c1.Green * a, c1.Blue * a);
        }

        public override bool Equals(object? obj)
        {
            bool res = false;

            if (obj is Color)
            {
                Color t = (Color)obj;
                Color temp = new Color(t.Red, t.Green, t.Blue);
                res = temp.Red.AbsDiff(Red) && 
                      temp.Green.AbsDiff(Green) && 
                      temp.Blue.AbsDiff(Blue);
            }

            return res;
        }

        public static bool operator ==(Color c1, Color c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Color c1, Color c2)
        {
            return !c1.Equals(c2);
        }

        public bool Equal(Color c)
        {
            var e = 1e-3;
            return Red.AbsDiff(c.Red, e) &&
                Green.AbsDiff(c.Green, e) &&
                Blue.AbsDiff(c.Blue, e);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Red,Green,Blue);
        }
    }
}
