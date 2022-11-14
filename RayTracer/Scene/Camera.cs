using RayTracer.Display;
using RayTracer.Matrices;
using RayTracer.Primitives;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Scene
{
    public class Camera
    {
        public Camera(double hsize, double vsize, double field_of_view)
            : this(hsize, vsize, field_of_view, Matrix4D.Identity)
        {

        }

        public Camera(double hsize, double vsize, double field_of_view, Matrix4D transform)
        {
            Hsize = hsize;
            Vsize = vsize;
            FieldOfView = field_of_view;
            Transform = transform;

            double halfView = Math.Tan(FieldOfView / 2);
            double aspect = Hsize / Vsize;

            double halfWidth = halfView * aspect;
            double halfHeight = halfView;
            
            if (aspect >= 1)
            {
                halfWidth = halfView;
                HalfHeight = halfView / aspect;
            }
            HalfHeight = halfHeight;
            HalfWidth = halfWidth;

            PixelSize = (halfWidth * 2) / hsize;
        }

        public double Hsize { get; set; }
        public double Vsize { get; set; }
        public double FieldOfView { get; set; }
        public Matrix4D Transform { get; }
        public double PixelSize { get; }
        public double HalfWidth { get; }
        public double HalfHeight { get; }

        public Ray RayForPixel(double x, double y)
        {
            double xoff = (x + 0.5) * PixelSize;
            double yoff = (y + 0.5) * PixelSize;

            double xworld = HalfWidth - xoff;
            double yworld = HalfHeight - yoff;

            var p1 = new Point(xworld, yworld, -1);
            var p2 = new Point(0,0,0);
            Point pixel = Transform.Inverse() * p1;
            Point origin = Transform.Inverse() * p2;
            Vector direction = (pixel - origin).Normalize();

            return new Ray(origin, direction);
        }

        public Canvas Render(World world)
        {
            Canvas image = new Canvas((int)Hsize, (int)Vsize);
            for (int y = 0; y < Vsize; y++)
            {
                for (int x = 0; x < Hsize; x++)
                {
                    Ray ray = RayForPixel(x, y);
                    Color color = world.ColorAt(ray);
                    image[x,y] = color;
                }
            }
            return image;
        }
    }
}
