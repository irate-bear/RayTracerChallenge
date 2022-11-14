using RayTracer.Tuple;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RayTracerDemo.Demos
{
    public class ProjectileDemo
    {
        private Ellipse ellipse;
        private TranslateTransform transform;
        private Projectile proj;
        private Environment env;
        private Canvas canvas;

        public ProjectileDemo(Canvas canvas)
        {
            this.canvas = canvas;
            StartDemo(canvas);
        }

        public Projectile Proj { get => proj; set => proj = value; }
        public Environment Env { get => env; set => env = value; }

        public void StartDemo(Canvas canvas)
        {
            ellipse = new Ellipse();
            ellipse.Height = 10;
            ellipse.Width = 10;
            ellipse.Fill = Brushes.Black;
            canvas.Children.Add(ellipse);

            transform = new TranslateTransform(20,1);
            ellipse.RenderTransform = transform;
            proj =
                new Projectile(new Point(20, 1, 0), new Vector(1, 1, 0).Normalize());
            env = new Environment(
                new Vector(0, 0.1, 0),
                new Vector(0.0000001, 0, 0)
            );
        }
        public void tick()
        {
            if ((proj.Position + proj.Velocity).Y < canvas.Height / 2)
            {
                var pos = proj.Position + proj.Velocity;
                var vel = proj.Velocity + env.Gravity + env.Wind;
                ellipse.SetValue(Canvas.LeftProperty, (double)pos.X);
                ellipse.SetValue(Canvas.TopProperty, (double)pos.Y);
                proj = new Projectile(pos, vel);
            }
        }
    }   
}
