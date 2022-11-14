using RayTracer;
using RayTracer.Display;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Shapes;
//using System.Windows.Controls;
//using System.Windows;

namespace RayTracerDemo.Demos
{
    public class ProjectileCanvasDemo
    {
        Environment env;
        Projectile proj;
        Canvas canvas;
        //private Ellipse ellipse;
        //private TranslateTransform transform;

        public ProjectileCanvasDemo(int height, int width)//, int height, int width)
        {
            this.canvas = /*canvas;*/ new Canvas(height, width);
            Init();
        }
        public void Init()
        {
            //ellipse = new Ellipse();
            //ellipse.Height = 10;
            //ellipse.Width = 10;
            //ellipse.Fill = Brushes.Black;
            //canvas.Children.Add(ellipse);

            //transform = new TranslateTransform(0,1);
            //ellipse.RenderTransform = transform;

            Point start = new Point(0, 400, 0);
            Vector velocity = new Vector(0.1, -0.2, 0).Normalize();

            env = new Environment(new Vector(1, 2, 0), new Vector(1, -2, 0));

            proj = new Projectile(start, velocity);
        }

        public string tick()
        {
            string status = "";
            canvas[0, 0] = new Color(1.0, 1.0, 1.0);
            if (proj.Position.X.Round() < canvas.Width)
            {
                canvas[(int)proj.Position.X, (int)proj.Position.Y] = new Color(1.0, 0.0, 0.0);
                status = "running";
            }
            else
            {
                string image = Image.CanvasToPPMString(canvas);
                WriteFile(image);
                status = "Done"; 
            }
            //ellipse.SetValue(Canvas.LeftProperty, (double)proj.Position.X);
            //ellipse.SetValue(Canvas.TopProperty, (double)proj.Position.Y);

            if (proj.Position.Y < 10)
            {
                proj.Velocity = new Vector(0.1, 0.2, 0).Normalize();
            }
            if (proj.Position.Y > 400)
            {
                proj.Velocity = new Vector(0.1, -0.2, 0).Normalize();
            }
            var pos = proj.Position + proj.Velocity;
            //var vel = proj.Velocity + env.Gravity + env.Wind;
            proj = new Projectile(pos, proj.Velocity);
            return status;
        }

        public static void WriteFile(string text)
        {
            File.WriteAllText("C:\\Users\\Obakeng\\Desktop\\RayTracerImages\\image.ppm", text);
        }
    }
}
