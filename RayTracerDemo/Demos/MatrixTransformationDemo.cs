using RayTracer;
using RayTracer.Display;
using RayTracer.Matrices;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerDemo.Demos
{
    public class MatrixTransformationDemo
    {
        Canvas canvas;
        Point point;
        Matrix4D matrix;
        bool hasRun;
        public MatrixTransformationDemo()
        {
            canvas = new Canvas(501,501);
            point = new Point(0, 250);
            matrix = Matrix4D.RotationZ(Math.PI / 6);
            hasRun = false;
        }

        public void RunDemo()
        {
            Point point = new Point(0, 125, 0);
            if (!hasRun)
            {
                for (int i = 0; i < 12; i++)
                {
                    Point p = Matrix4D.Translation(250, 250, 0) * point;
                    canvas[p.X.Round(), p.Y.Round()] = new Color(1, 1, 1);
                    point = Matrix4D.RotationZ(Math.PI / 6) * point;
                }
                WriteFile(canvas);
                hasRun = true;
            }
        }

        public static void WriteFile(Canvas canvas)
        {
            string image = Image.CanvasToPPMString(canvas);
            File.WriteAllText("C:\\Users\\Obakeng\\Desktop\\RayTracerImages\\translation.ppm", image);
        }
    }
}
