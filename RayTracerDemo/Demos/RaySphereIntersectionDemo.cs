using RayTracer.Display;
using RayTracer.Matrices;
using RayTracer.Primitives;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using Canv = RayTracer.Display.Canvas;
using Canvas = System.Windows.Controls.Canvas;
using Color = RayTracer.Display.Color;

namespace RayTracerDemo.Demos
{
    public class RaySphereIntersectionDemo
    {
        static IShape shape;

        public static void RunDemo(Canvas canvas)
        {
            shape = new Sphere();
            //shape.Transformation = Matrix4D.Scaling(1, 0.5, 1);
            //shape.Transformation = Matrix4D.Scaling(0.5, 1, 1);
            //shape.Transformation = Matrix4D.RotationZ(Math.PI / 4) * Matrix4D.Scaling(0.5, 1, 1);
            shape.Transformation = Matrix4D.Shearing(1, 0, 0, 0, 0, 0) * Matrix4D.Scaling(0.5, 1, 1);
            Point ray_origin = new Point(0, 0, -5);
            double wall_z = 10;
            double wall_size = 7.0;
            int canvas_pixels = 100;
            int canvas_height_scale = (int)(canvas.Height / canvas_pixels);
            int canvas_width_scale = (int)(canvas.Width / canvas_pixels);
            var pixel_size = wall_size / canvas_pixels;
            double half = wall_size / 2;

            Canv canv = new Canv(canvas_pixels, canvas_pixels);
            Color color = new Color(1, 0, 0);
            Brush brush = new SolidColorBrush(System.Windows.Media.Color
                .FromArgb(255, (byte)(color.Red * 255), (byte)(color.Green * 255), (byte)(color.Blue * 255)));
            
            for (int y = 0; y < canvas_pixels; y++)
            {
                double world_y = half - pixel_size * y;

                for (int x = 0; x < canvas_pixels; x++)
                {
                    double world_x = pixel_size * x - half;
                    Point position = new Point(world_x, world_y, wall_z);
                    Ray ray = new Ray(ray_origin, (position - ray_origin).Normalize());
                    double[] xs = shape.Intersects(ray);
                    Intersections intersects = new Intersections();
                    intersects.AddIntersection(shape, xs);
                    if (intersects.Hit() is not null)
                    {
                        Rectangle ellipse = new Rectangle();
                        ellipse.StrokeThickness = 0.0;
                        ellipse.Height = canvas_height_scale;
                        ellipse.Width = canvas_width_scale;
                        ellipse.Fill = brush;
                        canvas.Children.Add(ellipse);
                        ellipse.SetValue(Canvas.LeftProperty, (double)(x * canvas_width_scale));
                        ellipse.SetValue(Canvas.TopProperty, (double)(y * canvas_height_scale));

                        canv[x, y] = color;
                        //canvas
                    }
                }
            }
            WriteFile(Image.CanvasToPPMString(canv));
        }

        public static void WriteFile(string text)
        {
            File.WriteAllText("C:\\Users\\Obakeng\\Desktop\\RayTracerImages\\sphere_ray_intersection4.ppm", text);
        }
    }
}
