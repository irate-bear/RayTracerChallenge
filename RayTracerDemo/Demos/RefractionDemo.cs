using RayTracer.Display;
using RayTracer.Materials;
using RayTracer.Matrices;
using RayTracer.Primitives;
using RayTracer.Scene;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using Canv = RayTracer.Display.Canvas;
using Color = RayTracer.Display.Color;

namespace RayTracerDemo.Demos
{
    public class RefractionDemo
    {
        public static void RunDemo(System.Windows.Controls.Canvas canvas)
        {
            IShape middle = new Sphere();
            middle.Transformation = Matrix4D.Translation(-0.5, 1, 0.5);
            middle.Material = new Material();
            //middle.Material.Color = new Color(0.1, 1, 0.5);
            middle.Material.Color = new Color(1, 1, 1);
            middle.Material.Diffuse = 0.2;
            middle.Material.Specular = 0.3;
            middle.Material.Transparency = 1;
            middle.Material.Reflective = 1;
            middle.Material.RefractiveIndex = 1.5;
            //middle.Material.Pattern = new StripePattern(new Color(0.05, 0.5, 0.25), new Color(0.1, 1, 0.5));
            //middle.Material.Pattern.Transformation = 
            //    Matrix4D.Scaling(.25, .25, .25) * Matrix4D.RotationZ(-Math.PI / 4) * Matrix4D.RotationY(-Math.PI / 6);
            
            /*IShape middle = new Sphere();
            middle.Material.Reflective = 1;
            middle.Transformation = Matrix4D.Translation(-0.5, 1, 0.5);
            middle = CreateShape.CreateGlassShape(middle);
            */
            IShape right = new Sphere();
            right.Transformation = Matrix4D.Translation(1.5, 0.5, -0.5) * Matrix4D.Scaling(0.5, 0.5, 0.5);
            right.Material = new Material();
            right.Material.Color = new Color(0.5, 1, 0.1);
            right.Material.Diffuse = 0.7;
            right.Material.Specular = 0.3;
            right.Material.Reflective = 1.0;
            right.Material.Pattern =
                new GradientPattern(new Color(1, 0.5, 0.25), new Color(1, 1, 0));
            right.Material.Pattern.Transformation =
                Matrix4D.RotationY(-Math.PI / 2);

            IShape left = new Sphere();
            left.Transformation = Matrix4D.Translation(-1.5, 0.33, -0.75) * Matrix4D.Scaling(0.33, 0.33, 0.33);
            left.Material = new Material();
            left.Material.Color = new Color(0.25, 0.25, 0.25);
            left.Material.Diffuse = 0.7;
            left.Material.Specular = 0.3;
            left.Material.Pattern = new RingPattern(new Color(0.75, 1, 0.75), new Color(0.2, 2, 1));
            left.Material.Pattern.Transformation =
                Matrix4D.Scaling(0.2, 0.2, 0.2) * Matrix4D.RotationX(Math.PI / 2);

            IShape plane = new Plane();
            IPattern p1 =
                new CheckeredPattern(Color.White, new Color(1, 0, 0), Matrix4D.RotationY(Math.PI / 2)); 
            IPattern p2 =
                new CheckeredPattern(new Color(1, 1, 0), Color.White); 
            //plane.Material.Pattern = new CheckeredPattern(Color.White, new Color(1, 0, 0), Matrix4D.RotationY(Math.PI / 2));
            plane.Material.Pattern = new BlendedPattern(p1, p2);
            plane.Material.RefractiveIndex = 1.6;
            plane.Material.Reflective = 1.0;
            //plane.Material.Shininess = 1.0;

            ILight light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1));
            World world = new World(light, new IShape[] { middle, right, left, plane });
            //World world = World.DefaultWorld();

            Matrix4D transform =
                Matrix4D.ViewTransform(new Point(0, 1.5, -5), new Point(0, 1, 0), new Vector(0, 1, 0));
            Camera camera = new Camera(500, 500, Math.PI/3, transform);

            Canv canv = camera.Render(world);
            WriteFile(Image.CanvasToPPMString(canv));
            ShowDemo(canvas, canv, camera);
        }

        public static void ShowDemo(System.Windows.Controls.Canvas canvas, Canv canv, Camera camera)
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
                    ellipse.Fill = getBrush(canv[x, y]);
                    canvas.Children.Add(ellipse);
                    ellipse.SetValue(System.Windows.Controls.Canvas.LeftProperty, (double)(x * pixel_width));
                    ellipse.SetValue(System.Windows.Controls.Canvas.TopProperty, (double)(y * pixel_height));
                }
            }
        }

        public static void WriteFile(string text)
        {
            File.WriteAllText("C:\\Users\\Obakeng\\Desktop\\RayTracerImages\\patterns_demo.ppm", text);
        }
    }
}
