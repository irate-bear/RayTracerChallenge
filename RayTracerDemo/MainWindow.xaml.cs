using RayTracerDemo.Demos;
using System;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Numerics;

namespace RayTracerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        ProjectileDemo projectileDemo;
        ProjectileCanvasDemo canvasDemo;
        MatrixTransformationDemo transformationDemo;
        Canvas canvas;
        Label lbl;
        public MainWindow()
        {
            InitializeComponent();

            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(1);

            canvas = (Canvas)this.FindName("displayCanvas");
            lbl = (Label)this.FindName("label");

            //Projectile Demo
            projectileDemo = new ProjectileDemo(canvas);
            //Canvas Demo
            canvasDemo = new ProjectileCanvasDemo(900, 550);
            //Matrix Transformation Demos
            transformationDemo = new MatrixTransformationDemo();
            //RaySphereIntersectionDemo.RunDemo(canvas);

            //LightingDemo.RunDemo(canvas);

            ///Scene Demo
            //SceneDemo.RunDemo(canvas);

            ///Plane Demo
            //PlaneDemo.RunDemo(canvas);

            //Pattern Demo
            //PatternsDemo.RunDemo(canvas);

            ///Refraction Demo
            RefractionDemo.RunDemo(canvas);

            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            //projectileDemo.tick();
            //lbl.SetValue(Label.ContentProperty,  canvasDemo.tick());
            //transformationDemo.RunDemo();
        }
    }
}
