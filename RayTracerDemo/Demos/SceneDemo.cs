using RayTracer.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Canvas = System.Windows.Controls.Canvas;
using Canv = RayTracer.Display.Canvas;
using RayTracer.Primitives;
using RayTracer.Matrices;
using RayTracer.Scene;
using RayTracer.Tuple;
using System.IO;
using System.Windows.Media;
using Color = RayTracer.Display.Color;
using System.Windows.Shapes;
using RayTracer.Materials;

namespace RayTracerDemo.Demos
{
    public class SceneDemo
    {
        public static void RunDemo(Canvas canvas)
        {
            IShape floor = new Sphere();
            floor.Transformation = Matrix4D.Scaling(10, 0.01, 10);
            Material material = new Material();
            material.Color = new Color(1, 0.9, 0.9);
            material.Specular = 0;
            floor.Material = material;

            IShape left_wall = new Sphere();
            left_wall.Transformation = Matrix4D.Translation(0, 0, 5) *
                Matrix4D.RotationY(-Math.PI / 4) * Matrix4D.RotationX(Math.PI / 2) *
                Matrix4D.Scaling(10, 0.01, 10);
            left_wall.Material = material;

            IShape right_wall = new Sphere();
            right_wall.Transformation = Matrix4D.Translation(0, 0, 5) *
                Matrix4D.RotationY(Math.PI / 4) * Matrix4D.RotationX(Math.PI / 2) *
                Matrix4D.Scaling(10, 0.01, 10);
            right_wall.Material = material;

            IShape middle = new Sphere();
            middle.Transformation = Matrix4D.Translation(-0.5, 1, 0.5);
            middle.Material = new Material();
            middle.Material.Color = new Color(0.1, 1, 0.5);
            middle.Material.Diffuse = 0.7;
            middle.Material.Specular = 0.3;

            IShape right = new Sphere();
            right.Transformation = Matrix4D.Translation(1.5, 0.5, -0.5) * Matrix4D.Scaling(0.5, 0.5, 0.5);
            right.Material = new Material();
            right.Material.Color = new Color(0.5, 1, 0.1);
            right.Material.Diffuse = 0.7;
            right.Material.Specular = 0.3;

            IShape left = new Sphere();
            left.Transformation = Matrix4D.Translation(-1.5, 0.33, -0.75) * Matrix4D.Scaling(0.33, 0.33, 0.33);
            left.Material = new Material();
            left.Material.Color = new Color(1, 0.8, 0.1);
            left.Material.Diffuse = 0.7;
            left.Material.Specular = 0.3;

            ILight light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1));
            World world = new World(light, new IShape[] { floor, left_wall, right_wall/*, left, middle, right*/ });
            //World world = World.DefaultWorld();

            Matrix4D transform = 
                Matrix4D.ViewTransform(new Point(0, 1.5, -5), new Point(0,1,0), new Vector(0,1,0));
            Camera camera = new Camera(100, 100, Math.PI / 3, transform);

            Canv canv = camera.Render(world);
            WriteFile(Image.CanvasToPPMString(canv));
            ShowDemo(canvas, canv);
        }

        public static void ShowDemo(Canvas canvas, Canv canv)
        {
            int pixel_height = (int)(canvas.Height / canv.Height);
            int pixel_width = (int)(canvas.Width / canv.Width);

            
            static Brush getBrush(Color color) => new SolidColorBrush(System.Windows.Media.Color
                .FromArgb(255, (byte)(color.Red * 255), (byte)(color.Green * 255), (byte)(color.Blue * 255)));

            for (int y = 0; y < canv.Height; y++)
            {
                for (int x = 0; x < canv.Width; x++)
                {
                    Rectangle ellipse = new Rectangle();
                    ellipse.StrokeThickness = 0.0;
                    ellipse.Height = pixel_height;
                    ellipse.Width = pixel_width;
                    ellipse.Fill = getBrush(canv[x,y]);
                    canvas.Children.Add(ellipse);
                    ellipse.SetValue(Canvas.LeftProperty, (double)(x * pixel_width));
                    ellipse.SetValue(Canvas.TopProperty, (double)(y * pixel_height));
                }
            }
        }

        public static void WriteFile(string text)
        {
            File.WriteAllText("C:\\Users\\Obakeng\\Desktop\\RayTracerImages\\scene2.ppm", text);
        }
    }
}
