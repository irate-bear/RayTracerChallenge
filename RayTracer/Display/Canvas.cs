using System.Collections;
using System.Linq;

namespace RayTracer.Display
{
    public struct Pixel
    {
        public int X;
        public int Y;
        public Color Color;
    }
    public class Canvas : IEnumerable<Color>
    {
        private readonly Color[,] pixels;
        public Canvas(Color[,] pixels, int height, int width)
        {
            this.pixels = pixels;
            Height = height;
            Width = width;
        }

        public Canvas(int width, int height) : this (width, height, new Color()) { }

        public Canvas(int width, int height, Color color)        {
            pixels = new Color[height, width];
            //pixels.AsParallel().OfType<Color>().ForAll(pixel => pixel = color);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pixels[y,x] = color;
                }
            }
            Height = height;
            Width = width;
        }

        public int Height { get; }
        public int Width { get; }

        public IEnumerator<Color> GetEnumerator()
        {
            foreach (var color in pixels)
            {
                yield return color;
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Color this[int row, int col]
        {
            set => pixels[col, row] = value;
            get => pixels[col, row];
        }
    }
}
